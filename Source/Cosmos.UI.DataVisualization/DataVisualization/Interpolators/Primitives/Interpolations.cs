using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmos.Widgets.Primitives.Abstractions.DataVisualization.Interpolators.Primitives
{
    public static class Interpolations
    {
        public static double InterpolateDouble(double a, double b, double t)
        {
            return a * (1 - t) + b * t;
        }
        public static double InterpolateRound(double a, double b, double t)
        {
            return Math.Round(InterpolateDouble(a, b, t));
        }
    }
}
