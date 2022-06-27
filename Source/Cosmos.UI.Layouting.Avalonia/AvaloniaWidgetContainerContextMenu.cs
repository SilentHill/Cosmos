
using Cosmos.UI.Layoutting.Avalonia.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Avalonia.Controls;
using Cosmos.UI.Layoutting.Abstractions;
using Avalonia.Styling;
using Avalonia.Collections;
using System.Diagnostics;

namespace Cosmos.UI.Layoutting.Avalonia
{
    internal class AvaloniaWidgetContainerContextMenu : ContextMenu, IStyleable
    {
        Type IStyleable.StyleKey
        {
            get
            {
                return typeof(ContextMenu);
            }
        }
        internal AvaloniaWidgetContainerContextMenu(AvaloniaLayoutCell Cell)
        {
            this.Cell = Cell;
            // SetResourceReference(StyleProperty, typeof(ContextMenu));

            Items = new List<MenuItem>
            {
                MakeInsertLeftItem(),
                MakeInsertRightItem(),
                MakeInsertTopItem(),
                MakeInsertBottomItem(),
                MakeModuleWidgetMenuItems(),
                MakeCodeEditorItem(),
            };
        }
        private AvaloniaLayoutCell Cell { get; set; }


        private MenuItem MakeCodeEditorItem()
        {
            var menu_item = new MenuItem()
            {
                Header = "编辑脚本",
            };
            menu_item.Click += (sender, e) =>
            {
                //Cell.ScriptIDE.Visibility = Visibility.Visible;
            };
            return menu_item;
        }
        private MenuItem MakeModuleWidgetMenuItems()
        {
            var menu = new MenuItem();
            var root_menu_item = new MenuItem()
            {
                Header = "堆叠",
                Items = new AvaloniaList<MenuItem>()
            };

            foreach (var module_meta_data in ClientPackageHost.Current.LocalModules)
            {
                var module_menu_item = new MenuItem()
                {
                    Header = module_meta_data.DisplayName.Zh_Cn,
                    Tag = module_meta_data,
                    Items = new AvaloniaList<MenuItem>()
                };

                foreach (var widget_meta_data in module_meta_data.WidgetMetaDatas)
                {
                    var widget_item = new MenuItem()
                    {
                        Header = widget_meta_data.DisplayName.Zh_Cn,
                        Tag = widget_meta_data
                    };
                    widget_item.Click += (sender, e) =>
                    {
                        if (!(Cell.CellContent is AvaloniaLayoutTab))
                        {
                            Cell.CellContent = new AvaloniaLayoutTab()
                            {
                                
                            };
                        }
                        var tab = Cell.CellContent as AvaloniaLayoutTab;
                        
                        var widget = widget_meta_data.CreateWidget();
                        widget.WidgetName = widget_meta_data.DisplayName.Zh_Cn;
                        var tab_item = new AvaloniaLayoutTabItem()
                        {
                            Header = widget.WidgetName,
                            Content = widget
                        };
                        tab.LayoutTabItems.Add(tab_item);
                        if (tab.LayoutTabItems.Count == 1)
                        {
                            tab.SelectedItem = tab_item;
                        }
                    };
                    (module_menu_item.Items as AvaloniaList<MenuItem>).Add(widget_item);
                }
                (root_menu_item.Items as AvaloniaList<MenuItem>).Add(module_menu_item);


            }
            return root_menu_item;
        }
        private MenuItem MakeInsertLeftItem()
        {
            var menu_item = new MenuItem()
            {
                Header = "左插入",
            };
            menu_item.Click += (sender, e) =>
            {
                Cell.InsertLeft();
            };
            return menu_item;
        }
        private MenuItem MakeInsertRightItem()
        {
            var menu_item = new MenuItem()
            {
                Header = "右插入",
            };
            menu_item.Click += (sender, e) =>
            {
                Cell.InsertRight();
            };
            return menu_item;
        }
        private MenuItem MakeInsertTopItem()
        {
            var menu_item = new MenuItem()
            {
                Header = "上插入",
            };
            menu_item.Click += (sender, e) =>
            {
                Cell.InsertTop();
            };
            return menu_item;
        }
        private MenuItem MakeInsertBottomItem()
        {
            var menu_item = new MenuItem()
            {
                Header = "下插入",
            };
            menu_item.Click += (sender, e) =>
            {
                Cell.InsertBottom();
            };
            return menu_item;
        }
    }
}
