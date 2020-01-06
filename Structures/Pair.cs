/*
Developer       : Leonardo Arteaga dos Santos
First release   : 03/01/2020
File            : Structures/Pair.cs
Revision        : 1.0
Changelog       :   
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Structures
{
    /// <summary>
    /// A pair with two atributes with the same data type
    /// </summary>
    [Serializable]
    public struct Pair<T>
    {
        public T x;
        public T y;

        public Pair(T x, T y)
        {
            this.x = x;
            this.y = y;
        }
    }

    /// <summary>
    /// A pair with two atributes with different data types
    /// </summary>
    [Serializable]
    public struct Pair<T, G>
    {
        public T x;
        public G y;

        public Pair(T x, G y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
