using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Cosmos.UI.Layoutting.Abstractions
{
    public interface ILayoutSerializable
    {
        XElement ExportConfig();
        void ImportConfig(XElement rootElement);
    }

}
