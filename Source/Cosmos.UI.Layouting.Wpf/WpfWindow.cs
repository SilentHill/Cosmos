
using Cosmos.UI.Layoutting.Wpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shell;
//using static Cosmos.UI.Layoutting.Wpf.Win32.WinApi;

namespace Cosmos.UI.Layoutting.Wpf
{
    public class WpfWindow : Window
    {
        //public static IntPtr WpfWindowPreProc(IntPtr hWnd, Int32 msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        //{
        //    unsafe
        //    {
        //        const Int32 WM_GETMINMAXINFO = 0x0024;
        //        const Int32 MONITOR_DEFAULTTONULL = 0x00000000;
        //        const Int32 MONITOR_DEFAULTTOPRIMARY = 0x00000001;
        //        const Int32 MONITOR_DEFAULTTONEAREST = 0x00000002;
        //        if (msg == WM_GETMINMAXINFO)
        //        {
        //            var mmi = (MINMAXINFO*)lParam.ToPointer();
        //            var monitor = MonitorFromWindow(hWnd, MONITOR_DEFAULTTONEAREST);
        //            if (monitor != IntPtr.Zero)
        //            {
        //                var monitor_info = new MONITORINFO();
        //                monitor_info.cbSize = (UInt32)sizeof(MONITORINFO);
        //                GetMonitorInfo(monitor, ref monitor_info);
        //                RECT work_area = monitor_info.rcWork;
        //                RECT monitor_area = monitor_info.rcMonitor;
        //                mmi->ptMaxPosition.x = Math.Abs(work_area.Left - monitor_area.Left);
        //                mmi->ptMaxPosition.y = Math.Abs(work_area.Top - monitor_area.Top);
        //                mmi->ptMaxSize.x = Math.Abs(work_area.Right - work_area.Left);
        //                mmi->ptMaxSize.y = Math.Abs(work_area.Bottom - work_area.Top);
        //
        //                if (work_area.Bottom == monitor_area.Bottom)
        //                {
        //                    mmi->ptMaxSize.y--;
        //                }
        //            }
        //            handled = true;
        //        }
        //        return IntPtr.Zero;
        //    }
        //}

        
        public WpfWindow()
        {
            TextOptions.SetTextFormattingMode(this, TextFormattingMode.Display);
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            WindowStyle = WindowStyle.None;
            FixSomething();

            var dock_panel = new DockPanel()
            {
                LastChildFill = true,
            };
            dock_panel.AddChild(Caption, Dock.Top);
            dock_panel.AddChild(content_control, Dock.Bottom);
            base.Content = dock_panel;
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var wih = new WindowInteropHelper(this);
            var hs = HwndSource.FromHwnd(wih.Handle);
            //hs.AddHook(new HwndSourceHook(WpfWindowPreProc));
            InvalidateVisual();
        }

        bool IsTransparent
        {
            get
            {
                return AllowsTransparency == true && Opacity != 1.0;
            }
            set
            {
                AllowsTransparency = value;
                if (AllowsTransparency == true)
                {
                    Opacity = 0.618;
                }
                else
                {
                    Opacity = 1.0;
                }
            }
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
        public Object TitleContent
        {
            set
            {
                title_content = value;

                if (Caption != null)
                {
                    Caption.TitleControl.Content = value;
                }
            }
        }

        public WindowCaption Caption = new WindowCaption();

        void FixSomething()
        {
            var window_chrome = new WindowChrome
            {
                UseAeroCaptionButtons = false,
                CaptionHeight = 0,
                CornerRadius = new CornerRadius(0),
                GlassFrameThickness = new Thickness(0),
            };
            WindowChrome.SetWindowChrome(this, window_chrome);
        }
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
            SetResourceReference(StyleProperty, typeof(Button));
            SetResourceReference(ForegroundProperty, "Brush.CaptionForeground");
            SetResourceReference(FontFamilyProperty, "Font.SystemButton");
            SetResourceReference(FontSizeProperty, "FontSize.Big");
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
        public WindowCaption()
        {
            SetResourceReference(ForegroundProperty, "Brush.CaptionForeground");
            SetResourceReference(BackgroundProperty, "Brush.CaptionBackground");
            MouseLeftButtonDown += HandleMouseDown;
            MouseDoubleClick += HandleMouseDoubleClick;

            var logo_image = new VectorIconConst()
            {
                Height = 20
            };
            logo_image.SetResourceReference(VectorIconConst.SourceProperty, "Drawing.Logo");
            LogoImage = logo_image;
            LogoImage.Margin = new Thickness(4);
            var RootPanel = new DockPanel()
            {
                LastChildFill = false
            };

            MaximizeButton = MakeMaximizeButton();
            MinimizeButton = MakeMinimizeButton();
            CloseButton = MakeCloseButton();
            RootPanel.AddChild(LogoImage, Dock.Left);
            RootPanel.AddChild(TitleControl, Dock.Left);
            RootPanel.AddChild(CloseButton, Dock.Right);
            RootPanel.AddChild(MaximizeButton, Dock.Right);
            RootPanel.AddChild(MinimizeButton, Dock.Right);
            Content = RootPanel;
        }

        private Window ParentWindow
        {
            get
            {
                return Window.GetWindow(this);
            }
        }

        public readonly UserControl TitleControl = new UserControl()
        {
            VerticalContentAlignment = VerticalAlignment.Center
        };


        private void HandleMouseDoubleClick(object sender, MouseButtonEventArgs e)
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

        private void HandleMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    Window.GetWindow(this).DragMove();
                }
            }
        }

        public VectorIconConst LogoImage;

        TitleButton MakeMaximizeButton()
        {
            var button = new TitleButton(TitleButtonContent.Maximize, TitleButtonContent.Restore);
            button.Click += (sender, e) =>
            {
                var window = Window.GetWindow(this);
                if (window.WindowState == WindowState.Normal)
                {
                    window.WindowState = WindowState.Maximized;
                }
                else
                {
                    window.WindowState = WindowState.Normal;
                }
            };
            return button;
        }

        TitleButton MakeMinimizeButton()
        {
            var button = new TitleButton(TitleButtonContent.Minimize);
            button.Click += (sender, e) =>
            {
                var window = Window.GetWindow(this);
                window.WindowState = WindowState.Minimized;
            };
            return button;
        }

        TitleButton MakeCloseButton()
        {
            var button = new TitleButton(TitleButtonContent.Close);
            button.Click += (sender, e) =>
            {
                var window = Window.GetWindow(this);
                window.Close();
            };
            return button;
        }

        TitleButton MaximizeButton;
        TitleButton MinimizeButton;
        TitleButton CloseButton = new TitleButton(TitleButtonContent.Close);

    }

}
