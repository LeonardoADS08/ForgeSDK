/*
Developer       : Leonardo Arteaga dos Santos
First release   : 06/01/2020
File            : Structures/Values/RangedValue.cs
Revision        : 1.0
Changelog       :   
*/

using ForgeSDK.Structures;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ForgeSDK.Structures.Values
{
    /// <summary>
    /// Abstract implementation of <c>IRangedValue</c>
    /// </summary>
    /// <typeparam name="T">Type of the ranged value</typeparam>
    [Serializable, InlineProperty]
    public abstract class RangedValue<T> : IRangedValue<T>, ISerializationCallbackReceiver
    {
        [ShowInInspector, OdinSerialize, HideLabel, SuffixLabel("Min", true), HorizontalGroup(order: 0, GroupName = "Parameters", GroupID = "Parameters")]
        protected T _min;
        [ShowInInspector, OdinSerialize, HideLabel, SuffixLabel("Value", true), HorizontalGroup(order: 1, GroupName = "Parameters", GroupID = "Parameters")]
        protected T _value;
        [ShowInInspector, OdinSerialize, HideLabel, SuffixLabel("Max", true), HorizontalGroup(order: 2, GroupName = "Parameters", GroupID = "Parameters")]
        protected T _max;
        public virtual T Value => _value;

        public virtual T MinValue
        {
            get => _min;
            set
            {
                T oldValue = _min;
                _min = value;
                MinValueChanged?.Invoke(this, oldValue);
            }
        }

        public virtual T MaxValue
        {
            get => _max;
            set
            {
                T oldValue = _max;
                _max = value;
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

        public abstract RangeOption Validate();

        public virtual T ApplyVariation(T quantity, bool raiseEvents = true)
        {
            T finalVariation = ModifyValue(quantity);
            if (raiseEvents) _variation?.Invoke(this, finalVariation);
            return finalVariation;
        }

        public void OnBeforeSerialize() => Validate();

        public void OnAfterDeserialize() => Validate();
    }
}
