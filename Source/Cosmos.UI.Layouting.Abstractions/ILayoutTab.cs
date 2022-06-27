using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Linq;

namespace Cosmos.UI.Layoutting.Abstractions
{
    public interface ILayoutTab : ILayoutCellContent, ILayoutSerializable
    {
        IList LayoutTabItems { get; }
        ILayoutTabItem NewLayoutTabItem();
        XElement ILayoutSerializable.ExportConfig()
        {
            var xe = new XElement(nameof(ILayoutTab));

            if (LayoutTabItems != null)
            {
                foreach (ILayoutTabItem layout_tab_item in LayoutTabItems)
                {
                    xe.Add(layout_tab_item.ExportConfig());
                }
            }
            return xe;
        }
        void ILayoutSerializable.ImportConfig(XElement root_element)
        {
            foreach (var xe in root_element.Elements())
            {
                var tab_item = NewLayoutTabItem();
                tab_item.ImportConfig(xe);
                LayoutTabItems.Add(tab_item);
            }
        }
    }
}
