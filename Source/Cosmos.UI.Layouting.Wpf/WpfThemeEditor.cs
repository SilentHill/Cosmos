using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Cosmos.UI.Layoutting.Wpf
{
    public class WpfThemeEditor : UserControl
    {
        public WpfThemeEditor()
        {
            var sp = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                Children =
                {
                    new ThemeSelector(),
                    MakeLabelDemo(), 
                    MakeButtonDemo(),
                    MakeCheckBoxDemo(),
                    MakeRadioButtonDemo(),
                    MakeTextBoxDemo(),
                    MakeComboBoxDemo(),
                    MakeListBoxDemo(),
                    MakeDatePickerDemo(),
                    MakeCalendarDemo(),
                }
            };

            Content = sp;
        }

        UIElement MakeLabelDemo()
        {
            var normal_label = new Label()
            {
                Margin = new Thickness(2),
                Width = 256,
                Content = "正常",
            };

            var disabled_label = new Label()
            {
                Margin = new Thickness(2),
                Width = 256,
                Content = "禁用",
                IsEnabled = false
            };

            var sp = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Children =
                {
                   new TextBlock(){ Text = "标签", MinWidth = 64, Margin = new Thickness(4) },
                   normal_label, disabled_label,
                }
            };

            return sp;
        }
        UIElement MakeTextBoxDemo()
        {
            var width = 128;
            var normal_single_line_text_box = new TextBox()
            {
                Margin = new Thickness(2),
                Width = width,
                Text = "正常",
            };

            var disabled_single_line_text_box = new TextBox()
            {
                Margin = new Thickness(2),
                Width = width,
                Text = "禁用",
                IsEnabled = false
            };

            var normal_multiple_line_text_box = new TextBox()
            {
                Margin = new Thickness(2),
                Width = width,
                TextWrapping = TextWrapping.Wrap,
                AcceptsReturn = true,
                Text = "多行\n多行",
            };

            var disabled_multiple_line_text_box = new TextBox()
            {
                Margin = new Thickness(2),
                Width = width,
                Text = "禁用多行\n禁用多行",
                TextWrapping = TextWrapping.Wrap,
                AcceptsReturn = true,
                IsEnabled = false
            };

            var sp = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Children =
                {
                   new TextBlock(){ Text = "文本框", MinWidth = 64, Margin = new Thickness(4) },
                   normal_single_line_text_box, disabled_single_line_text_box,
                   normal_multiple_line_text_box, disabled_multiple_line_text_box
                }
            };

            return sp;
        }

        UIElement MakeButtonDemo()
        {
            Button normal_button = new Button()
            {
                Margin = new Thickness(2),
                Content = "正常"
            };

            Button disabled_button = new Button()
            {
                Margin = new Thickness(2),
                Content = "禁用",
                IsEnabled = false
            };

            var sp = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Children =
                {
                   new TextBlock(){ Text="按钮", MinWidth = 64, Margin = new Thickness(4) },
                   normal_button, disabled_button
                }
            };
            return sp;
        }



        UIElement MakeCheckBoxDemo()
        {
            CheckBox normal_check_box = new CheckBox()
            {
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(2),
                Content = "正常"
            };

            CheckBox disabled_check_box = new CheckBox()
            {
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(2),
                Content = "禁用",
                IsEnabled = false
            };

            var sp = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Children =
                {
                   new TextBlock(){ Text= "复选框", MinWidth = 64, Margin = new Thickness(4) },
                   normal_check_box, disabled_check_box
                }
            };
            return sp;
        }

        UIElement MakeRadioButtonDemo()
        {
            RadioButton normal_radio_button_0 = new RadioButton()
            {
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(2),
                Content = "正常0",
            };
            RadioButton normal_radio_button_1 = new RadioButton()
            {
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(2),
                Content = "正常1"
            };
            RadioButton disabled_radio_button = new RadioButton()
            {
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(2),
                Content = "禁用",
                IsEnabled = false
            };

            var sp = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Children =
                {
                   new TextBlock(){ Text= "单选框", MinWidth = 64, Margin = new Thickness(4) },
                   normal_radio_button_0, normal_radio_button_1, disabled_radio_button
                }
            };
            return sp;
        }
        UIElement MakeComboBoxDemo()
        {
            ComboBox normal_combo_box = new ComboBox()
            {
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(2),
                Width = 64,
                Items =
                {
                    "项目 1", "项目 2","项目 3",
                }
            };

            ComboBox disabled_combo_box = new ComboBox()
            {
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(2),
                Width = 64,
                IsEnabled = false
            };

            var sp = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Children =
                {
                   new TextBlock(){ Text= "下拉框", MinWidth = 64, Margin = new Thickness(4) },
                   normal_combo_box, disabled_combo_box
                }
            };
            return sp;
        }

        UIElement MakeListBoxDemo()
        {
            var normal_combo_box = new ListBox()
            {
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(2),
                Width = 64,
                Items =
                {
                    "项目 1", "项目 2","项目 3",
                }
            };

            var disabled_combo_box = new ListBox()
            {
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(2),
                Width = 64,
                Items =
                {
                    "项目 1", "项目 2","项目 3",
                },
                IsEnabled = false
            };

            var sp = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Children =
                {
                   new TextBlock(){ Text= "列表框", MinWidth = 64, Margin = new Thickness(4) },
                   normal_combo_box, disabled_combo_box
                }
            };
            return sp;
        }
        UIElement MakeDatePickerDemo()
        {
            var normal_date_picker = new DatePicker()
            {
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(2),
            };

            var disabled_date_picker = new DatePicker()
            {
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(2),
                IsEnabled = false,
            };

            var sp = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Children =
                {
                   new TextBlock(){ Text= "日期", MinWidth = 64, Margin = new Thickness(4) },
                   normal_date_picker, disabled_date_picker
                }
            };
            return sp;
        }
        UIElement MakeCalendarDemo()
        {
            var normal_calendar = new Calendar()
            {
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(2),
            };

            var disabled_calendar = new Calendar()
            {
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(2),
                IsEnabled = false,
            };

            var sp = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Children =
                {
                   new TextBlock(){ Text= "日历", MinWidth = 64, Margin = new Thickness(4) },
                   normal_calendar, disabled_calendar
                }
            };
            return sp;
        }
    }

    class ThemeSelector : UserControl
    {
        public ThemeSelector()
        {
            var theme_menu_button = new Button()
            {
                Content = "选择主题",
                ContextMenu = MakeThemesMenu(),
            };
            theme_menu_button.Click += (sender, e) =>
            {
                theme_menu_button.ContextMenu.Placement = PlacementMode.Bottom;
                theme_menu_button.ContextMenu.PlacementTarget = theme_menu_button;
                theme_menu_button.ContextMenu.IsOpen = true;
            };
            var root_stack_panel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Children =
                {
                    new TextBlock() { Text="主题", Margin = new Thickness(4), },
                    theme_menu_button,
                }
            };
            Content = root_stack_panel;
        }

        ContextMenu MakeThemesMenu()
        {
            var all_themes = Theme.GetThemesFromThisAssembly();
            var context_menu = new ContextMenu();
            foreach (var theme in all_themes)
            {
                var theme_item = new MenuItem()
                {
                    Header = theme.Name.Zh_Cn,
                    Tag = theme
                };
                context_menu.Items.Add(theme_item);
                foreach (var color_scheme in theme.ColorSchemes)
                {
                    var color_item = new MenuItem()
                    {
                        Header = color_scheme.UniqueName.Zh_Cn,
                        Tag = color_scheme
                    };
                    color_item.Click += (sender, e) =>
                    {
                        theme.CurrentColorScheme = color_scheme;
                        theme.ApplyTo(Application.Current.Resources);
                    };
                    theme_item.Items.Add(color_item);
                }
            }
            return context_menu;
        }
    }
}
