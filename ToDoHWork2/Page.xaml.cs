using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using ToDoHWork2.Modal;

namespace ToDoHWork2
{
    /// <summary>
    /// Interaction logic for Page.xaml
    /// </summary>
    public partial class Page : UserControl
    {
        private Tasks tasks;

        public Tasks Tasks { get => tasks;
            set
            {
                tasks = value;
                List1.ItemsSource = tasks.Items;
            }
        }

        public Page()
        {
            InitializeComponent();
            this.Unloaded += Page_Unloaded;
            StyleManager.SetTheme(Text1, App.Theme);
            StyleManager.SetTheme(List1, App.Theme);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            MainWindow.Me.Database.Save();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {if(Text1.Text != "")
            {
                Tasks.Add(Text1.Text);
                List1.Items.Refresh();
                Text1.Text = "";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.Me.Database.Save();
        }
        private void DelItem_Click(object sender, RoutedEventArgs e)
        {
            Task item = (Task)(sender as Button).DataContext;
            Tasks.Items.Remove(item);
            List1.Items.Refresh();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //MainWindow.Msg.Alert(Tasks.Title,Delete);
            var r=Messagebox.Show(MainWindow.Me, Tasks.Title, "Delete Work", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if(r==MessageBoxResult.OK)
                MainWindow.Me.Remove(this.Parent as HTabItem);
        }

        private void Delete(object sender, WindowClosedEventArgs e)
        {
            if(e.DialogResult.Value)
                MainWindow.Me.Remove(this.Parent as HTabItem);
        }
    }
}
