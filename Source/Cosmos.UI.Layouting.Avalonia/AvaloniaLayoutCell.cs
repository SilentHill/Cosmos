
using Cosmos.UI.Layoutting.Abstractions;
using Cosmos.UI.Layoutting.Avalonia.Controls;
//using Microsoft.Xaml.Behaviors.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Avalonia.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using Avalonia.Controls.Presenters;
using Avalonia;
using Avalonia.Layout;
using Avalonia.Media;

namespace Cosmos.UI.Layoutting.Avalonia
{
    public sealed class AvaloniaLayoutCell : UserControl, ILayoutCell
    {
        public ILayoutTab NewLayoutTab()
        {
            var widget_tab_control = new AvaloniaLayoutTab();
            return widget_tab_control;
        }

        private DockPanel RootPanel { get; set; } = new DockPanel()
        {
            LastChildFill = true
        };
        private ContentPresenter ContentPresenter { get; set; } = new ContentPresenter();
        internal AvaloniaWidgetContainerCaptionBar CaptionBar { get; private set; } = null;
        internal AvaloniaLayoutCell()
        {
            BorderThickness = new Thickness(0);
            Content = RootPanel;
            CaptionBar = new AvaloniaWidgetContainerCaptionBar(this);
            RootPanel.AddChild(CaptionBar, Dock.Top);
            RootPanel.AddChild(ContentPresenter, Dock.Top);
            ContextMenu = new AvaloniaWidgetContainerContextMenu(this);
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
            var grid = CellContent as AvaloniaLayoutGrid;
            if (LayoutOrientation == LayoutOrientation.Horizontal)
            {
                var length = grid.ColumnDefinitions[Grid.GetColumn(childCell as AvaloniaLayoutCell) ].Width;
                var layout_length = FromGridLength(length);
                return layout_length;
            }
            else if (LayoutOrientation == Abstractions.LayoutOrientation.Vertical)
            {
                var length = grid.RowDefinitions[Grid.GetRow(childCell as AvaloniaLayoutCell)].Height;
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
            var grid = CellContent as AvaloniaLayoutGrid;

            var grid_length = ToGridLength(childLength);

            if (LayoutOrientation == Abstractions.LayoutOrientation.Horizontal)
            {
                var column_index = Grid.GetColumn(childCell as AvaloniaLayoutCell);
                grid.ColumnDefinitions[column_index].Width = grid_length;
                return;
            }
            else if (LayoutOrientation == Abstractions.LayoutOrientation.Vertical)
            {
                var row_index = Grid.GetRow(childCell as AvaloniaLayoutCell);
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
                return (CellContent as AvaloniaLayoutGrid).Children[0] as ILayoutCell;
            }
            set
            {
                (CellContent as AvaloniaLayoutGrid).Children[0] = value as Control;
            }
        }
        public ILayoutCell LastChild
        {
            get
            {
                return (CellContent as AvaloniaLayoutGrid).Children[2] as ILayoutCell;
            }
            set
            {
                (CellContent as AvaloniaLayoutGrid).Children[2] = value as Control;
            }
        }

        private AvaloniaLayoutTab WidgetTab { get; } = new AvaloniaLayoutTab()
        {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
            HorizontalContentAlignment = HorizontalAlignment.Center,
            VerticalContentAlignment = VerticalAlignment.Center,
        };
        public ILayoutCell ParentCell { get; set; }

        private AvaloniaLayoutGrid CreateGridWithDoubleRows()
        {
            var child_grid = new AvaloniaLayoutGrid()
            {
                RowDefinitions =
                {
                    new RowDefinition() { Height = new GridLength(100, GridUnitType.Star) },
                    new RowDefinition() { Height = GridLength.Auto},
                    new RowDefinition() { Height = new GridLength(100, GridUnitType.Star) },
                }
            };
            child_grid.AddChild(new AvaloniaLayoutCell() { ParentCell = this }, 0, 0);
            child_grid.AddChild(CreateRowSplitter(), 0, 1);
            child_grid.AddChild(new AvaloniaLayoutCell() { ParentCell = this }, 0, 2);
            return child_grid;
        }
        private AvaloniaLayoutGrid CreateGridWithDoubleColumns()
        {
            var child_grid = new AvaloniaLayoutGrid()
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition() { Width = new GridLength(100, GridUnitType.Star) },
                    new ColumnDefinition() { Width = GridLength.Auto},
                    new ColumnDefinition() { Width = new GridLength(100, GridUnitType.Star) },
                }
            };
            child_grid.AddChild(new AvaloniaLayoutCell() { ParentCell = this }, 0, 0);
            child_grid.AddChild(CreateColumnSplitter(), 1, 0);
            child_grid.AddChild(new AvaloniaLayoutCell() { ParentCell = this }, 2, 0);
            return child_grid;
        }

        private GridSplitter CreateColumnSplitter()
        {
            var vertical_splitter = new GridSplitter()
            {
                Background = Brushes.DarkRed,
                Focusable = false,
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
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
            };
            return horizontal_splitter;
        }
        void Reset()
        {
            CaptionBar.IsVisible = false;
            CellContent = WidgetTab;
        }

        Control RemoveCurrentContent()
        {
            var removed_content = CellContent as Control;
            CellContent = null;
            
            LogicalChildren.Remove(removed_content);
            VisualChildren.Remove(removed_content);
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
            CaptionBar.IsVisible = false;
            RemoveCurrentContent();
            var grid = CreateGridWithDoubleColumns();
            CellContent = grid;
            FirstChild = grid.Children[0] as AvaloniaLayoutCell;
            LastChild = grid.Children[2] as AvaloniaLayoutCell;
            CellContent = grid;
        }
        public void SplitToRows()
        {
            CaptionBar.IsVisible = false;
            RemoveCurrentContent();
            var grid = CreateGridWithDoubleRows();
            CellContent = grid;
            FirstChild = grid.Children[0] as AvaloniaLayoutCell;
            LastChild = grid.Children[2] as AvaloniaLayoutCell;
            CellContent = grid;
        }

    }


}


