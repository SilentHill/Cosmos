using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Cosmos.UI.Layoutting.Wpf.Controls
{
    public class WpfEditableTabControl : TabControl
    {
        public WpfEditableTabControl()
        {
            SetResourceReference(StyleProperty, typeof(TabControl));
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            (GetTemplateChild("CosmosHeaderLeftControl") as UserControl).Content = HeaderLeftPanel;
            (GetTemplateChild("CosmosHeaderRightControl") as UserControl).Content = HeaderRightPanel;

            if (!IsHeaderPanelVisible)
            {
                IsHeaderPanelVisible = false;
            }
        }

        public DockPanel HeaderRightPanel { get; } = new DockPanel()
        {
            LastChildFill = false
        };

        public DockPanel HeaderLeftPanel { get; } = new DockPanel()
        {
            LastChildFill = false
        };
        public WpfLayoutTabItem AddCosmosTabItem(Object header, Object content, bool can_close = false)
        {
            var tag = new WpfLayoutTabItem()
            {
                Content = content,
                CellsTabControl = this,
            };

            tag.HeaderContent.Content = header;
            if (!can_close)
            {
                tag.CloseButton.Visibility = Visibility.Collapsed;
            }
            Items.Add(tag);
            return tag;
        }

        public object LastItem()
        {
            if (Items.Count == 0)
            {
                return null;
            }
            return Items[Items.Count - 1];
        }

        public object CurrentNextItem()
        {
            if (SelectedIndex < Items.Count - 1)
            {
                return Items[SelectedIndex + 1] as WpfLayoutTabItem;
            }
            else
            {
                return null;
            }
        }

        protected bool _IsHeaderPanelVisible = true;
        public bool IsHeaderPanelVisible
        {
            get
            {
                return _IsHeaderPanelVisible;
            }
            set
            {
                _IsHeaderPanelVisible = value;
                var header_panel = GetTemplateChild("headerPanel") as TabPanel;
                if (header_panel != null)
                {
                    if (value == false)
                    {
                        header_panel.Height = 0;
                    }
                    else
                    {
                        header_panel.Height = Double.NaN;
                    }
                }
            }
        }

    }

}
