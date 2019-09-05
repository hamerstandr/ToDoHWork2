using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Telerik.Windows;
using Telerik.Windows.Controls;

namespace ToDoHWork2.Modal
{
    class HTabItem: RadTabItem
    {
        public Label LableDate;
        public Label LableTitle;
        public HTabItem()
        {
            StyleManager.SetTheme(this, new Office2016Theme());
            Selector.SelectedEvent.AddOwner(typeof(HTabItem));
        }
        public override void OnSelected(RadRoutedEventArgs e)
        {
            base.OnSelected(e);
            LableTitle.Foreground = Brushes.Black;
            LableDate.Foreground = Brushes.Gray;
        }
        public override void OnUnselected(RadRoutedEventArgs e)
        {
            base.OnUnselected(e);
            LableTitle.Foreground = Brushes.White;
            LableDate.Foreground = Brushes.Gray;
        }
    }
}
