
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmos.Widgets.Primitives.Abstractions.DataVisualization
{
    public static class MathExtensions
    {
        public static IEnumerable<double> CaculateAverageDoubles(double start, double end, Int32 interval_count)
        {
            var dx = end - start;
            Int32 total_point_count = interval_count + 1;
            var dx_interval = dx / interval_count;
            for (int point_index = 0; point_index < total_point_count; ++point_index)
            {
                yield return start + dx_interval * point_index;
            }
        }
        
    }
}
