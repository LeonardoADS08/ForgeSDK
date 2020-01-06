/*
Developer       : Leonardo Arteaga dos Santos
First release   : 06/01/2020
File            : Structures/Values/IRangedValue.cs
Revision        : 1.0
Changelog       :   
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Structures.Values
{
    public interface IRangedValue<T>
    {
        /// <summary>
        /// Actual value
        /// </summary>
        T Value { get; }

        /// <summary>
        /// Max possible value
        /// </summary>
        T MaxValue { get; set; }

        /// <summary>
        /// Min possible value
        /// </summary>
        T MinValue { get; set; }

        /// <summary>
        /// Triggered when the value reach his minimun
        /// </summary>
        EventHandler<T> MinValueReached { get; set; }
        /// <summary>
        /// Triggered when the value reach his maximun
        /// </summary>
        EventHandler<T> MaxValueReached { get; set; }
        /// <summary>
        /// Triggered when the max value has been changed
        /// </summary>
        EventHandler<T> MinValueChanged { get; set; }
        /// <summary>
        /// Triggered when the min value has been changed
        /// </summary>
        EventHandler<T> MaxValueChanged { get; set; }
        /// <summary>
        /// Triggered when the value has been changed
        /// </summary>
        EventHandler<T> Variation { get; set; }

        /// <summary>
        /// Check if a value is in range according to the ranges
        /// </summary>
        /// <param name="value">Value to be analyzed</param>
        /// <returns>If it's higher than the maximun possible, in range or lower than the minimun possible</returns>
        RangeOption InRange(T value);

        /// <summary>
        /// Apply a variation to the value
        /// </summary>
        /// <param name="quantity">Quantitiy to be variated</param>
        /// <param name="raiseEvents">Raise <c>Variation</c> events</param>
        /// <returns>Return the real quantity that was modified according to the ranges</returns>
        T ApplyVariation(T quantity, bool raiseEvents = true);
    }
}
