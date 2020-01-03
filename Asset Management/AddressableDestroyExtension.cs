/*
Developer       : Leonardo Arteaga dos Santos
First release   : 03/01/2020
File            : AssetManagement/AddressableDestroyExtension.cs
Revision        : 1.0.0
Changelog       :   

*/

using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ForgeSDK.AssetManagement
{
    /// <summary>
    /// Extension class to support a safe way to destroy GameObjects no matters if there is an Addressable or a Instance
    /// </summary>
    public static class AddressableDestroyExtension
    {
        /// <summary>
        /// Destroy the GameObject
        /// </summary>
        /// <param name="gameObject">GameObject to be destroyed</param>
        public static void ForgeDestroy(this GameObject gameObject)
        {
            bool destroyed = Addressables.ReleaseInstance(gameObject);
            if (!destroyed) MonoBehaviour.Destroy(gameObject);
        }

        /// <summary>
        /// Destroy the GameObject in a given time
        /// </summary>
        /// <param name="gameObject">GameObject to be destroyed</param>
        /// <param name="time">Time to wait (In seconds)</param>
        public async static void ForgeDestroy(this GameObject gameObject, float time)
        {
            await Task.Delay(TimeSpan.FromSeconds(time));
            gameObject.ForgeDestroy();
        }

        /// <summary>
        /// Destroy the GameObject in a given time and execute an action before the object is destroyed
        /// </summary>
        /// <param name="gameObject">GameObject to be destroyed</param>
        /// <param name="time">Time to wait (In seconds)</param>
        /// <param name="action">Action to execute before destruction</param>
        public async static void ForgeDestroy(this GameObject gameObject, float time, Action<GameObject> action)
        {
            await Task.Delay(TimeSpan.FromSeconds(time));
            action?.Invoke(gameObject);
            gameObject.ForgeDestroy();
        }

    }
}
