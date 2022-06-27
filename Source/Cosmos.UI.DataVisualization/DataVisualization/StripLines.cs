
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Cosmos.Widgets.Primitives.Abstractions.DataVisualization
{
    public class StripLines : IChartElement
    {
        public StripLines()
        {
            
        }
        public bool IsVisible { get; set; } = true;

        public void PlotToCanvas(SKCanvas skCanvas, SKRect skRect)
        {
            using var sk_paint = new SKPaint();
            sk_paint.Style = SKPaintStyle.Stroke;
            sk_paint.Color = SKColors.DarkRed;
            sk_paint.StrokeWidth = 1;

            var v_line_start_points = SKPointExtensions.CaculateAveragePoints(skRect.TopLeft(), skRect.TopRight(), ColumnCount).Select(p=>new SKPoint((float)p.X, (float)p.Y)).ToArray();
            
            var v_line_end_points = SKPointExtensions.CaculateAveragePoints(skRect.BottomLeft(), skRect.BottomRight(), ColumnCount).Select(p => new SKPoint((float)p.X, (float)p.Y)).ToArray();

            for (int i = 0; i < ColumnCount + 1; ++i)
            {
                skCanvas.DrawLine(v_line_start_points[i], v_line_end_points[i], sk_paint);
            }

            var h_line_start_points = SKPointExtensions.CaculateAveragePoints(skRect.TopLeft(), skRect.BottomLeft(), RowCount).Select(p => new SKPoint((float)p.X, (float)p.Y)).ToArray();
            var h_line_end_points = SKPointExtensions.CaculateAveragePoints(skRect.TopRight(), skRect.BottomRight(), RowCount).Select(p => new SKPoint((float)p.X, (float)p.Y)).ToArray();

            for (int i = 0; i < RowCount + 1; ++i)
            {
                skCanvas.DrawLine(h_line_start_points[i], h_line_end_points[i], sk_paint);
            }
        }
        private static SKPaint StripLineBrush { get; } = new SKPaint()
        {
            Style = SKPaintStyle.Fill,
            Color = new SKColor(0xFF, 0, 0)
        };
        private static SKPaint StripLinePen { get; } = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = new SKColor(0xFF, 0, 0)
        };
        public Int32 RowCount { get; set; } = 1;
        public Int32 ColumnCount { get; set; } = 1;
    }
}
