/*
Developer       : Leonardo Arteaga dos Santos
First release   : 06/01/2020
File            : Structures/Values/FloatRangedValue.cs
Revision        : 1.0
Changelog       :   
*/

using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Structures.Values
{
    /// <summary>
    /// A <c>RangedValue</c> implementation for <c>float</c> ranged values
    /// </summary>
    [Serializable, InlineProperty]
    public class FloatRangedValue : RangedValue<float>
    {
        public FloatRangedValue()
        {
            _min = 0;
            _max = 100;
            _value = 50;
        }

        public FloatRangedValue(float value, float minValue, float maxValue)
        {
            _min = minValue;
            _max = maxValue;
            _value = value;
        }

        public override RangeOption InRange(float value)
        {
            if (value > MaxValue)
                return RangeOption.Higher;
            else if (value < MinValue)
                return RangeOption.Minor;
            else
                return RangeOption.InRange;
        }

        public override RangeOption Validate()
        {
            if (_value > MaxValue)
            {
                _value = MaxValue;
                return RangeOption.Higher;
            }
            else if (_value < MinValue)
            {
                _value = MinValue;
                return RangeOption.Minor;
            }
            else return RangeOption.InRange;
        }

        protected override float ModifyValue(float quantity)
        {
            _value += quantity;
            var result = InRange(_value);
            if (result == RangeOption.InRange)
                return quantity;
            else if (result == RangeOption.Minor)
            {
                float difference = MinValue - _value;
                _value = MinValue;
                return difference;
            }
            else
            {
                float difference = _value - MaxValue;
                _value = MaxValue;
                return difference;
            }
        }


    }
}
