/*
Developer       : Leonardo Arteaga dos Santos
First release   : 03/01/2020
File            : Maths/ForgeRandom.cs
Revision        : 1.0
Changelog       :   
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Maths
{
    /// <summary>
    /// Random values generator
    /// </summary>
    public class ForgeRandom
    {
        private Random _generator;

        /// <summary>
        /// Seed used for the generator
        /// </summary>
        public int Seed { get; private set; }

        public ForgeRandom()
        {
            Seed = (new Random()).Next(0, int.MaxValue);
            _generator = new Random(Seed);
        }

        public ForgeRandom(int seed)
        {
            Seed = seed;
            _generator = new Random(seed);
        }

        /// <summary>
        /// Generate an integer between 0 - 2^31
        /// </summary>
        /// <returns>A random integer</returns>
        public int NextInteger() => _generator.Next();

        /// <summary>
        /// Generate an integer between 0 - <paramref name="Max"/>
        /// </summary>
        /// <param name="Max">Max value to be generated</param>
        /// <returns>A random integer</returns>
        public int NextInteger(int Max) => _generator.Next(Max);

        /// <summary>
        /// Generate an integer between <paramref name="Min"/> - <paramref name="Max"/>
        /// </summary>
        /// <param name="Min">Min value</param>
        /// <param name="Max">Max value</param>
        /// <returns>A random integer</returns>
        public int NextInteger(int Min, int Max) => _generator.Next(Min, Max);

        /// <summary>
        /// Generate a random double between 0 - 1
        /// </summary>
        /// <returns>A random double</returns>
        public double NextDouble() => _generator.NextDouble();

        /// <summary>
        /// Generate a random double between 0 - <paramref name="Max"/>
        /// </summary>
        /// <param name="Max">Max value</param>
        /// <returns>A random double</returns>
        public double NextDouble(double Max) => NextDouble() * Max;

        /// <summary>
        /// Generate a random double between <paramref name="Min"/> - <paramref name="Max"/>
        /// </summary>
        /// <param name="Min">Min value</param>
        /// <param name="Max">Max value</param>
        /// <returns>A random double</returns>
        public double NextDouble(double Min, double Max) => NextDouble() * (Max - Min) + Min;

        /// <summary>
        /// Generate a float double between 0 - 1
        /// </summary>
        /// <returns>A random float</returns>
        public float NextFloat() => (float)_generator.NextDouble();

        /// <summary>
        /// Generate a float double between 0 - <paramref name="Max"/>
        /// </summary>
        /// <param name="Max">Max value</param>
        /// <returns>A random float</returns>
        public float NextFloat(float Max) => NextFloat() * Max;

        /// <summary>
        /// Generate a random float between <paramref name="Min"/> - <paramref name="Max"/>
        /// </summary>
        /// <param name="Min">Min value</param>
        /// <param name="Max">Max value</param>
        /// <returns>A random float</returns>
        public float NextFloat(float Min, float Max) => NextFloat() * (Max - Min) + Min;

        /// <summary>
        /// Generate a random bool
        /// </summary>
        /// <returns>A random bool</returns>
        public bool NextBool() => NextFloat() > 0.5f;

        /// <summary>
        /// Generate a random signed integer (1 or -1)
        /// </summary>
        /// <returns>A random signed integer</returns>
        public int NextSign() => (NextBool()) ? 1 : -1;
    }
}
