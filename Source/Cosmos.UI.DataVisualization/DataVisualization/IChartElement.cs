using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cosmos.Widgets.Primitives.Abstractions.DataVisualization
{
    public interface IChartElement
    {
        void PlotToCanvas(SKCanvas skCanvas, SKRect skRect);
        bool IsVisible { get; set; }
    }
}
