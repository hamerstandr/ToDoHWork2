# راهنمای توسعه و یکپارچه‌سازی افزونه‌های داشبورد TrafficWatch

## فهرست مطالب
1. [معرفی سیستم افزونه‌ها](#معرفی-سیستم-افزونه‌ها)
2. [معماری سیستم](#معماری-سیستم)
3. [راه‌اندازی اولیه](#راه‌اندازی-اولیه)
4. [ایجاد افزونه جدید](#ایجاد-افزونه-جدید)
5. [یکپارچه‌سازی با DownloadMenger2](#یکپارچه‌سازی-با-downloadmenger2)
6. [تنظیمات و پیکربندی](#تنظیمات-و-پیکربندی)
7. [نمونه کدها](#نمونه-کدها)

---

## معرفی سیستم افزونه‌ها

سیستم داشبورد TrafficWatch یک سیستم قابل گسترش است که امکان اضافه کردن ماژول‌های مختلف را فراهم می‌کند. هر افزونه می‌تواند:

- اطلاعات خاصی را نمایش دهد (دانلود، موسیقی، مانیتورینگ سیستم و...)
- تنظیمات مخصوص به خود داشته باشد
- به صورت مستقل فعال یا غیرفعال شود
- با برنامه‌های خارجی ارتباط برقرار کند

### ویژگی‌های کلیدی

1. **نصب آسان**: افزونه‌ها به صورت خودکار شناسایی می‌شوند
2. **قابل پیکربندی**: هر افزونه تنظیمات مخصوص به خود را دارد
3. **ترتیب نمایش**: کاربر می‌تواند ترتیب نمایش افزونه‌ها را تغییر دهد
4. **ارتباط API**: امکان ارتباط با برنامه‌های خارجی از طریق HTTP API

---

## معماری سیستم

```
┌─────────────────────────────────────────────────────────┐
│                   TrafficWatch Dashboard                │
├─────────────────────────────────────────────────────────┤
│  ┌─────────────┐  ┌─────────────┐  ┌─────────────┐     │
│  │   Addon 1   │  │   Addon 2   │  │   Addon 3   │     │
│  │  (Download) │  │   (Music)   │  │  (System)   │     │
│  └─────────────┘  └─────────────┘  └─────────────┘     │
├─────────────────────────────────────────────────────────┤
│              DashboardAddonService                      │
│         (مدیریت افزونه‌ها و تنظیمات)                     │
├─────────────────────────────────────────────────────────┤
│              External APIs (Optional)                   │
│    DownloadMenger2 :9090 | MusicPlayer :9091 | ...      │
└─────────────────────────────────────────────────────────┘
```

### ساختار فایل‌ها

```
ToDoHWork2/
├── Models/
│   └── Dashboard/
│       ├── AddonInfo.cs              # مدل‌های پایه افزونه
│       └── [AddonName]AddonInfo.cs   # مدل‌های اختصاصی
├── Services/
│   └── Dashboard/
│       ├── DashboardAddonService.cs  # سرویس مدیریت افزونه‌ها
│       └── [AddonName]Service.cs     # سرویس‌های اختصاصی
└── View/
    └── Dashboard/
        └── [AddonName]Tab.xaml       # UI هر افزونه
```

---

## راه‌اندازی اولیه

### 1. افزودن به App.xaml.cs

در فایل `App.xaml.cs`، سرویس داشبورد را در روش `OnStartup` راه‌اندازی کنید:

```csharp
protected override void OnStartup(StartupEventArgs e)
{
    base.OnStartup(e);
    
    // راه‌اندازی سرویس داشبورد
    DashboardAddonService.Instance.Initialize();
}
```

### 2. بررسی وضعیت نصب

برنامه باید به صورت دوره‌ای وضعیت نصب بودن برنامه‌های خارجی را بررسی کند:

```csharp
// در MainWindow یا هر جای دیگر
await DashboardAddonService.Instance.ScanAllAddonsAsync();
```

---

## ایجاد افزونه جدید

### مرحله 1: ایجاد مدل اطلاعات

یک کلاس جدید در پوشه `Models/Dashboard` ایجاد کنید:

```csharp
using System.Collections.Generic;
using ToDoHWork2.Models.Dashboard;

namespace ToDoHWork2.Models.Dashboard
{
    public class MyNewAddonInfo : AddonInfo
    {
        public MyNewAddonInfo()
        {
            Id = "my-new-addon";
            Name = "My New Addon";
            Description = "Description of my new addon";
            Version = "1.0.0";
            Author = "Your Name";
            ApiPort = 9092; // پورت API
            DisplayOrder = 4; // ترتیب نمایش
            
            Settings = new Dictionary<string, object>
            {
                { "Setting1", true },
                { "Setting2", 100 },
                { "Setting3", "value" }
            };
        }
    }
}
```

### مرحله 2: ایجاد سرویس اختصاصی

یک سرویس برای مدیریت منطق افزونه ایجاد کنید:

```csharp
using System;
using System.Threading.Tasks;
using ToDoHWork2.Services.Dashboard;

namespace ToDoHWork2.Services.Dashboard
{
    public class MyNewAddonService
    {
        private readonly string _apiEndpoint;
        
        public MyNewAddonService()
        {
            var addon = DashboardAddonService.Instance.GetAddonById("my-new-addon");
            _apiEndpoint = DashboardAddonService.Instance.GetAddonApiEndpoint("my-new-addon");
        }
        
        public async Task<object> GetDataAsync()
        {
            // دریافت داده از API یا منابع دیگر
            // ...
            return null;
        }
    }
}
```

### مرحله 3: ایجاد UI

یک UserControl یا Tab برای نمایش افزونه ایجاد کنید:

```xml
<!-- View/Dashboard/MyNewAddonTab.xaml -->
<UserControl x:Class="ToDoHWork2.View.Dashboard.MyNewAddonTab"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <Grid>
        <!-- UI elements here -->
    </Grid>
</UserControl>
```

### مرحله 4: ثبت افزونه

در روش `RegisterDefaultAddons` از کلاس `DashboardAddonService`:

```csharp
private void RegisterDefaultAddons()
{
    // ... افزونه‌های موجود ...
    
    if (!_addons.Any(a => a.Id == "my-new-addon"))
    {
        _addons.Add(new MyNewAddonInfo());
    }
}
```

---

## یکپارچه‌سازی با DownloadMenger2

### پیش‌نیازها

- نصب بودن DownloadMenger2 نسخه 2.0 یا بالاتر
- فعال بودن API در تنظیمات DownloadMenger2
- پورت پیش‌فرض: 9090

### مراحل اتصال

#### 1. بررسی نصب بودن

سیستم به صورت خودکار مسیرهای نصب معمول را بررسی می‌کند:

```csharp
bool isInstalled = DownloadManagerService.IsDownloadMengerInstalled();
// یا
var addon = DashboardAddonService.Instance.GetAddonById("download-manager");
bool isInstalled = addon.IsInstalled;
```

#### 2. دریافت وضعیت دانلود منیجر

```csharp
var dmService = new DownloadManagerService();
dmService.IsEnabled = true;
dmService.SetApiEndpoint("http://127.0.0.1:9090");

var status = await dmService.GetStatusAsync();

if (status.IsRunning)
{
    Console.WriteLine($"Active Downloads: {status.ActiveDownloads}");
    Console.WriteLine($"Total Speed: {status.TotalDownloadSpeed} bytes/s");
}
```

#### 3. دریافت لیست دانلودها

```csharp
var downloads = await dmService.GetActiveDownloadsAsync();

foreach (var download in downloads)
{
    Console.WriteLine($"{download.FileName}: {download.Progress}%");
}
```

### تنظیمات DownloadMenger2

در برنامه DownloadMenger2، تنظیمات زیر باید فعال باشند:

1. **Enable TrafficWatch Integration**: `true`
2. **API Port**: `9090` (یا پورت دلخواه)
3. **Allow Local Connections**: `true`

---

## تنظیمات و پیکربندی

### تنظیمات عمومی داشبورد

| نام تنظیم | نوع | پیش‌فرض | توضیحات |
|-----------|-----|---------|----------|
| IsEnabled | bool | true | فعال/غیرفعال کردن کل سیستم داشبورد |
| RefreshInterval | int | 5 | فاصله بروزرسانی (ثانیه) |

### تنظیمات هر افزونه

هر افزونه می‌تواند تنظیمات مخصوص به خود را داشته باشد:

```csharp
var addon = DashboardAddonService.Instance.GetAddonById("download-manager");

// خواندن تنظیم
bool showSpeed = (bool)addon.Settings["ShowSpeed"];

// تغییر تنظیم
addon.Settings["ShowSpeed"] = false;
DashboardAddonService.Instance.UpdateAddonSettings("download-manager", addon.Settings);
```

### تغییر ترتیب نمایش

```csharp
// قرار دادن دانلود منیجر در اولویت اول
DashboardAddonService.Instance.SetAddonDisplayOrder("download-manager", 1);

// قرار دادن مانیتور سیستم در اولویت دوم
DashboardAddonService.Instance.SetAddonDisplayOrder("system-monitor", 2);
```

### فعال/غیرفعال کردن افزونه

```csharp
// غیرفعال کردن افزونه موسیقی
DashboardAddonService.Instance.SetAddonEnabled("music-player", false);

// فعال کردن مجدد
DashboardAddonService.Instance.SetAddonEnabled("music-player", true);
```

---

## نمونه کدها

### نمونه کامل: نمایش اطلاعات دانلود منیجر در داشبورد

```csharp
using System;
using System.Threading.Tasks;
using System.Windows;
using ToDoHWork2.Services.Dashboard;
using ToDoHWork2.Models.Dashboard;

namespace ToDoHWork2.ViewModel
{
    public class DownloadManagerViewModel
    {
        private readonly DownloadManagerService _dmService;
        private System.Timers.Timer _refreshTimer;
        
        public DownloadManagerViewModel()
        {
            _dmService = new DownloadManagerService();
            
            // بررسی نصب بودن
            var addon = DashboardAddonService.Instance.GetAddonById("download-manager");
            if (addon.IsInstalled && addon.IsEnabled)
            {
                Initialize();
            }
            
            // گوش دادن به تغییرات وضعیت افزونه
            DashboardAddonService.Instance.OnAddonStateChanged += OnAddonStateChanged;
        }
        
        private void OnAddonStateChanged(object sender, AddonStateChangedEventArgs e)
        {
            if (e.Addon.Id == "download-manager")
            {
                if (e.Addon.IsEnabled && e.Addon.IsInstalled)
                {
                    Initialize();
                }
                else
                {
                    Stop();
                }
            }
        }
        
        private void Initialize()
        {
            _dmService.IsEnabled = true;
            _dmService.SetApiEndpoint(DashboardAddonService.Instance.GetAddonApiEndpoint("download-manager"));
            
            _refreshTimer = new System.Timers.Timer(5000); // 5 seconds
            _refreshTimer.Elapsed += async (s, e) => await RefreshDataAsync();
            _refreshTimer.Start();
            
            // اولین بروزرسانی
            _ = RefreshDataAsync();
        }
        
        private async Task RefreshDataAsync()
        {
            try
            {
                var status = await _dmService.GetStatusAsync();
                
                if (status.IsRunning)
                {
                    // بروزرسانی UI
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ActiveDownloads = status.ActiveDownloads;
                        TotalSpeed = status.TotalDownloadSpeed;
                    });
                    
                    var downloads = await _dmService.GetActiveDownloadsAsync();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error refreshing download data: {ex.Message}");
            }
        }
        
        private void Stop()
        {
            _refreshTimer?.Stop();
            _refreshTimer?.Dispose();
        }
        
        public int ActiveDownloads { get; private set; }
        public double TotalSpeed { get; private set; }
    }
}
```

---

## عیب‌یابی

### مشکل: افزونه نمایش داده نمی‌شود

**راه حل:**
1. بررسی کنید افزونه در `RegisterDefaultAddons` ثبت شده باشد
2. بررسی کنید `IsEnabled` و `IsInstalled` هر دو `true` باشند
3. لاگ‌ها را بررسی کنید

### مشکل: ارتباط با DownloadMenger2 برقرار نمی‌شود

**راه حل:**
1. بررسی کنید DownloadMenger2 در حال اجرا باشد
2. بررسی کنید پورت 9090 آزاد باشد
3. API را مستقیماً تست کنید: `curl http://127.0.0.1:9090/api/status`
4. تنظیمات DownloadMenger2 را بررسی کنید

---

## بهترین روش‌ها

1. **Thread Safety**: همیشه بروزرسانی UI را در thread اصلی انجام دهید
2. **Error Handling**: تمام خطاها را مدیریت کنید تا برنامه کرش نکند
3. **Performance**: از Timer با فاصله مناسب استفاده کنید (نه خیلی کوتاه)
4. **Memory Management**: رویدادها را هنگام حذف اشتراک لغو کنید
5. **User Experience**: وضعیت نصب/اجرا را به کاربر نمایش دهید

---

## سوالات متداول

**سوال:** آیا می‌توانم افزونه‌های شخص ثالث اضافه کنم؟  
**جواب:** بله، با پیروی از ساختار `AddonInfo` می‌توانید افزونه‌های جدید ایجاد کنید.

**سوال:** چگونه می‌توانم پورت API را تغییر دهم؟  
**جواب:** در تنظیمات هر افزونه، مقدار `ApiPort` را تغییر دهید.

**سوال:** آیا افزونه‌ها می‌توانند به اینترنت متصل شوند؟  
**جواب:** بله، اما توصیه می‌شود فقط از localhost استفاده کنید.

---

**نسخه سند:** 1.0  
**تاریخ انتشار:** 2024  
**تهیه شده برای:** TrafficWatch Development Team
