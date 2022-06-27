
using Cosmos.UI.Layoutting.Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Avalonia.Controls;
using System.Windows.Input;
using Avalonia.Input;
using Avalonia;
using Avalonia.Media;
using Avalonia.Layout;

namespace Cosmos.UI.Layoutting.Avalonia
{
    public class AvaloniaWindow : Window
    {
        public AvaloniaWindow()
        {
            FontFamily = new FontFamily("Microsoft Yahei");
            FontSize = 14;
            //TextOptions.SetTextFormattingMode(this, TextFormattingMode.Display);
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //WindowStyle = WindowStyle.None;
            //FixSomething();

            var dock_panel = new DockPanel()
            {
                LastChildFill = true,
            };
            dock_panel.AddChild(content_control, Dock.Bottom);
            base.Content = dock_panel;
        }
        UserControl content_control = new UserControl();
        public new Object Content
        {
            get
            {
                return content_control.Content;
            }
            set
            {
                content_control.Content = value;
            }
        }

        public Object title_content;
        

        public WindowCaption Caption { get; }

    }


    public sealed class TitleButtonContent
    {
        public const char Minimize  = (char)0x30;
        public const char Maximize  = (char)0x31;
        public const char Restore   = (char)0x32;
        public const char Close     = (char)0x72;
    }

    public class TitleButton : Button
    {
        public TitleButton(char char0, char char1 = (char)0)
        {
            // SetResourceReference(StyleProperty, typeof(Button));
            // SetResourceReference(ForegroundProperty, "Brush.CaptionForeground");
            // SetResourceReference(FontFamilyProperty, "Font.SystemButton");
            // SetResourceReference(FontSizeProperty, "FontSize.Big");
            Padding = new Thickness(8, 0, 8, 0);
            BorderThickness = new Thickness(0);
            Background = Brushes.Transparent;
            Content = char0;
            if (char1 != 0)
            {
                Click += (sender, e) =>
                {
                    if ((char)Content == char0)
                    {
                        Content = char1;
                    }
                    else
                    {
                        Content = char0;
                    }
                };
            }
        }
    }

    public class WindowCaption : UserControl
    {
        public WindowCaption(Window parentWindow)
        {
            ParentWindow = parentWindow;
            // SetResourceReference(ForegroundProperty, "Brush.CaptionForeground");
            // SetResourceReference(BackgroundProperty, "Brush.CaptionBackground");

            DoubleTapped += WindowCaption_DoubleTapped;

            var RootPanel = new DockPanel()
            {
                LastChildFill = false
            };

            MaximizeButton = MakeMaximizeButton();
            MinimizeButton = MakeMinimizeButton();
            CloseButton = MakeCloseButton();
            RootPanel.AddChild(TitleControl, Dock.Left);
            RootPanel.AddChild(CloseButton, Dock.Right);
            RootPanel.AddChild(MaximizeButton, Dock.Right);
            RootPanel.AddChild(MinimizeButton, Dock.Right);
            Content = RootPanel;
        }

        private void WindowCaption_DoubleTapped(object sender, global::Avalonia.Interactivity.RoutedEventArgs e)
        {
            var parent_window = ParentWindow;
            if (parent_window == null)
            {
                return;
            }
            else
            {
                if (parent_window.WindowState == WindowState.Maximized)
                {
                    parent_window.WindowState = WindowState.Normal;
                }
                else if (parent_window.WindowState == WindowState.Normal)
                {
                    parent_window.WindowState = WindowState.Maximized;
                }
            }
        }

        public readonly UserControl TitleControl = new UserControl()
        {
            VerticalContentAlignment = VerticalAlignment.Center
        };


        TitleButton MakeMaximizeButton()
        {
            var button = new TitleButton(TitleButtonContent.Maximize, TitleButtonContent.Restore);
            button.Click += (sender, e) =>
            {
                if (ParentWindow.WindowState == WindowState.Normal)
                {
                    ParentWindow.WindowState = WindowState.Maximized;
                }
                else
                {
                    ParentWindow.WindowState = WindowState.Normal;
                }
            };
            return button;
        }

        TitleButton MakeMinimizeButton()
        {
            var button = new TitleButton(TitleButtonContent.Minimize);
            button.Click += (sender, e) =>
            {
                ParentWindow.WindowState = WindowState.Minimized;
            };
            return button;
        }

        TitleButton MakeCloseButton()
        {
            var button = new TitleButton(TitleButtonContent.Close);
            button.Click += (sender, e) =>
            {
                ParentWindow.Close();
            };
            return button;
        }
        public Window ParentWindow { get; set; }
        TitleButton MaximizeButton;
        TitleButton MinimizeButton;
        TitleButton CloseButton = new TitleButton(TitleButtonContent.Close);

    }

}
