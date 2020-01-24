/*
Developer       : Leonardo Arteaga dos Santos
First release   : 03/01/2020
File            : AssetManagement/Inspector/AddressableRepositoryWindowElement.cs
Revision        : 1.0.0
Changelog       :   

*/
#if UNITY_EDITOR

using ForgeSDK.AssetManagement.Repository;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ForgeSDK.AssetManagement
{
    /// <summary>
    /// Data structure for a visual representation on editor of <c>AddressableRepositoryItem</c>
    /// </summary>
    public class AddressableRepositoryWindowElement
    {
        /// <summary>
        /// Original key of the <c>AddressableRepositoryItem</c>
        /// </summary>
        private string _originalKey;
        /// <summary>
        /// The <c>AddressableRepositoryItem</c> is new?
        /// </summary>
        private bool _new;

        /// <summary>
        /// Asset referenced on <c>Reference</c>
        /// </summary>
        [ReadOnly]
        public UnityEngine.Object Asset;

        /// <summary>
        /// Key for the <c>AddressableRepository</c> system
        /// </summary>
        public string Key = "[Key]";

        /// <summary>
        /// <c>AssetReference</c> related with the <c>Key</c>
        /// </summary>
        [DrawWithUnity]
        public AssetReference Reference;

        /// <summary>
        /// Called when the item is updated, it sends the new key as message
        /// </summary>
        [HideInInspector]
        public EventHandler<string> Updated;

        /// <summary>
        /// Check if the originalKey exists in the <c>AddressableRepository</c> system
        /// </summary>
        public bool ExistsInRepository => AddressableRepository.Instance.Exists(_originalKey);

        /// <summary>
        /// Default constructor, it set the item as new and without key
        /// </summary>
        public AddressableRepositoryWindowElement() 
        {
            _originalKey = string.Empty;
            _new = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public AddressableRepositoryWindowElement(AddressableRepositoryItem item)
        {
            _new = false;
            Key = _originalKey = item.Key;
            if (item.Reference != null)
            {
                Asset = item.Reference.editorAsset;
                Reference = item.Reference;
            }
        }

        [ButtonGroup("Tools"), Button("Add"), ShowIf("_new")]
        public void Add()
        {
            var repo = AddressableRepository.Instance;
            repo.Add(new AddressableRepositoryItem(Key, Reference));
            _new = !repo.Save();
            if (!_new) _originalKey = Key;
        }

        [ButtonGroup("Tools"), Button("Update"), HideIf("_new")]
        public void Update()
        {
            var repo = AddressableRepository.Instance;
            var result = repo.Update(item => item.Key == _originalKey, item =>
            {
                item.Key = Key;
                item.Reference = Reference;
            });
            repo.Save();
            if (result != 0) _originalKey = Key;
        }

        [ButtonGroup("Tools"), ShowIf("ExistsInRepository"), Button("Remove")]
        public void Remove()
        {
            var repo = AddressableRepository.Instance;
            repo.Remove(_originalKey);
            repo.Save();
            Updated?.Invoke(this, _originalKey);
        }
    }
}
#endif