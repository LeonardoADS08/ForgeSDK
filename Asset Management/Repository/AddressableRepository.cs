/*
Developer       : Leonardo Arteaga dos Santos
First release   : 03/01/2020
File            : AssetManagement/Repository/AddressableRepository.cs
Revision        : 1.0.0
Changelog       :   

*/

using ForgeSDK.Extensions.Linq;
using ForgeSDK.Repositories;
using ForgeSDK.Structures;
using ForgeSDK.Tools;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace ForgeSDK.AssetManagement.Repository
{
    /// <summary>
    /// <c>AddressableRepository</c> is an abstract implementation of <c>Repository</c>,
    /// it gives all the repository management, an inherited class of <c>AddressableRepository</c> just need
    /// to implement the way how the data is stored
    /// </summary>
    public abstract class AddressableRepository : Repository<AddressableRepositoryItem>
    {

        #region Singleton  
        /// <summary>
        /// Instance of singleton
        /// </summary>
        private static AddressableRepository _instance;

        /// <summary>
        /// Get method of singleton
        /// </summary>
        public static AddressableRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new JsonAddressableRepository();
                    _instance.Load();
                }
                return _instance;
            }
        }

        public static void PurgeInstance() => _instance = null;

        #endregion

        /// <summary>
        /// Dicitonary where is stored all the references
        /// </summary>

        protected Dictionary<string, AddressableRepositoryItem> _repository = new Dictionary<string, AddressableRepositoryItem>();
        
        /// <summary>
        /// Method to get all the <c>AddressableRepositoryItem</c> available on the repository
        /// </summary>
        protected override IEnumerable<AddressableRepositoryItem> _items => _repository.Values;

        /// <summary>
        /// Abstract method, it save the repository into a persistent file
        /// </summary>
        /// <returns>If the file was successfully saved or not</returns>
        public abstract override bool Save();

        /// <summary>
        /// Abstract method, it loads the repository into memory
        /// </summary>
        /// <returns>If the file was successfully loaded or not</returns>
        public abstract override bool Load();

        /// <summary>
        /// Get an item from the repository given a key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public AddressableRepositoryItem GetByKey(string key) => _repository.ContainsKey(key) ? _repository[key] : null;

        /// <summary>
        /// Add a new asset to the repository, but isn't saved on a persistent way
        /// </summary>
        /// <param name="element">New element to be added</param>
        /// <returns>If the element was successfully added to the repository or not</returns>
        public override bool Add(AddressableRepositoryItem element)
        {
            if (!_repository.ContainsKey(element.Key))
            {
                _repository.Add(element.Key, element);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Ask if a key exists in the repository
        /// </summary>
        /// <param name="key">Key to be searched</param>
        /// <returns>If the element was successfully found or not</returns>
        public bool Exists(string key) => _repository.ContainsKey(key);

        /// <summary>
        /// Ask if an item exists in the repository
        /// </summary>
        /// <param name="item">Item to be searched</param>
        /// <returns>If the element was successfully found or not</returns>
        public bool Exists(AddressableRepositoryItem item) => Exists(item.Key);

        /// <summary>
        /// Given a predicate, search if at least one element accept the predicate
        /// </summary>
        /// <param name="predicate">Predicate to be used as test</param>
        /// <returns>If an element succesfully pass the predicate or not</returns>
        public override bool Exists(Func<AddressableRepositoryItem, bool> predicate) => _repository.Values.ToList().Exists(element => predicate(element));


        /// <summary>
        /// Remove an element from the repository, but not in the persistent way
        /// </summary>
        /// <param name="key">Key to be removed</param>
        /// <returns>If the element was succesfully removed or not</returns>
        public bool Remove(string key) => _repository.Remove(key);

        /// <summary>
        /// Remove an element from the repository, but not in the persistent way
        /// </summary>
        /// <param name="element">Element to be removed</param>
        /// <returns>If the element was succesfully removed or not</returns>
        public override bool Remove(AddressableRepositoryItem element) => Remove(element.Key);

        /// <summary>
        /// Remove an element from the repository, but not in the persistent way
        /// </summary>
        /// <param name="predicate">Predicate to test all the items</param>
        /// <returns>If an element was succesfully removed or not</returns>
        public override int Remove(Func<AddressableRepositoryItem, bool> predicate)
        {
            int count = 0;
            List<string> removeKeys = new List<string>();
            foreach (var item in _repository.Values)
            {
                if (predicate(item))
                {
                    removeKeys.Add(item.Key);
                    count++;
                }
            }
            removeKeys.ForEach(key => _repository.Remove(key));
            return count;
        }

        /// <summary>
        /// Update the dictionary records, not in the persistent way
        /// </summary>
        /// <param name="predicate">Predicate to know wich elements should be updated</param>
        /// <param name="action">Action to be done</param>
        /// <returns>Quantity of updated elements</returns>
        public override int Update(Func<AddressableRepositoryItem, bool> predicate, Action<AddressableRepositoryItem> action)
        {
            int count = 0;
            _repository.Values
            .Where(item => predicate(item))
            .ForEach(original =>
            {
                var modified = original.Copy();
                action(modified);

                if (original.Equals(modified))
                {
                    _repository[modified.Key] = modified;
                    count++;
                }
                else if (!_repository.ContainsKey(modified.Key))
                {
                    _repository.Remove(original.Key);
                    _repository.Add(modified.Key, modified);
                    count++;
                }
            });
            return count;
        }
    }

}
