using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmos.Widgets.Primitives.Abstractions.DataVisualization.Interpolators
{
    public class NumberInterpolator : IInterpolator
    {
        public object Start { get; set; }
        public object End { get; set; }

        public object Interploate(object value)
        {
            var start_double = (double)Start;
            var end_double = (double)End;
            var value_double = (double)value;
            return start_double * (1 - value_double) + end_double * value_double;
        }
    }
}
