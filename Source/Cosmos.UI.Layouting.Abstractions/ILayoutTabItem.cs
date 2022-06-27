using Cosmos.ComponentModel.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Cosmos.UI.Layoutting.Abstractions
{
    public interface ILayoutTabItem : ILayoutSerializable
    {
        String ContentHeader { get; set; }
        IWidget NewWidget(Guid widgetGuid);
        IWidget ContentWidget { get; set; }
        XElement ILayoutSerializable.ExportConfig()
        {
            var xe = new XElement(nameof(ILayoutTabItem));
            if (ContentWidget != null)
            {
                // todo xe.Add(new XAttribute("WidgetGuid", ContentWidget.MetaData.UniqueID.ToString()));
                xe.Add(ContentWidget.ExportStatus());
            }
            return xe;
        }
        void ILayoutSerializable.ImportConfig(XElement root_element)
        {

            var widgetGuid = Guid.Parse(root_element.Attribute("WidgetGuid").Value);

            // create widget
            var widget = NewWidget(widgetGuid);
            try
            {
                widget.ImportStatus(root_element.Elements().First().Value);
            }
            catch (Exception e)
            {
                // 吃掉加载异常
            }
            //ContentHeader = widget.MetaData.DisplayName.Zh_Cn;
            ContentWidget = widget;
        }
    }
}
