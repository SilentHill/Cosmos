using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Cosmos.UI.Layoutting.Abstractions
{
    public enum LayoutOrientation
    {
        Vertical,
        Horizontal,
        ContentOnly
    }
    public interface ILayoutCell : ILayoutSerializable
    {
        ILayoutCell ParentCell { get; set; }
        ILayoutCell FirstChild { get; set; }
        ILayoutCell LastChild { get; set; }
        ILayoutCellContent CellContent { get; set; }
        LayoutOrientation LayoutOrientation { get; set; }
        ILayoutTab NewLayoutTab();
        void SplitToColumns();
        void SplitToRows();
        LayoutCellLength GetChildCellLength(ILayoutCell childCell);
        void SetChildCellLength(ILayoutCell childCell, LayoutCellLength childLength);
        LayoutCellLength CellLengthInParent 
        {
            get
            {
                if (ParentCell == null)
                {
                    return LayoutCellLength.BadLength;
                }
                var layout_cell_length = ParentCell.GetChildCellLength(this);
                return layout_cell_length;
            }
            set
            {
                if (ParentCell == null)
                {
                    return;
                }
                ParentCell.SetChildCellLength(this, value);
            }
        }
        XElement ILayoutSerializable.ExportConfig()
        {
            var xe = new XElement(nameof(ILayoutCell),
                        new XAttribute(nameof(LayoutOrientation), LayoutOrientation),
                        new XAttribute(nameof(LayoutCellLength), CellLengthInParent)
                        );
            if (LayoutOrientation == LayoutOrientation.ContentOnly)
            {
                if (CellContent is ILayoutTab layout_tab)
                {
                    var config = layout_tab.ExportConfig();
                    xe.Add(config);
                }
            }
            else
            {
                xe.Add(FirstChild.ExportConfig());
                xe.Add(LastChild.ExportConfig());
            }
            return xe;
        }
        void ILayoutSerializable.ImportConfig(XElement rootElement)
        {
            LayoutOrientation orientation = (LayoutOrientation)Enum.Parse(typeof(LayoutOrientation),
                rootElement.Attribute(nameof(LayoutOrientation)).Value);

            if (orientation == Abstractions.LayoutOrientation.ContentOnly)
            {
                if (rootElement.HasElements)
                {
                    var cell_selector = NewLayoutTab();
                    var cell_selector_config = rootElement.Element(nameof(ILayoutTab));
                    cell_selector.ImportConfig(cell_selector_config);
                    CellContent = cell_selector;
                }
                else
                {
                    // 若没有内容则不创建
                }
            }
            else
            {
                if (orientation == Abstractions.LayoutOrientation.Horizontal)
                {
                    SplitToColumns();
                }
                else if (orientation == Abstractions.LayoutOrientation.Vertical)
                {
                    SplitToRows();
                }

                var child_xes = rootElement.Elements().ToList();
                FirstChild.ImportConfig(child_xes[0]);
                LastChild.ImportConfig(child_xes[1]);
            }
            if (ParentCell != null && ParentCell.CellContent is ILayoutCellContent parent_cell_content)
            {
                var layout_cell_length = rootElement.Attribute(nameof(LayoutCellLength)).Value; 
                var length_in_parent = LayoutCellLength.FromString(layout_cell_length);
                ParentCell.LayoutOrientation = orientation;
                CellLengthInParent = length_in_parent;
            }
        }
    }
}
