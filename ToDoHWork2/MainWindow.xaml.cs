using System;
using System.Windows;
using System.Windows.Controls;

namespace ToDoHWork2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow? Me { get; private set; }
        private readonly Database _database = new();

        public Database Database => _database;

        public MainWindow()
        {
            InitializeComponent();
            Me = this;
            Closing += MainWindow_Closing;

            foreach (var tasks in Database.Data.Works)
            {
                var page = new Page { Tasks = tasks };
                AddWorkBook(page);
            }
        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            Database.Save();
        }

        private void AddWorkBook(Page page)
        {
            var tabItem = new TabItem
            {
                Header = CreateHeader(page.Tasks),
                Content = page,
                Tag = page
            };
            WorkList.Items.Add(tabItem);
            WorkList.SelectedItem = tabItem;
        }

        private StackPanel CreateHeader(Tasks tasks)
        {
            var stackPanel = new StackPanel();
            var labelTitle = new TextBlock
            {
                Text = tasks.Title,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            var labelDate = new TextBlock
            {
                Text = tasks.Date.ToString("yyyy-MM-dd HH:mm"),
                FontSize = 11,
                Foreground = System.Windows.Media.Brushes.Gray
            };
            stackPanel.Children.Add(labelTitle);
            stackPanel.Children.Add(labelDate);
            return stackPanel;
        }

        internal void Remove(TabItem tabItem)
        {
            WorkList.Items.Remove(tabItem);
            if (tabItem.Content is Page page)
            {
                Database.Data.Works.Remove(page.Tasks);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new InputDialog("Enter Title");
            dialog.Owner = this;
            if (dialog.ShowDialog() == true && !string.IsNullOrEmpty(dialog.Result))
            {
                AddNewWork(dialog.Result);
            }
        }

        private void AddNewWork(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                var page = new Page
                {
                    Tasks = Database.Data.Add(title)
                };
                AddWorkBook(page);
            }
        }
    }
}
