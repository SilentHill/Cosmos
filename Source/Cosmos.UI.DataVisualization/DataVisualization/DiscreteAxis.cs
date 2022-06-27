
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmos.Widgets.Primitives.Abstractions.DataVisualization
{
    public class DiscreteAxis : IChartElement
    {
        public DiscreteAxis()
        {

        }
        public double KeyToPixel(object key)
        {
            return PixelRange.RatioToValue(KeyRange.ValueToRatio(key));
        }
        public object PixelToKey(double pixel)
        {
            return KeyRange.RatioToValue(PixelRange.ValueToRatio(pixel));
        }
        public void PlotToCanvas(SKCanvas skCanvas, SKRect skRect)
        {
            if (Position == AxisOrientation.Top)
            {
                PlotAsX(skCanvas, skRect);
            }
        }
        public void PlotAsX(SKCanvas skCanvas, SKRect skRect)
        {
            var label_strings_list = KeyRange.Objects.Select(key => key.ToString()).ToList();
            var label_center_points = SKPointExtensions.CaculateAveragePoints(skRect.TopLeft(), skRect.TopRight(), label_strings_list.Count - 1)
                .Select(p => new SKPoint((float)p.X, (float)p.Y)).ToArray();

            for (int i = 0; i < KeyRange.Objects.Count; ++i)
            {
                if (i >= label_strings_list.Count)
                {
                    break;
                }
                var point = label_center_points[i];
                point.Offset((float)(skRect.Width / 2), (float)LabelMargin.Top);
                skCanvas.DrawText(label_strings_list[i], point, TickMarkPaint);
            }
        }
        public SKPaint TickMarkPaint { get; set; } = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = new SKColor(0xFF, 0x00, 0x00)
        };
        public AxisOrientation Position { get; set; } = AxisOrientation.Top;
        public SKThickness LabelMargin { get; set; } = new SKThickness(4);
        public DiscreteRange KeyRange { get; } = new DiscreteRange();
        public ContinousRange PixelRange { get; set; } = new ContinousRange();
        public bool IsVisible { get; set; } = true;
    }
}
