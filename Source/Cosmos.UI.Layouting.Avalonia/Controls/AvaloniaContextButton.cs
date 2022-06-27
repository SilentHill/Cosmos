using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cosmos.UI.Layoutting.Avalonia.Controls
{
    public class AvaloniaContextButton : Button
    {
        public AvaloniaContextButton()
        {
            // SetResourceReference(StyleProperty, typeof(Button));
            Background = Brushes.Transparent;
            BorderThickness = new Thickness(0);
            FontFamily = new FontFamily("Webdings");
            Content = DropDown;
        }
        private static char DropDown { get; } = (char)54; 

    }
}
