using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmos.Widgets.Primitives.Abstractions.DataVisualization
{
    public interface IInterpolator
    {
        Object Start { get; }
        Object End { get; }
        object Interploate(Object value);
    }
}
