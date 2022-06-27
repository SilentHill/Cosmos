
using Cosmos.ComponentModel.Management;
using Cosmos.UI.Layoutting.Abstractions;
using Cosmos.UI.Layoutting.Wpf.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Cosmos.UI.Layoutting.Wpf
{
    internal class WpfWidgetContainerContextMenu : ContextMenu
    {
        internal WpfWidgetContainerContextMenu(WpfLayoutCell Cell)
        {
            this.Cell = Cell;
            SetResourceReference(StyleProperty, typeof(ContextMenu));
            Items.Add(MakeInsertLeftItem());
            Items.Add(MakeInsertRightItem());
            Items.Add(MakeInsertTopItem());
            Items.Add(MakeInsertBottomItem());
            Items.Add(MakeModuleWidgetMenuItems());
            Items.Add(MakeCodeEditorItem());
        }
        private WpfLayoutCell Cell { get; set; }


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
            var menu_item = new MenuItem()
            {
                Header = "堆叠",
            };
            // todo
            //foreach (var module_meta_data in LocalPackageHost.Current.LoadedModuleMetaDataCollection)
            //{
            //    var module_item = new MenuItem()
            //    {
            //        Header = module_meta_data.DisplayName.Zh_Cn,
            //        Tag = module_meta_data
            //    };
            //    foreach (var widget_meta_data in module_meta_data.WidgetMetaDatas)
            //    {
            //        var widget_item = new MenuItem()
            //        {
            //            Header = widget_meta_data.DisplayName.Zh_Cn,
            //            Tag = widget_meta_data
            //        };
            //        widget_item.Click += (sender, e) =>
            //        {
            //            if (!(Cell.CellContent is WpfLayoutTab))
            //            {
            //                Cell.CellContent = new WpfLayoutTab();
            //            }
            //            var tab = Cell.CellContent as WpfLayoutTab;
            //
            //            var widget = widget_meta_data.CreateWidget();
            //            // todo widget.WidgetName = widget_meta_data.DisplayName.Zh_Cn;
            //            var tab_item = new WpfLayoutTabItem()
            //            {
            //                // todo Header = widget.WidgetName,
            //                Content = widget
            //            };
            //            tab.Items.Add(tab_item);
            //            if (tab.Items.Count == 1)
            //            {
            //                tab.SelectedItem = tab_item;
            //            }
            //        };
            //        module_item.Items.Add(widget_item);
            //    }
            //    menu_item.Items.Add(module_item);
            //
            //}
            return menu_item;
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
