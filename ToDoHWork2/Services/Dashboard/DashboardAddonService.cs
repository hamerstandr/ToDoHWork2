using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ToDoHWork2.Models.Dashboard;

namespace ToDoHWork2.Services.Dashboard
{
    /// <summary>
    /// سرویس مدیریت افزونه‌های داشبورد TrafficWatch
    /// مسئولیت‌ها:
    /// - ثبت و شناسایی افزونه‌ها
    /// - بررسی وضعیت نصب بودن برنامه‌های خارجی
    /// - مدیریت تنظیمات افزونه‌ها
    /// - ارائه API Endpoint برای هر افزونه
    /// </summary>
    public class DashboardAddonService
    {
        private static DashboardAddonService? _instance;
        private readonly List<AddonInfo> _addons = new();
        private readonly string _configPath;
        private readonly object _lock = new();

        /// <summary>
        /// نمونه یکتای سرویس (Singleton)
        /// </summary>
        public static DashboardAddonService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DashboardAddonService();
                }
                return _instance;
            }
        }

        /// <summary>
        /// رویداد تغییر وضعیت افزونه
        /// </summary>
        public event EventHandler<AddonStateChangedEventArgs>? OnAddonStateChanged;

        /// <summary>
        /// فاصله بروزرسانی خودکار (ثانیه)
        /// </summary>
        public int RefreshInterval { get; set; } = 5;

        /// <summary>
        /// آیا سیستم داشبورد فعال است؟
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        private DashboardAddonService()
        {
            _configPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "TrafficWatch",
                "addons.json");

            RegisterDefaultAddons();
        }

        /// <summary>
        /// راه‌اندازی اولیه سرویس
        /// </summary>
        public void Initialize()
        {
            LoadAddons();
            _ = ScanAllAddonsAsync();
        }

        /// <summary>
        /// ثبت افزونه‌های پیش‌فرض
        /// </summary>
        private void RegisterDefaultAddons()
        {
            // افزونه دانلود منیجر
            if (!_addons.Any(a => a.Id == "download-manager"))
            {
                _addons.Add(new AddonInfo
                {
                    Id = "download-manager",
                    Name = "Download Manager",
                    Description = "Integration with DownloadMenger2",
                    Version = "1.0.0",
                    Author = "TrafficWatch Team",
                    ApiPort = 9090,
                    DisplayOrder = 1,
                    Settings = new Dictionary<string, object>
                    {
                        { "ShowSpeed", true },
                        { "ShowProgress", true },
                        { "MaxItems", 10 }
                    }
                });
            }

            // افزونه پخش موسیقی
            if (!_addons.Any(a => a.Id == "music-player"))
            {
                _addons.Add(new AddonInfo
                {
                    Id = "music-player",
                    Name = "Music Player",
                    Description = "Integration with Music Player",
                    Version = "1.0.0",
                    Author = "TrafficWatch Team",
                    ApiPort = 9091,
                    DisplayOrder = 2,
                    Settings = new Dictionary<string, object>
                    {
                        { "ShowAlbumArt", true },
                        { "ShowLyrics", false }
                    }
                });
            }

            // افزونه مانیتور سیستم
            if (!_addons.Any(a => a.Id == "system-monitor"))
            {
                _addons.Add(new AddonInfo
                {
                    Id = "system-monitor",
                    Name = "System Monitor",
                    Description = "CPU, RAM, and Network monitoring",
                    Version = "1.0.0",
                    Author = "TrafficWatch Team",
                    ApiPort = 0, // No external API needed
                    DisplayOrder = 3,
                    Settings = new Dictionary<string, object>
                    {
                        { "ShowCPU", true },
                        { "ShowRAM", true },
                        { "ShowNetwork", true },
                        { "UpdateInterval", 2 }
                    }
                });
            }
        }

        /// <summary>
        /// اسکن تمام افزونه‌ها برای بررسی وضعیت نصب بودن
        /// </summary>
        public async Task ScanAllAddonsAsync()
        {
            foreach (var addon in _addons)
            {
                await ScanAddonAsync(addon);
            }
        }

        /// <summary>
        /// اسکن یک افزونه خاص برای بررسی وضعیت نصب بودن
        /// </summary>
        private async Task ScanAddonAsync(AddonInfo addon)
        {
            await Task.Run(() =>
            {
                var previousStatus = addon.Status;
                bool wasInstalled = addon.IsInstalled;

                // بررسی نصب بودن بر اساس ID
                addon.IsInstalled = addon.Id switch
                {
                    "download-manager" => CheckDownloadMengerInstalled(),
                    "music-player" => CheckMusicPlayerInstalled(),
                    "system-monitor" => true, // Always available
                    _ => false
                };

                addon.LastUpdated = DateTime.Now;
                addon.Status = addon.IsInstalled ? "Ready" : "Not Installed";

                // ارسال رویداد در صورت تغییر وضعیت
                if (wasInstalled != addon.IsInstalled || previousStatus != addon.Status)
                {
                    OnAddonStateChanged?.Invoke(this, new AddonStateChangedEventArgs
                    {
                        Addon = addon,
                        PreviousStatus = previousStatus,
                        NewStatus = addon.Status
                    });
                }
            });
        }

        /// <summary>
        /// بررسی نصب بودن DownloadMenger2
        /// </summary>
        private bool CheckDownloadMengerInstalled()
        {
            // مسیرهای معمول نصب
            var possiblePaths = new[]
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "DownloadMenger2"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "DownloadMenger2"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DownloadMenger2")
            };

            return possiblePaths.Any(Directory.Exists);
        }

        /// <summary>
        /// بررسی نصب بودن Music Player
        /// </summary>
        private bool CheckMusicPlayerInstalled()
        {
            var possiblePaths = new[]
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "MusicPlayer"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "MusicPlayer")
            };

            return possiblePaths.Any(Directory.Exists);
        }

        /// <summary>
        /// دریافت تمام افزونه‌ها
        /// </summary>
        public List<AddonInfo> GetAllAddons()
        {
            lock (_lock)
            {
                return _addons.OrderBy(a => a.DisplayOrder).ToList();
            }
        }

        /// <summary>
        /// دریافت افزونه بر اساس ID
        /// </summary>
        public AddonInfo? GetAddonById(string id)
        {
            lock (_lock)
            {
                return _addons.FirstOrDefault(a => a.Id == id);
            }
        }

        /// <summary>
        /// دریافت API Endpoint برای افزونه
        /// </summary>
        public string GetAddonApiEndpoint(string addonId)
        {
            var addon = GetAddonById(addonId);
            if (addon == null || addon.ApiPort == 0)
            {
                return string.Empty;
            }

            return $"http://127.0.0.1:{addon.ApiPort}";
        }

        /// <summary>
        /// بروزرسانی تنظیمات افزونه
        /// </summary>
        public void UpdateAddonSettings(string addonId, Dictionary<string, object> settings)
        {
            var addon = GetAddonById(addonId);
            if (addon != null)
            {
                lock (_lock)
                {
                    addon.Settings = settings;
                    SaveAddons();
                }
            }
        }

        /// <summary>
        /// تنظیم ترتیب نمایش افزونه
        /// </summary>
        public void SetAddonDisplayOrder(string addonId, int order)
        {
            var addon = GetAddonById(addonId);
            if (addon != null)
            {
                lock (_lock)
                {
                    addon.DisplayOrder = order;
                    SaveAddons();
                }
            }
        }

        /// <summary>
        /// فعال/غیرفعال کردن افزونه
        /// </summary>
        public void SetAddonEnabled(string addonId, bool enabled)
        {
            var addon = GetAddonById(addonId);
            if (addon != null)
            {
                lock (_lock)
                {
                    var previousStatus = addon.Status;
                    addon.IsEnabled = enabled;
                    addon.Status = enabled ? "Ready" : "Disabled";

                    OnAddonStateChanged?.Invoke(this, new AddonStateChangedEventArgs
                    {
                        Addon = addon,
                        PreviousStatus = previousStatus,
                        NewStatus = addon.Status
                    });

                    SaveAddons();
                }
            }
        }

        /// <summary>
        /// بارگذاری تنظیمات افزونه‌ها از فایل
        /// </summary>
        private void LoadAddons()
        {
            // TODO: Implement JSON loading
            // For now, use default registered addons
        }

        /// <summary>
        /// ذخیره تنظیمات افزونه‌ها در فایل
        /// </summary>
        private void SaveAddons()
        {
            // TODO: Implement JSON saving
            try
            {
                var directory = Path.GetDirectoryName(_configPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                // Implementation pending
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving addons config: {ex.Message}");
            }
        }
    }
}
