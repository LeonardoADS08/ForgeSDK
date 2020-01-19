using ForgeSDK.Stats;
using ForgeSDK.Structures.Values;
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
    public class Attribute
    {
#if UNITY_EDITOR
        [ShowInInspector, ReadOnly]
        private string _name;
        [ShowInInspector, ReadOnly]
        private AttributeType _type;
#endif

        public object Value;

        [ReadOnly]
        public AttributeInfo AttributeInfo { get; private set; }

#if UNITY_EDITOR
#pragma warning disable CS0414, CS0649

        [ShowInInspector, SerializeField, ShowIf("IsBoolean"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private bool _boolValue = false;
        public bool IsBoolean() => _type == AttributeType.Bool;

        [ShowInInspector, SerializeField, ShowIf("IsString"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private string _stringValue = string.Empty;
        public bool IsString() => _type == AttributeType.String;

        [ShowInInspector, SerializeField, ShowIf("IsInt"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private int _intValue = 0;
        public bool IsInt() => _type == AttributeType.Int;

        [ShowInInspector, SerializeField, ShowIf("IsFloat"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private float _floatValue = 0.0f;
        public bool IsFloat() => _type == AttributeType.Float;

        [ShowInInspector, SerializeField, ShowIf("IsVector2"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private Vector2 _vector2Value = Vector2.zero;
        public bool IsVector2() => _type == AttributeType.Vector2;

        [ShowInInspector, SerializeField, ShowIf("IsVector3"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private Vector3 _vector3Value = Vector3.zero;
        public bool IsVector3() => _type == AttributeType.Vector3;

        [ShowInInspector, SerializeField, ShowIf("IsColor"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private Color _colorValue = Color.white;
        public bool IsColor() => _type == AttributeType.Color;

        [ShowInInspector, SerializeField, ShowIf("IsIntRangedValue"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private IntRangedValue _intRangedValue = new IntRangedValue();
        public bool IsIntRangedValue() => _type == AttributeType.IntRangedValue;


        [ShowInInspector, SerializeField, ShowIf("IsFloatRangedValue"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private FloatRangedValue _floatRangedValue = new FloatRangedValue();
        public bool IsFloatRangedValue() => _type == AttributeType.FloatRangedValue;

        [ShowInInspector, SerializeField, ShowIf("IsStat"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private Stat _statValue = new Stat();
        public bool IsStat() => _type == AttributeType.Stat;

        [ShowInInspector, ShowIf("NotSupportedType"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel, ReadOnly]
        private string _notSupportedType = "Not supported Type";
        public bool NotSupportedType() => !IsBoolean() && !IsString() && !IsInt() && !IsFloat() && !IsVector2() && !IsVector3() && !IsColor() && !IsIntRangedValue() && !IsFloatRangedValue() && !IsStat();

#pragma warning restore CS0414, CS0649
#endif


        public Attribute(AttributeInfo attributeInfo)
        {
            _name = attributeInfo.Name;
            _type = attributeInfo.Type;
            Value = attributeInfo.DefaultValue;
            AttributeInfo = attributeInfo;
        }
    }
}
