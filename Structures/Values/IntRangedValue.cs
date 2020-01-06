using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Structures.Values
{
    public class IntRangedValue : RangedValue<int>
    {
        public override RangeOption InRange(int value)
        {
            if (value > MaxValue)
                return RangeOption.Higher;
            else if (value < MinValue) 
                return RangeOption.Minor;
            else 
                return RangeOption.InRange;
        }

        protected override int ModifyValue(int quantity)
        {
            _value += quantity;
            var result = InRange(_value);
            if (result == RangeOption.InRange)
                return quantity;
            else if (result == RangeOption.Minor)
            {
                int difference = MinValue - _value;
                _value = MinValue;
                return difference;
            }
            else
            {
                int difference =  _value - MaxValue;
                _value = MaxValue;
                return difference;
            }
        }
    }
}
