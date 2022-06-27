
using Cosmos.UI.Layoutting.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Avalonia.Controls;
using System.Xml.Linq;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Styling;
using System.Collections;

namespace Cosmos.UI.Layoutting.Avalonia.Controls
{
    public sealed class AvaloniaLayoutTab : TabControl, ILayoutTab, IStyleable
    {
        Type IStyleable.StyleKey
        {
            get
            {
                return typeof(TabControl);
            }
        }

        public IList LayoutTabItems
        {
            get
            {
                return Items as IList;
            }
        }
        public AvaloniaLayoutTab()
        {
            Items = new AvaloniaList<ILayoutTabItem>();
        }
        public ILayoutTabItem NewLayoutTabItem()
        {
            var tab_item = new AvaloniaLayoutTabItem();
            return tab_item;
        }
        public AvaloniaLayoutTabItem AddCosmosTabItem(Object header, Object content, bool can_close = false)
        {
            var tag = new AvaloniaLayoutTabItem()
            {
                Content = content,
                CellsTabControl = this,
            };

            tag.HeaderContent.Content = header;
            if (!can_close)
            {
                tag.CloseButton.IsVisible = false;
            }
            LayoutTabItems.Add(tag);
            return tag;
        }

        public object LastItem()
        {
            if (LayoutTabItems.Count == 0)
            {
                return null;
            }
            return LayoutTabItems[LayoutTabItems.Count - 1];
        }

        public object CurrentNextItem()
        {
            if (SelectedIndex < LayoutTabItems.Count - 1)
            {
                return LayoutTabItems[SelectedIndex + 1] as AvaloniaLayoutTabItem;
            }
            else
            {
                return null;
            }
        }

    }
}
