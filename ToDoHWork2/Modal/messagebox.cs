using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace ToDoHWork2.Modal
{
    public class Messagebox
    {
        public void Alert(string Messsage="Hello", EventHandler<WindowClosedEventArgs> closed=null)
        {
            if (closed==null)
                RadWindow.Alert(Messsage);
            else
                RadWindow.Alert(Messsage, closed);
        }
        private void AlertClosed(object sender, WindowClosedEventArgs e)
        {
            var result = e.DialogResult;
            if (result == true)
            {
                // handle confirmation 
            }
        }
        public static MessageBoxResult Show(ContentControl ctrlOwner, string strMessage, string strCaption = null, MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage image = MessageBoxImage.Warning)
        {
            try
            {
                RadMessageBox dlg = new RadMessageBox();
                dlg.DialogImage = image;
                dlg.Buttons = button;

                if (strCaption != null)
                    dlg.Header = strCaption;

                dlg.txtMessage.Text = strMessage;

                if (ctrlOwner != null)
                {
                    dlg.Owner = ctrlOwner;
                }

                dlg.ShowDialog();

                MessageBoxResult res = dlg.Result;
                return (res);
            }
            catch (Exception err)
            {
                System.Diagnostics.Trace.TraceError(err.ToString());
                return (MessageBoxResult.None);
            }
        }
        public void Input(string Message= "Enter your name:")
        {
            //StyleManager.SetTheme(this, new Office2016Theme());
            RadWindow.Prompt(Message, InputClose);
        }
        public event EventHandler<string> InputClosed;
        private void InputClose(object sender, WindowClosedEventArgs e)
        {
            InputClosed.Invoke(this, e.PromptResult);
        }
        public string InputBox(string Message = "Enter your name:")
        {
            var i=new InputMessage(Message);
            i.ShowDialog();
            return i.Result;
        }
    }
}
