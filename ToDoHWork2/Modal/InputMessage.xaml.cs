using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Telerik.Windows.Controls;

namespace ToDoHWork2.Modal
{
    /// <summary>
    /// Interaction logic for InputMessage.xaml
    /// </summary>
    public partial class InputMessage
    {
        public string Result
        {
            get; set;
        }
        public InputMessage()
        {
            InitializeComponent();
        }
        public InputMessage(string Message)
        {
            InitializeComponent();
            Lable1.Content = Message;
            TxtInput.Focus();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Result = TxtInput.Text;
            this.Close();
        }

        private void TxtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Result = TxtInput.Text;
                this.Close();
            }
        }
    }
}
