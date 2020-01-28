using ForgeSDK.Stats;
using ForgeSDK.Structures.Values;
using ForgeSDK.Tools;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ForgeSDK.Attributes
{
    [Serializable]
    public sealed class Attribute<T> : ISerializationCallbackReceiver, IEquatable<Attribute<T>>
    {
        #region EDITOR
        [ShowInInspector, DisplayAsString, LabelWidth(70)]
        private string _name = string.Empty;

        [ShowInInspector, DisplayAsString, LabelWidth(50)]
        private AttributeType _type;
        #endregion

        [HideInInspector]
        public AttributeInfo AttributeInfo;

        public T Value;

        public void OnBeforeSerialize() 
        {
#if UNITY_EDITOR
            if (AttributeInfo != null)
            {
                _name = AttributeInfo.Name;
                _type = AttributeInfo.Type;
            }
#endif
        }

        public void OnAfterDeserialize()  { }

        public Attribute(AttributeInfo attributeInfo)
        {
            AttributeInfo = attributeInfo.Copy();
            Value = default(T);
        }

        public override int GetHashCode() => AttributeInfo.GetHashCode();

        public bool Equals(Attribute<T> other) => other.AttributeInfo.Equals(AttributeInfo);

    }
}
