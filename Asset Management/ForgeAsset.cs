/*
Developer       : Leonardo Arteaga dos Santos
First release   : 03/01/2020
File            : AssetManagement/ForgeAsset.cs
Revision        : 1.0.0
Changelog       :   

*/
using ForgeSDK.AssetManagement.Repository;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ForgeSDK.AssetManagement
{
    /// <summary>
    /// A forge asset is the ForgeSDK asset reference way,
    /// </summary>
    /// <typeparam name="T"></typeparam>

    [Serializable, InlineProperty]
    public class ForgeAsset<T> where T : UnityEngine.Object
    {
#if UNITY_EDITOR
        [OnValueChanged("UpdateReference")]
#endif
        [HorizontalGroup("Asset"), SuffixLabel("KEY", true), HideLabel]
        public string Key;

#if UNITY_EDITOR
        [ShowIf("InPlayMode"), HorizontalGroup("Instance", width: 1), HideLabel, SuffixLabel("INSTANCE")]
#endif
        public T Instance;

        [HideInInspector]
        public EventHandler<T> Instantiated;

#if UNITY_EDITOR
        [HorizontalGroup("Asset", width: 100), ReadOnly, ShowInInspector, HideLabel]
        private UnityEngine.Object Asset;

        private void UpdateReference()
        {
            if (Repository.Exists(Key))
                Asset = Repository.GetByKey(Key).Reference.editorAsset;
            else
                Asset = null;
        }

        private bool InPlayMode => Application.isPlaying;
#endif
        private AddressableRepository Repository => AddressableRepository.Instance;

        /// <summary>
        /// Instantiate an GameObject 
        /// </summary>
        /// <param name="position">Initial position</param>
        /// <param name="rotation">Initial rotation</param>
        /// <param name="parent">Parent</param>
        /// <param name="action">Action to be done when the object it's instantiated</param>
        public void Instantiate(Vector3 position, Quaternion rotation, Transform parent = null, Action<T> action = null)
        {
            if (Repository.Exists(Key))
            {
                Addressables.InstantiateAsync(Repository.GetByKey(Key).Reference, position, rotation, parent).Completed += e =>
                {
                    T componentResult = null;
                    if (e.Result != null)
                    {
                        componentResult = e.Result.GetComponent<T>();
                        if (componentResult != null && action != null) action(componentResult);
                        Instantiated?.Invoke(this, componentResult);
                    }
                };
            }
        }
    }
}
