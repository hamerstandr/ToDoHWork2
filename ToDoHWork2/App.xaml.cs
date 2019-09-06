using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;

namespace ToDoHWork2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {

        }
        public static Theme Theme;
        protected override void OnStartup(StartupEventArgs e)
        {
            Theme = new FluentTheme();
            StyleManager.ApplicationTheme = Theme;
            var w = new MainWindow();
            StyleManager.SetTheme(w, Theme);
            w.Show();
            base.OnStartup(e);
        }
        //public static void ShowInTaskbar(RadWindow radWindow)
        //{
        //    Window w = radWindow as Window;
        //    w.ShowInTaskbar = true;
        //}
    }
}
