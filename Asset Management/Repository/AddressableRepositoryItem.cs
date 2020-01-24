/*
Developer       : Leonardo Arteaga dos Santos
First release   : 03/01/2020
File            : AssetManagement/Repository/AddressableRepositoryItem.cs
Revision        : 1.0.0
Changelog       :   

*/

using ForgeSDK.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace ForgeSDK.AssetManagement.Repository
{
    /// <summary>
    /// An repository item, it contains a key and the asset reference
    /// </summary>
    [Serializable]
    public class AddressableRepositoryItem : ICopyable<AddressableRepositoryItem>
    {
        public string Key { get; set; }

        public AssetReference Reference { get; set; }

        public AddressableRepositoryItem() { }

        public AddressableRepositoryItem(string key, string GUID) 
        {
            Key = key;
            Reference = new AssetReference(GUID);
        }

        public AddressableRepositoryItem(string key, AssetReference reference)
        {
            Key = key;
            Reference = reference;
        }

        public AddressableRepositoryItem Copy() => new AddressableRepositoryItem(Key, Reference);

        public bool Equals(AddressableRepositoryItem item) => Key == item.Key;
    }
}
