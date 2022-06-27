using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Cosmos.UI.Layoutting.Wpf.Controls
{
    public class WpfContextButton : Button
    {
        public WpfContextButton()
        {
            SetResourceReference(StyleProperty, typeof(Button));
            Background = Brushes.Transparent;
            BorderThickness = new Thickness(0);
            FontFamily = new FontFamily("Webdings");
            Content = DropDown;
        }
        private static char DropDown { get; } = (char)54; 

    }
}
