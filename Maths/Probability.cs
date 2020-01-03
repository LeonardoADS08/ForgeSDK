/*
Developer       : Leonardo Arteaga dos Santos
First release   : 03/01/2020
File            : Maths/Probability.cs
Revision        : 1.0
Changelog       :   
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ForgeSDK.Maths
{
    /// <summary>
    /// Manage probabilities, events, random elements from list and lists shuffle
    /// </summary>
    public class Probability : ForgeRandom
    {
        public Probability() : base() { }

        public Probability(int Semilla) : base(Semilla) { }

        /// <summary>
        /// A simple statistical test 
        /// </summary>
        /// <param name="probability">Probability of true</param>
        /// <returns>Test result</returns>
        public bool SimpleEvent(float probability) => this.NextFloat() <= probability;

        /// <summary>
        /// Select a random element from the <paramref name="data"/>
        /// </summary>
        /// <param name="data">List of elements</param>
        /// <returns>Random element or default(<typeparamref name="T"/>) if list is empty</returns>
        public T RandomElement<T>(List<T> data)
        {
            if (data.Count == 1) return data[0];
            else if (data.Count != 0) return data[NextInteger(data.Count)];
            else return default(T);
        }

        /// <summary>
        /// Select a fixed <paramref name="quantity"/> of random elements from the list <paramref name="data"/>
        /// </summary>
        /// <param name="data">List of elements</param>
        /// <param name="quantity">Quantity of elements to select</param>
        /// <returns>A random list of elements</returns>
        public List<T> RandomElements<T>(List<T> data, int quantity)
        {
            if (data.Count < quantity) return data;
            List<T> result = new List<T>();
            List<T> temp = new List<T>(data);
            for (int i = 0; i < quantity; i++)
            {
                var element = RandomElement(temp);
                temp.Remove(element);
                result.Add(element);
            }
            return result;
        }

        /// <summary>
        /// Shuffle the list randomly
        /// </summary>
        /// <param name="data">List of elements</param>
        /// <returns>A randomized list</returns>
        public List<T> Shuffle<T>(List<T> data)
        {
            List<T> result = new List<T>();
            int finalQuantity = data.Count;
            while (result.Count != finalQuantity)
            {
                var element = RandomElement(data);
                result.Add(element);
                data.Remove(element);
            }
            return result;
        }

        /// <summary>
        /// Select a fixed shuffled <paramref name="quantity"/> of elements from the list <paramref name="data"/>
        /// </summary>
        /// <param name="data">List of elements</param>
        /// <param name="quantity">Quantity of elements to select</param>
        /// <returns>A shuffled list of size <paramref name="quantity"/></returns>
        public List<T> Shuffle<T>(List<T> data, int quantity)
        {
            List<T> temp = new List<T>(data);
            List<T> result = new List<T>();
            quantity = Mathf.Min(temp.Count, quantity);
            while (result.Count != quantity)
            {
                var element = RandomElement(temp);
                result.Add(element);
                temp.Remove(element);
            }
            return result;
        }
    }
}
