using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmos.Widgets.Primitives.Abstractions.DataVisualization
{
    public static class MathChartPlotter
    {
        public static void PlotSerieses(SKCanvas skCanvas, SKRect skRect, List<Series> serieses)
        {
            skCanvas.Save();
            foreach (var series in serieses)
            {
                series.PlotToCanvas(skCanvas, skRect);
            }
            skCanvas.Restore();
        }
    }
}
