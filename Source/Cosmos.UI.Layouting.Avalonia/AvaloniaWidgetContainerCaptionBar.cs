
using Cosmos.UI.Layoutting.Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Interactivity;

namespace Cosmos.UI.Layoutting.Avalonia
{
    internal class AvaloniaWidgetContainerCaptionBar : UserControl
    {
        internal AvaloniaWidgetContainerCaptionBar(AvaloniaLayoutCell cell)
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
            Cell.ContextMenu.Open();
        }

        private DockPanel RootPanel { get; } = new DockPanel()
        {
            LastChildFill = false
        };
        private AvaloniaLayoutCell Cell { get; set; }
        private AvaloniaContextButton ContextButton { get; } = new AvaloniaContextButton();
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
