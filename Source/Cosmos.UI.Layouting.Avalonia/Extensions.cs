using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Avalonia.Controls;

namespace Cosmos.UI.Layoutting.Avalonia
{
    public static class DockPanelExtension
    {
        public static void AddChild(this DockPanel dock_panel, Control element, Dock dock)
        {
            DockPanel.SetDock(element, dock);
            dock_panel.Children.Add(element);
        }
    }
    public static class GridExtension
    {
        public static RowDefinition InsertRow(this Grid grid, int row_index)
        {
            var row = new RowDefinition();
            grid.RowDefinitions.Insert(row_index, row);
            return row;
        }
        public static int GetRowIndex(this Grid grid, RowDefinition row)
        {
            return grid.RowDefinitions.IndexOf(row);
        }
        public static RowDefinition RowAt(this Grid grid, int row_index)
        {
            return grid.RowDefinitions[row_index];
        }
        public static ColumnDefinition InsertColumn(this Grid grid, int column_index)
        {
            var column = new ColumnDefinition();
            grid.ColumnDefinitions.Insert(column_index, column);
            return column;
        }
        public static int GetColumnIndex(this Grid grid, ColumnDefinition columnn)
        {
            return grid.ColumnDefinitions.IndexOf(columnn);
        }
        public static ColumnDefinition ColumnAt(this Grid grid, int colomn_index)
        {
            return grid.ColumnDefinitions[colomn_index];
        }
        public static void AddColumnsRows(this Grid grid, int column_count, int row_count)
        {
            grid.AddColumns(column_count);
            grid.AddRows(row_count);
        }
        public static void AddColumns(this Grid grid, int count = 1)
        {
            for (int i = 0; i < count; ++i)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }
        public static void AddRows(this Grid grid, int count = 1)
        {
            for (int i = 0; i < count; ++i)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
        }

        public static void RemoveChild(this Grid grid, Int32 column_index, Int32 row_index)
        {
            foreach (Control child in grid.Children)
            {
                var child_column = Grid.GetColumn(child);
                var child_row = Grid.GetRow(child);
                if (child_column == column_index
                    && child_row == row_index)
                {
                    grid.Children.Remove(child);
                    return;
                }
            }

        }
        public static void AddChild(this Grid grid, Control element, Int32 column_index, Int32 column_span, Int32 row_index, Int32 row_span)
        {
            grid.Children.Add(element);
            grid.SetChildColumnRow(element, column_index, row_index);
            grid.SetChildColumnRowSpan(element, column_span, row_span);
        }
        public static void AddChild(this Grid grid, Control element, Int32 column_index, Int32 row_index)
        {
            grid.AddChild(element, column_index, 1, row_index, 1);
        }
        public static void SetChildColumnRowSpan(this Grid grid, Control element, Int32 column_span, Int32 row_span)
        {
            Grid.SetColumnSpan(element, column_span);
            Grid.SetRowSpan(element, row_span);
        }
        public static void SetChildColumnRow(this Grid grid, Control element, Int32 column_index, Int32 row_index)
        {
            Grid.SetColumn(element, column_index);
            Grid.SetRow(element, row_index);
        }
    }
}
