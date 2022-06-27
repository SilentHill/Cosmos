
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.Linq;

namespace Cosmos.UI.Layoutting.Abstractions
{
    public class MuiString
    {
        public static MuiString FromXElement(XElement xe)
        {
            var ms = new MuiString() 
            {
                En_Us = xe.Attribute("en-us").Value,
                Zh_Cn = xe.Attribute("zh-cn").Value,
                Zh_Tw = xe.Attribute("zh-tw").Value,
                Ja_Jp = xe.Attribute("ja-jp").Value,
            };
            return ms;
        }
        public override bool Equals(object obj)
        {
            if (obj is MuiString rhs)
            {
                return En_Us == rhs.En_Us
                    && Zh_Cn == rhs.Zh_Cn
                    && Zh_Tw == rhs.Zh_Tw
                    && Ja_Jp == rhs.Ja_Jp;
            }
            else
            {
                return false;
            }
        }
        public String En_Us { get; set;} = String.Empty;
        public String Zh_Cn { get; set;} = String.Empty;
        public String Zh_Tw { get; set;} = String.Empty;
        public String Ja_Jp { get; set; } = String.Empty;
    }
}
