
using Cosmos.UI.Layoutting.Abstractions;
using Cosmos.UI.Layoutting.Wpf.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace Cosmos.UI.Layoutting.Wpf
{
    public class WpfDesigner : UserControl, IDesigner
    {
        private WpfEditableTabControl LayoutTab { get; } = new WpfEditableTabControl();
        public WpfDesigner()
        {
            CreateNewLayout();
            var dock_panel = new DockPanel()
            {
                LastChildFill = true
            };
            dock_panel.AddChild(MakeMenuLayout(), Dock.Top);
            dock_panel.AddChild(LayoutTab, Dock.Bottom);
            Content = dock_panel;
        }

        WpfLayoutTabItem CreateNewLayout()
        {
            var tab_item = LayoutTab.AddCosmosTabItem("新页面", new WpfLayoutCell()
            {
                BorderBrush = Brushes.DarkRed,
                BorderThickness = new Thickness(4),
                LayoutName = "新页面"
            }, true);
            tab_item.IsSelected = true;
            return tab_item;
        }
        WpfLayoutCell CurrentCell
        {
            get
            {
                return LayoutTab.SelectedItem as WpfLayoutCell;
            }
        }

        void SaveFile(String file_path)
        {
            var root_node = new XElement("CosmosLayout");
            foreach (WpfLayoutTabItem item in LayoutTab.Items)
            {
                var cell_node = (item.Content as ILayoutSerializable).ExportConfig();
                root_node.Add(cell_node);
            }
            root_node.Save(file_path);
        }

        void LoadFile(String file_path)
        {
            LayoutTab.Items.Clear();
            var root_node = XElement.Load(file_path);
            var cells = root_node.Elements(nameof(ILayoutCell));
            foreach (var cell in cells)
            {
                var tab_item = CreateNewLayout();
                (tab_item.Content as ILayoutSerializable).ImportConfig(cell);
            }
        }
        MenuItem MakeMenuItemNew()
        {
            var menu_item = new MenuItem()
            {
                Header = "新建页面"
            };

            menu_item.Click += (sender, e) =>
            {
                CreateNewLayout();
            };
            return menu_item;
        }
        MenuItem MakeMenuItemSaveAs()
        {
            var menu_item = new MenuItem()
            {
                Header = "另存为..."
            };

            menu_item.Click += (sender, e) =>
            {
                var sfd = new SaveFileDialog();
                sfd.ShowDialog();
                if (String.IsNullOrEmpty(sfd.FileName))
                {
                    return;
                }
                SaveFile(sfd.FileName);
            };
            return menu_item;
        }
        MenuItem MakeMenuItemLoad()
        {
            var menu_item = new MenuItem()
            {
                Header = "打开..."
            };

            menu_item.Click += (sender, e) =>
            {
                var ofd = new OpenFileDialog();
                ofd.ShowDialog();
                if (String.IsNullOrEmpty(ofd.FileName))
                {
                    return;
                }
                LoadFile(ofd.FileName);
            };
            return menu_item;
        }
        MenuItem MakeMenuItemThemeEditor()
        {
            var menu_item = new MenuItem()
            {
                Header = "主题编辑器"
            };

            menu_item.Click += (sender, e) =>
            {
                var theme_editor = new WpfThemeEditor();
                var win = new WpfWindow()
                {
                    Content = theme_editor,
                    SizeToContent = SizeToContent.WidthAndHeight
                };
                win.ShowDialog();
            };
            return menu_item;
        }
        Menu MakeMenuLayout()
        {
            var title_item = new MenuItem()
            {
                Header = "布局",
                Items =
                {
                    MakeMenuItemNew(),
                    MakeMenuItemSaveAs(),
                    MakeMenuItemLoad(),
                    MakeMenuItemThemeEditor(),
                }
            };
            var menu = new Menu()
            {
                Items =
                {
                    title_item
                }
            };
            return menu;
        }

    }
}
