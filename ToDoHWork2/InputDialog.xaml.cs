using System.Windows;

namespace ToDoHWork2
{
    /// <summary>
    /// Simple input dialog for getting user text input
    /// </summary>
    public partial class InputDialog : Window
    {
        public string? Result { get; private set; }

        public InputDialog(string message)
        {
            InitializeComponent();
            MessageText.Text = message;
            InputTextBox.Focus();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Result = InputTextBox.Text;
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Result = null;
            DialogResult = false;
            Close();
        }
    }
}
