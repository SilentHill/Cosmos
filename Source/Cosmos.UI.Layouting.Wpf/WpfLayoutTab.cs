
using Cosmos.UI.Layoutting.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Cosmos.UI.Layoutting.Wpf.Controls
{
    public sealed class WpfLayoutTab : WpfEditableTabControl, ILayoutTab
    {
        public IList LayoutTabItems
        {
            get
            {
                return Items;
            }
        }
        public WpfLayoutTab()
        {
            BorderThickness = new Thickness(0);
        }
        public ILayoutTabItem NewLayoutTabItem()
        {
            var tab_item = new WpfLayoutTabItem();
            return tab_item;
        }

    }
}
