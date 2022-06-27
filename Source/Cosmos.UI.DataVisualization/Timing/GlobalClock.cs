using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Cosmos.Widgets.Primitives.Abstractions.Timing
{
    public class GlobalClock
    {
        public GlobalClock()
        {

        }
        DateTime InitialDateTime { get; set; }
        
        Timer Timer { get; }
    }
}
