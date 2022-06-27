using Cosmos.UI.Layoutting.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Avalonia.Controls;
using Avalonia.Styling;

namespace Cosmos.UI.Layoutting.Avalonia
{
    public class AvaloniaLayoutGrid : Grid, ILayoutCellContent, IStyleable
    {
        Type IStyleable.StyleKey
        {
            get
            {
                return typeof(Grid);
            }
        }
        public AvaloniaLayoutGrid()
        {
            
        }
    }
}
