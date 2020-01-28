using ForgeSDK.Stats;
using ForgeSDK.Structures.Values;
using ForgeSDK.Tools;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ForgeSDK.Attributes
{
    [Serializable, InlineProperty]
    public sealed class AttributeInfo : ICopyable<AttributeInfo>, IEquatable<AttributeInfo>
    {
        public string Name = string.Empty;
        public AttributeType Type = AttributeType.Bool;

        public AttributeInfo() { }

        public AttributeInfo(string name, AttributeType type)
        {
            Name = name;
            Type = type;
        }

        public AttributeInfo Copy() => new AttributeInfo(Name, Type);

        public bool Equals(AttributeInfo other) => Name == other.Name;

        public override int GetHashCode() => Name.GetHashCode();
    }
}
