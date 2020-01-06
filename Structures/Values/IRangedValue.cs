using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Structures.Values
{
    public interface IRangedValue<T>
    {
        T Value { get; }
        T MaxValue { get; set; }
        T MinValue { get; set; }

        EventHandler<T> MinValueReached { get; set; }
        EventHandler<T> MaxValueReached { get; set; }
        EventHandler<T> Variation { get; set; }
        EventHandler<T> MinValueChanged { get; set; }
        EventHandler<T> MaxValueChanged { get; set; }

        RangeOption InRange(T value);
        T ApplyVariation(T quantity, bool raiseEvents = true);
    }
}
