using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ToDoHWork2.Services.Dashboard
{
    /// <summary>
    /// سرویس ارتباط با DownloadMenger2 از طریق HTTP API
    /// </summary>
    public class DownloadManagerService
    {
        private HttpClient? _httpClient;
        private string _apiEndpoint = "http://127.0.0.1:9090";
        private bool _isEnabled;

        /// <summary>
        /// آیا سرویس فعال است؟
        /// </summary>
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                if (value && _httpClient == null)
                {
                    _httpClient = new HttpClient();
                }
            }
        }

        /// <summary>
        /// تنظیم API Endpoint
        /// </summary>
        public void SetApiEndpoint(string endpoint)
        {
            _apiEndpoint = endpoint;
        }

        /// <summary>
        /// بررسی نصب بودن DownloadMenger2
        /// </summary>
        public static bool IsDownloadMengerInstalled()
        {
            var possiblePaths = new[]
            {
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
            };

            foreach (var basePath in possiblePaths)
            {
                var path = System.IO.Path.Combine(basePath, "DownloadMenger2");
                if (System.IO.Directory.Exists(path))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// دریافت وضعیت دانلود منیجر
        /// </summary>
        public async Task<DownloadManagerStatus> GetStatusAsync()
        {
            if (!_isEnabled || _httpClient == null)
            {
                return new DownloadManagerStatus { IsRunning = false };
            }

            try
            {
                var response = await _httpClient.GetAsync($"{_apiEndpoint}/api/status");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    // TODO: Parse JSON response
                    return new DownloadManagerStatus
                    {
                        IsRunning = true,
                        ActiveDownloads = 0,
                        TotalDownloadSpeed = 0
                    };
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting download status: {ex.Message}");
            }

            return new DownloadManagerStatus { IsRunning = false };
        }

        /// <summary>
        /// دریافت لیست دانلودهای فعال
        /// </summary>
        public async Task<DownloadItem[]> GetActiveDownloadsAsync()
        {
            if (!_isEnabled || _httpClient == null)
            {
                return Array.Empty<DownloadItem>();
            }

            try
            {
                var response = await _httpClient.GetAsync($"{_apiEndpoint}/api/downloads/active");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    // TODO: Parse JSON response
                    return Array.Empty<DownloadItem>();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting active downloads: {ex.Message}");
            }

            return Array.Empty<DownloadItem>();
        }
    }

    /// <summary>
    /// وضعیت دانلود منیجر
    /// </summary>
    public class DownloadManagerStatus
    {
        public bool IsRunning { get; set; }
        public int ActiveDownloads { get; set; }
        public double TotalDownloadSpeed { get; set; }
    }

    /// <summary>
    /// آیتم دانلود
    /// </summary>
    public class DownloadItem
    {
        public string FileName { get; set; } = string.Empty;
        public double Progress { get; set; }
        public double Speed { get; set; }
        public long TotalSize { get; set; }
        public long DownloadedSize { get; set; }
    }
}
