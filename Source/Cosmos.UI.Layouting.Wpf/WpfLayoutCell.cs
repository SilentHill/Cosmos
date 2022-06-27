
using Cosmos.UI.Layoutting.Abstractions;
using Cosmos.UI.Layoutting.Wpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;

namespace Cosmos.UI.Layoutting.Wpf
{
    public sealed class WpfLayoutCell : UserControl, ILayoutCell
    {
        public ILayoutTab NewLayoutTab()
        {
            var widget_tab_control = new WpfLayoutTab();
            return widget_tab_control;
        }

        private DockPanel RootPanel { get; set; } = new DockPanel()
        {
            LastChildFill = true
        };
        private ContentPresenter ContentPresenter { get; set; } = new ContentPresenter();
        internal WpfWidgetContainerCaptionBar CaptionBar { get; private set; } = null;
        internal WpfLayoutCell()
        {
            BorderThickness = new Thickness(0);
            Content = RootPanel;
            CaptionBar = new WpfWidgetContainerCaptionBar(this);
            CaptionBar.Visibility = Visibility.Collapsed;
            RootPanel.AddChild(CaptionBar, Dock.Top);
            RootPanel.AddChild(ContentPresenter, Dock.Top);
            ContextMenu = new WpfWidgetContainerContextMenu(this);
            Reset();
        }

        public static GridLength ToGridLength(LayoutCellLength cellLength)
        {
            GridUnitType gl = GridUnitType.Auto;
            switch (cellLength.LayoutCellUnitType)
            {
                case LayoutCellUnitType.Auto:
                    gl = GridUnitType.Auto;
                    break;
                case LayoutCellUnitType.Pixel:
                    gl = GridUnitType.Pixel;
                    break;
                case LayoutCellUnitType.Star:
                    gl = GridUnitType.Star;
                    break;
                default:
                    gl = GridUnitType.Auto;
                    break;
            };
            var layout_cell_length = new GridLength(cellLength.Value, gl);
            return layout_cell_length;
        }
        public static LayoutCellLength FromGridLength(GridLength gridLength)
        {
            LayoutCellUnitType lcut = LayoutCellUnitType.Auto;
            switch (gridLength.GridUnitType)
            {
                case GridUnitType.Auto:
                    lcut = LayoutCellUnitType.Auto;
                    break;
                case GridUnitType.Pixel:
                    lcut = LayoutCellUnitType.Pixel;
                    break;
                case GridUnitType.Star:
                    lcut = LayoutCellUnitType.Star;
                    break;
                default:
                    lcut = LayoutCellUnitType.Auto;
                    break;
            };
            var layout_cell_length = new LayoutCellLength(gridLength.Value, lcut);
            return layout_cell_length;
        }
        public LayoutCellLength GetChildCellLength(ILayoutCell childCell)
        {
            var grid = CellContent as WpfLayoutGrid;
            if (LayoutOrientation == LayoutOrientation.Horizontal)
            {
                var length = grid.ColumnDefinitions[Grid.GetColumn(childCell as WpfLayoutCell) ].Width;
                var layout_length = FromGridLength(length);
                return layout_length;
            }
            else if (LayoutOrientation == Abstractions.LayoutOrientation.Vertical)
            {
                var length = grid.RowDefinitions[Grid.GetRow(childCell as WpfLayoutCell)].Height;
                var layout_length = FromGridLength(length);
                return layout_length;
            }
            else
            {
                return LayoutCellLength.BadLength;
            }
        }
        public void SetChildCellLength(ILayoutCell childCell, LayoutCellLength childLength)
        {
            var grid = CellContent as WpfLayoutGrid;

            var grid_length = ToGridLength(childLength);

            if (LayoutOrientation == Abstractions.LayoutOrientation.Horizontal)
            {
                var column_index = Grid.GetColumn(childCell as WpfLayoutCell);
                grid.ColumnDefinitions[column_index].Width = grid_length;
                return;
            }
            else if (LayoutOrientation == Abstractions.LayoutOrientation.Vertical)
            {
                var row_index = Grid.GetRow(childCell as WpfLayoutCell);
                grid.RowDefinitions[row_index].Height = grid_length;
                return;
            }
            else
            {
                return;
            }
        }

        public ILayoutCellContent CellContent
        {
            get
            {
                return ContentPresenter.Content as ILayoutCellContent;
            }
            set
            {
                ContentPresenter.Content = value;
            }
        }
        internal String LayoutName { get; set; } = String.Empty;
        public LayoutOrientation LayoutOrientation
        {
            get
            {
                if (CellContent is Grid grid)
                {
                    if (grid.ColumnDefinitions.Count == 3)
                    {
                        return Abstractions.LayoutOrientation.Horizontal;
                    }
                    else if (grid.RowDefinitions.Count == 3)
                    {
                        return Abstractions.LayoutOrientation.Vertical;
                    }
                }
                return Abstractions.LayoutOrientation.ContentOnly;
            }
            set
            {
                return;
            }
        }

        public ILayoutCell FirstChild
        {
            get
            {
                return (CellContent as WpfLayoutGrid).Children[0] as ILayoutCell;
            }
            set
            {
                (CellContent as WpfLayoutGrid).Children[0] = value as UIElement;
            }
        }
        public ILayoutCell LastChild
        {
            get
            {
                return (CellContent as WpfLayoutGrid).Children[2] as ILayoutCell;
            }
            set
            {
                (CellContent as WpfLayoutGrid).Children[2] = value as UIElement;
            }
        }

        private WpfLayoutTab WidgetTab { get; } = new WpfLayoutTab()
        {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
            HorizontalContentAlignment = HorizontalAlignment.Center,
            VerticalContentAlignment = VerticalAlignment.Center,
        };
        public ILayoutCell ParentCell { get; set; }

        private WpfLayoutGrid CreateGridWithDoubleRows()
        {
            var child_grid = new WpfLayoutGrid()
            {
                RowDefinitions =
                {
                    new RowDefinition() { Height = new GridLength(100, GridUnitType.Star) },
                    new RowDefinition() { Height = GridLength.Auto},
                    new RowDefinition() { Height = new GridLength(100, GridUnitType.Star) },
                }
            };
            child_grid.AddChild(new WpfLayoutCell() { ParentCell = this }, 0, 0);
            child_grid.AddChild(CreateRowSplitter(), 0, 1);
            child_grid.AddChild(new WpfLayoutCell() { ParentCell = this }, 0, 2);
            return child_grid;
        }
        private WpfLayoutGrid CreateGridWithDoubleColumns()
        {
            var child_grid = new WpfLayoutGrid()
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition() { Width = new GridLength(100, GridUnitType.Star) },
                    new ColumnDefinition() { Width = GridLength.Auto},
                    new ColumnDefinition() { Width = new GridLength(100, GridUnitType.Star) },
                }
            };
            child_grid.AddChild(new WpfLayoutCell() { ParentCell = this }, 0, 0);
            child_grid.AddChild(CreateColumnSplitter(), 1, 0);
            child_grid.AddChild(new WpfLayoutCell() { ParentCell = this }, 2, 0);
            return child_grid;
        }

        private GridSplitter CreateColumnSplitter()
        {
            var vertical_splitter = new GridSplitter()
            {
                Background = Brushes.DarkRed,
                Focusable = false,
                Width = 4,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Stretch,
            };
            return vertical_splitter;
        }
        private GridSplitter CreateRowSplitter()
        {
            var horizontal_splitter = new GridSplitter()
            {
                Background = Brushes.DarkRed,
                Focusable = false,
                Height = 4,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
            };
            return horizontal_splitter;
        }
        void Reset()
        {
            //CaptionBar.Visibility = Visibility.Visible;
            CellContent = WidgetTab;
        }

        UIElement RemoveCurrentContent()
        {
            var removed_content = CellContent as UIElement;
            CellContent = null;
            RemoveLogicalChild(removed_content);
            RemoveVisualChild(removed_content);
            return removed_content;
        }


        internal void InsertRight()
        {
            var content = CellContent;
            SplitToColumns();
            FirstChild.CellContent = content;
        }
        internal void InsertLeft()
        {
            var content = CellContent;
            SplitToColumns();
            LastChild.CellContent = content;
        }
        internal void InsertTop()
        {
            var content = CellContent;
            SplitToRows();
            LastChild.CellContent = content;
        }
        internal void InsertBottom()
        {
            var content = CellContent;
            SplitToRows();
            FirstChild.CellContent = content;
        }
        public void SplitToColumns()
        {
            CaptionBar.Visibility = Visibility.Collapsed;
            RemoveCurrentContent();
            var grid = CreateGridWithDoubleColumns();
            CellContent = grid;
            FirstChild = grid.Children[0] as WpfLayoutCell;
            LastChild = grid.Children[2] as WpfLayoutCell;
            CellContent = grid;
        }
        public void SplitToRows()
        {
            CaptionBar.Visibility = Visibility.Collapsed;
            RemoveCurrentContent();
            var grid = CreateGridWithDoubleRows();
            CellContent = grid;
            FirstChild = grid.Children[0] as WpfLayoutCell;
            LastChild = grid.Children[2] as WpfLayoutCell;
            CellContent = grid;
        }

    }


}


