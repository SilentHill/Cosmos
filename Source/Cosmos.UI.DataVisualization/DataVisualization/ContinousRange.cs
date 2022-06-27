using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmos.Widgets.Primitives.Abstractions.DataVisualization
{
    /// <summary>
    /// 数学范围，基于直角坐标系，即Y轴指向上（与屏幕矩形相反）
    /// </summary>
    public struct ContinousRange
    {
        public double Min { get; set; }
        public double Max { get; set; }
        public ContinousRange(double minimum, double maximum)
        {
            if (minimum < maximum)
            {
                Min = minimum;
                Max = maximum;
            }
            else
            {
                Min = maximum;
                Max = minimum;
            }
        }
        private ContinousRange(bool isEmpty)
        {
            if (isEmpty)
            {
                Min = double.PositiveInfinity;
                Max = double.NegativeInfinity;
            }
            else
            {
                Min = 0;
                Max = 0;
            }
        }
        public static readonly ContinousRange Empty = new ContinousRange(true);
        public bool IsEmpty
        {
            get
            {
                return Min > Max;
            }
        }
        public bool IsPoint
        {
            get
            {
                return Max == Min;
            }
        }
        public void Surround(double value)
        {
            if (value < Min)
            {
                Min = value;
            }
            if (value > Max)
            {
                Max = value;
            }
        }
        public void Surround(ContinousRange range)
        {
            if (range.IsEmpty)
            {
                return;
            }
            Surround(range.Min);
            Surround(range.Max);
        }
        public override string ToString()
        {
            return "[" + Min.ToString(CultureInfo.InvariantCulture) + "," + Max.ToString(CultureInfo.InvariantCulture) + "]";
        }
        public ContinousRange Zoom(double factor)
        {
            if (IsEmpty)
            {
                return new ContinousRange(true);
            }
            double delta = (Max - Min) / 2;
            double center = (Max + Min) / 2;
            var new_range = new ContinousRange(center - delta * factor, center + delta * factor);
            return new_range;
        }
        public double ValueToRatio(double value)
        {
            return (value - Min) / Span;
        }
        public double RatioToValue(double ratio)
        {
            return (ratio * Span) + Min;
        }
        public double Span
        {
            get
            {
                return Max - Min;
            }
        }
        public override bool Equals(object obj)
        {
            ContinousRange r = (ContinousRange)obj;
            return r.Min == Min && r.Max == Max;
        }
        public override int GetHashCode()
        {
            return Min.GetHashCode() ^ Max.GetHashCode();
        }
        public static bool operator ==(ContinousRange first, ContinousRange second)
        {
            return first.Equals(second);
        }
        public static bool operator !=(ContinousRange first, ContinousRange second)
        {
            return !first.Equals(second);
        }
    }
}
