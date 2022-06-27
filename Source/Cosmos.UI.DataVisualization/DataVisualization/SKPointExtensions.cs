using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmos.Widgets.Primitives.Abstractions.DataVisualization
{
    public static class SKPointExtensions
    {
        public static IEnumerable<SKPoint> CaculateAveragePoints(SKPoint startPoint, SKPoint endPoint, Int32 intervalCount)
        {

            var dx = endPoint.X - startPoint.X;
            var dy = endPoint.Y - startPoint.Y;

            Int32 total_point_count = intervalCount + 1;

            var dx_interval = dx / intervalCount;
            var dy_interval = dy / intervalCount;

            for (int point_index = 0; point_index < total_point_count; ++point_index)
            {
                var point = startPoint;
                point.Offset(dx_interval * point_index, dy_interval * point_index);
                yield return point;
            }
        }
    }
}
