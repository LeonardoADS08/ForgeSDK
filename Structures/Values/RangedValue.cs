/*
Developer       : Leonardo Arteaga dos Santos
First release   : 06/01/2020
File            : Structures/Values/RangedValue.cs
Revision        : 1.0
Changelog       :   
*/

using ForgeSDK.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Structures.Values
{
    /// <summary>
    /// Abstract implementation of <c>IRangedValue</c>
    /// </summary>
    /// <typeparam name="T">Type of the ranged value</typeparam>
    public abstract class RangedValue<T> : IRangedValue<T>
    {
        protected T _value;
        public virtual T Value => _value;

        protected Pair<T> _range;
        
        public virtual T MinValue
        {
            get => _range.x;
            set
            {
                T oldValue = _range.x;
                _range.x = value;
                MinValueChanged?.Invoke(this, oldValue);
            }
        }

        public virtual T MaxValue
        {
            get => _range.y;
            set
            {
                T oldValue = _range.y;
                _range.y = value;
                MaxValueChanged?.Invoke(this, oldValue);
            }
        }

        protected EventHandler<T> _minValueChanged, _minValueReached, _maxValueChanged, _maxValueReached, _variation;

        public EventHandler<T> MinValueReached { get => _minValueReached; set => _minValueReached = value; }
        public EventHandler<T> MaxValueReached { get => _maxValueReached; set => _maxValueReached = value; }
        public EventHandler<T> MinValueChanged { get => _minValueChanged; set => _minValueChanged = value; }
        public EventHandler<T> MaxValueChanged { get => _maxValueChanged; set => _maxValueChanged = value; }
        public EventHandler<T> Variation { get => _variation; set => _variation = value; }

        /// <summary>
        /// Logic for the value modification
        /// </summary>
        /// <param name="quantity">Quantity to be modified</param>
        /// <returns>Return the real quantity that was modified according to the ranges</returns>
        protected abstract T ModifyValue(T quantity);

        public abstract RangeOption InRange(T value);

        public virtual T ApplyVariation(T quantity, bool raiseEvents = true)
        {
            T finalVariation = ModifyValue(quantity);
            if (raiseEvents) _variation?.Invoke(this, finalVariation);
            return finalVariation;
        }


    }
}
