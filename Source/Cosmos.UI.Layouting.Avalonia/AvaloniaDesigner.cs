
using Cosmos.UI.Layoutting.Abstractions;
using Cosmos.UI.Layoutting.Avalonia.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Avalonia.Controls;
using System.Xml.Linq;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia;

namespace Cosmos.UI.Layoutting.Avalonia
{
    public class AvaloniaDesigner : UserControl, IDesigner
    {
        private AvaloniaLayoutTab LayoutTab { get; }

        public AvaloniaDesigner(Window parentWindow)
        {
            LayoutTab = new AvaloniaLayoutTab()
            {

            };
            CreateNewLayout();
            var dock_panel = new DockPanel()
            {
                LastChildFill = true
            };
            dock_panel.AddChild(MakeMenuLayout(parentWindow), Dock.Top);
            dock_panel.AddChild(LayoutTab, Dock.Bottom);
            Content = dock_panel;
        }

        AvaloniaLayoutTabItem CreateNewLayout()
        {
            var tab_item = LayoutTab.AddCosmosTabItem("新页面", new AvaloniaLayoutCell()
            {
                BorderBrush = Brushes.DarkRed,
                BorderThickness = new Thickness(4),
                LayoutName = "新页面"
            }, true);
            tab_item.IsSelected = true;
            return tab_item;
        }
        AvaloniaLayoutCell CurrentCell
        {
            get
            {
                return LayoutTab.SelectedItem as AvaloniaLayoutCell;
            }
        }

        void SaveFile(String file_path)
        {
            var root_node = new XElement("CosmosLayout");
            foreach (AvaloniaLayoutTabItem item in LayoutTab.LayoutTabItems)
            {
                var cell_node = (item.Content as ILayoutSerializable).ExportConfig();
                root_node.Add(cell_node);
            }
            root_node.Save(file_path);
        }

        void LoadFile(String file_path)
        {
            LayoutTab.LayoutTabItems.Clear();
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
        MenuItem MakeMenuItemSaveAs(Window parentWindow)
        {
            var menu_item = new MenuItem()
            {
                Header = "另存为..."
            };

            menu_item.Click += async (sender, e) =>
            {
                var sfd = new SaveFileDialog();
                var path = await sfd.ShowAsync(parentWindow);
                if (String.IsNullOrEmpty(path))
                {
                    return;
                }
                SaveFile(path);
            };
            return menu_item;
        }
        MenuItem MakeMenuItemLoad(Window parentWindow)
        {
            var menu_item = new MenuItem()
            {
                Header = "打开..."
            };

            menu_item.Click += async (sender, e) =>
            {
                var ofd = new OpenFileDialog();
                var paths = await ofd.ShowAsync(parentWindow);

                if (paths == null || paths.Length == 0)
                {
                    return;
                }
                LoadFile(paths[0]);
            };
            return menu_item;
        }

        Menu MakeMenuLayout(Window parentWindow)
        {
            var title_item = new MenuItem()
            {
                Header = "布局",
                Items = new List<MenuItem>
                {
                    MakeMenuItemNew(),
                    MakeMenuItemSaveAs(parentWindow),
                    MakeMenuItemLoad(parentWindow),
                }
            };
            var menu = new Menu()
            {
                Items = new List<MenuItem>
                {
                    title_item
                }
            };
            return menu;
        }

    }
}
