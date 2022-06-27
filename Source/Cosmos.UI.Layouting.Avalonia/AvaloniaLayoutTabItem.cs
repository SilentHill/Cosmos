using Cosmos.UI.Layoutting.Abstractions;
using Cosmos.UI.Layoutting.Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Avalonia.Controls;
using Avalonia;
using Avalonia.Styling;
using Cosmos.ComponentModel.Public;

namespace Cosmos.UI.Layoutting.Avalonia
{
    public sealed class AvaloniaLayoutTabItem : TabItem, ILayoutTabItem, IStyleable
    {
        public AvaloniaLayoutTab CellsTabControl { get; set; } = null;
        public Label HeaderContent { get; } = new Label();
        public Button CloseButton { get; } = new TitleButton(TitleButtonContent.Close)
        {
            Padding = new Thickness(6, 0, 6, 0)
        };
        Type IStyleable.StyleKey
        {
            get
            {
                return typeof(TabItem);
            }
        }
        public AvaloniaLayoutTabItem()
        {
            // SetResourceReference(StyleProperty, typeof(TabItem));
            DockPanel.SetDock(HeaderContent, Dock.Left);
            DockPanel.SetDock(CloseButton, Dock.Right);
            CloseButton.Click += (sender, e) =>
            {
                var next_item = CellsTabControl.CurrentNextItem() as AvaloniaLayoutTabItem;
                Content = null;
                CellsTabControl.LayoutTabItems.Remove(this);

                if (next_item != null)
                {
                    next_item.IsSelected = true;
                }
                else if (CellsTabControl.LastItem() != null)
                {
                    (CellsTabControl.LastItem() as AvaloniaLayoutTabItem).IsSelected = true;
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
            return ClientPackageHost.Current.CreateWidgetFromGuid(widgetGuid);
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
