using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmos.Widgets.Primitives.Abstractions.DataVisualization.Scales.Primitives
{
    /// <summary>
    /// 连续比例尺（将一个连续的，定量的输入映射到连续的输出区间）
    /// </summary>
    public class ContinuousScale : IScale
    {
        public double ComputeOutputValue(double inputValue)
        {
            var output_value = (((inputValue - InputRange.Min) * OutputRange.Span) / InputRange.Span) + OutputRange.Min;
            return output_value;
        }
        public double ComputeInputValue(double outputValue)
        {
            var input_value = (((outputValue - OutputRange.Min) * InputRange.Span) / OutputRange.Span) + InputRange.Min;
            return input_value;
        }
        public ContinousRange InputRange { get; set; }
        public ContinousRange OutputRange { get; set; }
        public IInterpolator Interpolator { get; set; }
    }
}
