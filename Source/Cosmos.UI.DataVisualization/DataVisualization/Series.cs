
using Cosmos.Widgets.Primitives.Abstractions.DataVisualization.Scales.Primitives;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cosmos.Widgets.Primitives.Abstractions.DataVisualization
{
    public enum SeriesType
    {
        Polyline,
        CandleStick,
        Bar,
    }

    public class Series : IChartElement
    {
        public void PlotToCanvas(SKCanvas skCanvas, SKRect skRect)
        {
            FullArea = skRect;
            if (IsVisible)
            {
                // 绘制网格
                if (StripLines.IsVisible)
                {
                    StripLines.PlotToCanvas(skCanvas, StripLinesArea);
                }
                // 绘制X轴
                if (AxisX.IsVisible)
                {
                    AxisX.KeyRange.Objects = new List<Object>() { "9:30", "10:00", "10:30", "11:00", "11:30", "13:30", "14:00", "14:30", "15:00" };
                    AxisX.PixelRange = new ContinousRange(AxisXArea.Left, AxisXArea.Right);
                    AxisX.PlotAsX(skCanvas, AxisXArea);
                }
                // 绘制Y轴
                if (AxisY.IsVisible)
                {
                    AxisY.Scale = new ContinuousScale()
                    {
                        InputRange = new ContinousRange(0, 100),
                        OutputRange = new ContinousRange(AxisXArea.Top, AxisXArea.Bottom)
                    };
                    AxisY.IntervalCount = 8;
                    AxisY.PlotAsLeft(skCanvas, AxisYArea);
                }
            }
        }
        private SKRect FullArea { get; set; }
        public SKThickness StripLineAreaMargin { get; set; } = new SKThickness(32, 16, 32, 16);
        public SKRect StripLinesArea
        {
            get
            {
                var top = (float)StripLineAreaMargin.Top;
                var left = (float)StripLineAreaMargin.Left;
                var bottom = FullArea.Height - (float)StripLineAreaMargin.Bottom;
                var right = FullArea.Width - (float)StripLineAreaMargin.Right;
                return new SKRect(top, left, right, bottom);
            }
        }
        public SKRect AxisXArea
        {
            get
            {
                var top = FullArea.Height - (float)StripLineAreaMargin.Bottom;
                var left = (float)StripLineAreaMargin.Left;
                var bottom = FullArea.Height;
                var right = FullArea.Width - (float)StripLineAreaMargin.Right;

                return new SKRect(top, left, right, bottom);
            }
        }
        public SKRect AxisYArea
        {
            get
            {
                var top = (float)StripLineAreaMargin.Top;
                var left = 0;
                var bottom = FullArea.Height - (float)StripLineAreaMargin.Bottom;
                var right = (float)StripLineAreaMargin.Right;

                return new SKRect(top, left, right, bottom);

            }
        }
        public DataTable Table { get; set; }
        public DiscreteAxis AxisX { get; } = new DiscreteAxis();
        public Axis AxisY { get; } = new Axis();
        public SeriesType Type { get; set; } = SeriesType.Polyline;
        public StripLines StripLines { get; } = new StripLines()
        {
            RowCount = 8,
            ColumnCount = 8,
        };
        public bool IsVisible { get; set; } = true;
    }

}
