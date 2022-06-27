using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmos.Widgets.Primitives.Abstractions.DataVisualization
{
    public class DiscreteRange
    {
        public double ValueToRatio(object value)
        {
            return IndexOf(value) / Objects.Count;
        }
        public object RatioToValue(double ratio)
        {
            Int32 index = (Int32)ratio * Objects.Count;
            return Objects[index];
        }
        private Int32 IndexOf(object value)
        {
            return Objects.IndexOf(value);
        }
        public List<Object> Objects { get; set; } = new List<object>();
    }
}
