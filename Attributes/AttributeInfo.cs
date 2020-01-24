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
    [Serializable]
    public sealed class AttributeInfo : ICopyable<AttributeInfo>, IEquatable<AttributeInfo>
    {
        public string Name;
        [HideInInspector]
        public object DefaultValue;
        public AttributeType Type;

#if UNITY_EDITOR
#pragma warning disable CS0414, CS0649

        [ShowInInspector, SerializeField, ShowIf("IsBoolean"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private bool _boolValue = false;
        public bool IsBoolean() => Type == AttributeType.Bool;

        [ShowInInspector, SerializeField, ShowIf("IsString"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private string _stringValue = string.Empty;
        public bool IsString() => Type == AttributeType.String;

        [ShowInInspector, SerializeField, ShowIf("IsInt"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private int _intValue = 0;
        public bool IsInt() => Type == AttributeType.Int;

        [ShowInInspector, SerializeField, ShowIf("IsFloat"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private float _floatValue = 0.0f;
        public bool IsFloat() => Type == AttributeType.Float;

        [ShowInInspector, SerializeField, ShowIf("IsVector2"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private Vector2 _vector2Value = Vector2.zero;
        public bool IsVector2() => Type == AttributeType.Vector2;

        [ShowInInspector, SerializeField, ShowIf("IsVector3"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private Vector3 _vector3Value = Vector3.zero;
        public bool IsVector3() => Type == AttributeType.Vector3;

        [ShowInInspector, SerializeField, ShowIf("IsColor"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private Color _colorValue = Color.white;
        public bool IsColor() => Type == AttributeType.Color;

        [ShowInInspector, SerializeField, ShowIf("IsIntRangedValue"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private IntRangedValue _intRangedValue = new IntRangedValue();
        public bool IsIntRangedValue() => Type == AttributeType.IntRangedValue;

        [ShowInInspector, SerializeField, ShowIf("IsFloatRangedValue"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private FloatRangedValue _floatRangedValue = new FloatRangedValue();
        public bool IsFloatRangedValue() => Type == AttributeType.FloatRangedValue;

        [ShowInInspector, SerializeField, ShowIf("IsIntStat"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private IntStat _intStatValue = new IntStat();
        public bool IsIntStat() => Type == AttributeType.IntStat;

        [ShowInInspector, SerializeField, ShowIf("IsFloatStat"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private FloatStat _floatStatValue = new FloatStat();
        public bool IsFloatStat() => Type == AttributeType.FloatStat;

        [ShowInInspector, ShowIf("NotSupportedType"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel, ReadOnly]
        private string _notSupportedType = "Not supported Type";
        public bool NotSupportedType() => !IsBoolean() && !IsString() && !IsInt() && !IsFloat() && !IsVector2() && !IsVector3() && !IsColor() && !IsIntRangedValue() && !IsFloatRangedValue() && !IsIntStat() && !IsFloatStat();

#pragma warning restore CS0414, CS0649
#endif

        public AttributeInfo() { }

        public AttributeInfo(string name, object defaultValue, AttributeType type)
        {
            Name = name;
            DefaultValue = defaultValue;
            Type = type;
#if UNITY_EDITOR
            _boolValue = false;
            _stringValue = string.Empty;
            _intValue = 0;
            _floatValue = 
                +0.0f;
            _vector2Value = Vector2.zero;
            _vector3Value = Vector3.zero;
            _colorValue = Color.white;
            _notSupportedType = "Not supported type.";
#endif
        }

        public AttributeInfo Copy() => new AttributeInfo(Name, DefaultValue, Type);

        public bool Equals(AttributeInfo other) => Name == other.Name;
        public override int GetHashCode() => Name.GetHashCode();
    }
}
