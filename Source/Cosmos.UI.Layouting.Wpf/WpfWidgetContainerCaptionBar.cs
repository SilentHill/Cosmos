
using Cosmos.UI.Layoutting.Wpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Cosmos.UI.Layoutting.Wpf
{
    internal class WpfWidgetContainerCaptionBar : UserControl
    {
        internal WpfWidgetContainerCaptionBar(WpfLayoutCell cell)
        {
            FontSize = 8;
            //BorderBrush = Brushes.Firebrick;
            //BorderThickness = new Thickness(1);
            Background = Brushes.Crimson;
            Cell = cell;
            ContextButton.Click += HandleContextButtonClick;
            RootPanel.AddChild(TitleTextBlock, Dock.Left);
            RootPanel.AddChild(ContextButton, Dock.Right);
            Content = RootPanel;
        }

        private void HandleContextButtonClick(object sender, RoutedEventArgs e)
        {
            Cell.ContextMenu.IsOpen = true;
        }

        private DockPanel RootPanel { get; } = new DockPanel()
        {
            LastChildFill = false
        };
        private WpfLayoutCell Cell { get; set; }
        private WpfContextButton ContextButton { get; } = new WpfContextButton();
        private TextBlock TitleTextBlock { get; } = new TextBlock();
        public String Title
        {
            get
            {
                return TitleTextBlock.Text;
            }
            set
            {
                TitleTextBlock.Text = value;
            }
        }
    }
}
