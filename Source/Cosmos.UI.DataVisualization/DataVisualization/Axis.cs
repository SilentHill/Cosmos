
using Cosmos.Widgets.Primitives.Abstractions.DataVisualization.Scales.Primitives;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Cosmos.Widgets.Primitives.Abstractions.DataVisualization
{
    public enum AxisOrientation
    {
        Left,
        Top,
        Right,
        Bottom,
    }
    public class Axis : IChartElement
    {
        public void PlotToCanvas(SKCanvas skCanvas, SKRect skRect)
        {
            if (Orientation == AxisOrientation.Left)
            {
                PlotAsLeft(skCanvas, skRect);
            }
        }
        public bool IsVisible { get; set; } = true;
        public Axis()
        {

        }
        public delegate List<double> TicksGenerator<in T, out TResult>(T arg);

        AxisOrientation Orientation { get; set; } = AxisOrientation.Left;
        public ContinuousScale Scale { get; set; }
        public Int32 IntervalCount { get; set; } = 8;
        public SKThickness LabelMargin { get; set; } = new SKThickness(4);
        public SKPaint TickMarkPaint { get; set; } = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = new SKColor(0xFF, 0x00, 0x00)
        };
        public void PlotAsLeft(SKCanvas skCanvas, SKRect skRect)
        {
            var label_strings = MathExtensions.CaculateAverageDoubles(Scale.InputRange.Min, Scale.InputRange.Max, IntervalCount);

            var label_strings_list = label_strings.ToList();

            var tick_points = SKPointExtensions.CaculateAveragePoints(skRect.TopRight(), skRect.BottomRight(), label_strings_list.Count - 1)
                .Select(p => new SKPoint((float)p.X, (float)p.Y)).ToArray();

            for (int i = 0; i < label_strings_list.Count; ++i)
            {
                var point = tick_points[i];
                point.Offset((float)(skRect.Width - LabelMargin.Left),
                                     (float)(skRect.Height / 2));
                skCanvas.DrawText(label_strings_list[i].ToString(), point, TickMarkPaint);
            }
        }
    }

}
