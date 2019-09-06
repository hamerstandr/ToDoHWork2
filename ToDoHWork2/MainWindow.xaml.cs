using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using ToDoHWork2.Modal;
using Label = Telerik.Windows.Controls.Label;

namespace ToDoHWork2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RadWindow
    {
        public static MainWindow Me;
        public static Messagebox Msg = new Messagebox();
        readonly Database database = new Database();
        internal Database Database => database;
        public MainWindow()
        {
            InitializeComponent();
            Me = this;
            this.Closed += MainWindow_Closed; ;
            foreach(Tasks t in Database.Data.Works)
            {
                var p = new Page();
                p.Tasks = t;
                AddWorkBook(p);
            }
            //StyleManager.SetTheme(this,new Office2016Theme());
            //Msg.InputClosed += Msg_InputClosed;
            
        }

        private void MainWindow_Closed(object sender, WindowClosedEventArgs e)
        {
            // e.DialogResult = false;//==e.Cancel
            Database.Save();
            App.Current.Shutdown();
        }

        //private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    Database.Save();
        //}

        void AddWorkBook(Page page)
        {
            HTabItem tabItem = new HTabItem();
            tabItem.Header = SetHeader(tabItem,page.Tasks);
            
            tabItem.Content = page;
            WorkList.Items.Add(tabItem);
            WorkList.SelectedItem = tabItem;
            var ev= Selector.SelectedEvent.AddOwner(tabItem.GetType());
            
        }

        internal void Remove(HTabItem TabItem)
        {
            WorkList.Items.Remove(TabItem);
            var page = TabItem.Content as Page;
            Database.Data.Works.Remove(page.Tasks);
        }

        double Size;
        private object SetHeader(HTabItem tabItem, Tasks tasks)
        {
            StackPanel s = new StackPanel();
            Label LableTitle = new Label();
            LableTitle.Content = tasks.Title;
            Label LableDate = new Label();

            Size = LableTitle.FontSize;
            LableDate.FontSize = Size - 2;

            LableDate.Content = tasks.Date;
            LableDate.Foreground = Brushes.DarkGray;
            s.Children.Add(LableTitle);
            s.Children.Add(LableDate);
            tabItem.LableDate = LableDate;
            tabItem.LableTitle = LableTitle;
            return s;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            database.Save();
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //Msg.Input("Enter Title");
            var title = Msg.InputBox("Enter Title");
            AddnewWork(title);
        }

        //private void Msg_InputClosed(object sender, string e)
        //{
        //    AddnewWork(e);
        //}

        private void AddnewWork(string Title)
        {
            if (Title != null)
            {
                Page p = new Page(); 
                p.Tasks = database.Data.Add(Title);
                StyleManager.SetTheme(p, App.Theme);
                AddWorkBook(p);
            }
            
        }

        private void WorkList_SelectionChanged(object sender, RadSelectionChangedEventArgs e)
        {
            foreach (HTabItem tab in e.AddedItems)
            {
                if (tab.IsSelected)
                {
                    tab.LableTitle.Foreground = Brushes.Black;
                    tab.LableDate.Foreground = Brushes.DarkGray;
                }
            }
            foreach (HTabItem tab in e.RemovedItems)
            {
                if (!tab.IsSelected)
                {
                    tab.LableTitle.Foreground = Brushes.FloralWhite;
                    tab.LableDate.Foreground = Brushes.Gray;
                }
            }
        }
    }
}
