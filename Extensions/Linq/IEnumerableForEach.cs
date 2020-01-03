/*
Developer       : Leonardo Arteaga dos Santos
First release   : 20/12/19
File            : Extensions/Linq/IEnumerableForEach.cs
Revision        : 1.0
Changelog       :   
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace ForgeSDK.Extensions.Linq
{
    /// <summary>
    /// This extension class permit the use of.ForEach(Action<T>) on IEnumerable interfaces.
    /// </summary>
    public static class IEnumerableForEach
    {
        public static void ForEach<T>(this IEnumerable<T> col, Action<T> action)
        {
            if (action != null)
            {
                foreach (var item in col)
                {
                    action(item);
                }
            }
        }
    }
}
