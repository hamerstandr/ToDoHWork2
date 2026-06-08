using System;
using System.Collections.Generic;

namespace ToDoHWork2.Models.Dashboard
{
    /// <summary>
    /// مدل پایه برای اطلاعات افزونه‌های داشبورد
    /// </summary>
    public class AddonInfo
    {
        /// <summary>
        /// شناسه یکتای افزونه
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// نام نمایشی افزونه
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// توضیحات افزونه
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// نسخه افزونه
        /// </summary>
        public string Version { get; set; } = "1.0.0";

        /// <summary>
        /// نویسنده افزونه
        /// </summary>
        public string Author { get; set; } = string.Empty;

        /// <summary>
        /// پورت API برای ارتباط با برنامه خارجی
        /// </summary>
        public int ApiPort { get; set; } = 9090;

        /// <summary>
        /// ترتیب نمایش در داشبورد
        /// </summary>
        public int DisplayOrder { get; set; } = 99;

        /// <summary>
        /// آیا افزونه نصب شده است؟
        /// </summary>
        public bool IsInstalled { get; set; } = false;

        /// <summary>
        /// آیا افزونه فعال است؟
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// تنظیمات اختصاصی افزونه
        /// </summary>
        public Dictionary<string, object> Settings { get; set; } = new();

        /// <summary>
        /// آخرین بروزرسانی وضعیت
        /// </summary>
        public DateTime LastUpdated { get; set; } = DateTime.MinValue;

        /// <summary>
        /// وضعیت فعلی (در حال اجرا، متوقف شده، خطا)
        /// </summary>
        public string Status { get; set; } = "Unknown";
    }

    /// <summary>
    /// آرگومان‌های رویداد تغییر وضعیت افزونه
    /// </summary>
    public class AddonStateChangedEventArgs : EventArgs
    {
        public AddonInfo Addon { get; set; } = null!;
        public string PreviousStatus { get; set; } = string.Empty;
        public string NewStatus { get; set; } = string.Empty;
    }
}
