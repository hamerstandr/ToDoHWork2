using System.Windows;
using System.Windows.Controls;

namespace ToDoHWork2
{
    /// <summary>
    /// Interaction logic for Page.xaml
    /// </summary>
    public partial class Page : UserControl
    {
        private Tasks _tasks = null!;

        public Tasks Tasks
        {
            get => _tasks;
            set
            {
                _tasks = value;
                List1.ItemsSource = _tasks.Items;
            }
        }

        public Page()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Text1.Text))
            {
                Tasks.Add(Text1.Text);
                List1.Items.Refresh();
                Text1.Clear();
            }
        }

        private void DelItem_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is Task item)
            {
                Tasks.Items.Remove(item);
                List1.Items.Refresh();
            }
        }
    }
}
