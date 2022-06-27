using Cosmos.ComponentModel.Management;
using Cosmos.ComponentModel.Public;
using Cosmos.UI.Layoutting.Abstractions;
using Cosmos.UI.Layoutting.Wpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Cosmos.UI.Layoutting.Wpf
{
    public sealed class WpfLayoutTabItem : TabItem, ILayoutTabItem
    {
        public WpfEditableTabControl CellsTabControl = null;
        public readonly Label HeaderContent = new Label();
        public readonly Button CloseButton = new TitleButton(TitleButtonContent.Close)
        {
            Padding = new Thickness(6, 0, 6, 0)
        };
        public WpfLayoutTabItem()
        {
            SetResourceReference(StyleProperty, typeof(TabItem));
            DockPanel.SetDock(HeaderContent, Dock.Left);
            DockPanel.SetDock(CloseButton, Dock.Right);
            CloseButton.Click += (sender, e) =>
            {
                var next_item = CellsTabControl.CurrentNextItem() as WpfLayoutTabItem;
                Content = null;
                CellsTabControl.Items.Remove(this);

                if (next_item != null)
                {
                    next_item.IsSelected = true;
                }
                else if (CellsTabControl.LastItem() != null)
                {
                    (CellsTabControl.LastItem() as WpfLayoutTabItem).IsSelected = true;
                }
            };
            var dock_panel = new DockPanel()
            {
                Children =
                {
                    HeaderContent, CloseButton
                }
            };
            Header = dock_panel;
        }
        public IWidget NewWidget(Guid widgetGuid)
        {
            // todo
            return null;
        }
        public IWidget ContentWidget
        {
            get
            {
                return Content as IWidget;
            }
            set
            {
                Content = value;
            }
        }

        public String ContentHeader
        {
            get
            {
                return Header as String;
            }
            set
            {
                Header = value;
            }
        }
    }
}
