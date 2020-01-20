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
    public sealed class Attribute : ISerializationCallbackReceiver
    {
#if UNITY_EDITOR
        [ShowInInspector, DisplayAsString, LabelWidth(70)]
        private string _name;

        [ShowInInspector, DisplayAsString, LabelWidth(50)]
        private AttributeType _type;
#endif
        [HideInInspector]
        public AttributeInfo AttributeInfo;
        public object Value;


#if UNITY_EDITOR
#pragma warning disable CS0414, CS0649

        [ShowInInspector, SerializeField, ShowIf("IsBoolean"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private bool _boolValue = false;
        public bool IsBoolean() => AttributeInfo.Type == AttributeType.Bool;

        [ShowInInspector, SerializeField, ShowIf("IsString"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private string _stringValue = string.Empty;
        public bool IsString() => AttributeInfo.Type == AttributeType.String;

        [ShowInInspector, SerializeField, ShowIf("IsInt"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private int _intValue = 0;
        public bool IsInt() => AttributeInfo.Type == AttributeType.Int;

        [ShowInInspector, SerializeField, ShowIf("IsFloat"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private float _floatValue = 0.0f;
        public bool IsFloat() => AttributeInfo.Type == AttributeType.Float;

        [ShowInInspector, SerializeField, ShowIf("IsVector2"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private Vector2 _vector2Value = Vector2.zero;
        public bool IsVector2() => AttributeInfo.Type == AttributeType.Vector2;

        [ShowInInspector, SerializeField, ShowIf("IsVector3"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private Vector3 _vector3Value = Vector3.zero;
        public bool IsVector3() => AttributeInfo.Type == AttributeType.Vector3;

        [ShowInInspector, SerializeField, ShowIf("IsColor"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private Color _colorValue = Color.white;
        public bool IsColor() => AttributeInfo.Type == AttributeType.Color;

        [ShowInInspector, SerializeField, ShowIf("IsIntRangedValue"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private IntRangedValue _intRangedValue = new IntRangedValue();
        public bool IsIntRangedValue() => AttributeInfo.Type == AttributeType.IntRangedValue;

        [ShowInInspector, SerializeField, ShowIf("IsFloatRangedValue"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private FloatRangedValue _floatRangedValue = new FloatRangedValue();
        public bool IsFloatRangedValue() => AttributeInfo.Type == AttributeType.FloatRangedValue;

        [ShowInInspector, SerializeField, ShowIf("IsIntStat"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private IntStat _intStatValue = new IntStat();
        public bool IsIntStat() => AttributeInfo.Type == AttributeType.IntStat;

        [ShowInInspector, SerializeField, ShowIf("IsFloatStat"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private FloatStat _floatStatValue = new FloatStat();
        public bool IsFloatStat() => AttributeInfo.Type == AttributeType.FloatStat;

        [ShowInInspector, ShowIf("NotSupportedType"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel, ReadOnly]
        private string _notSupportedType = "Not supported Type";
        public bool NotSupportedType() => !IsBoolean() && !IsString() && !IsInt() && !IsFloat() && !IsVector2() && !IsVector3() && !IsColor() && !IsIntRangedValue() && !IsFloatRangedValue() && !IsIntStat() && !IsFloatStat();

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
#if UNITY_EDITOR
            if (AttributeInfo != null)
            {
                _name = AttributeInfo.Name;
                _type = AttributeInfo.Type;
            }
#endif
        }

#pragma warning restore CS0414, CS0649
#endif


        public Attribute(AttributeInfo attributeInfo)
        {
            AttributeInfo = attributeInfo.Copy();
            Value = AttributeInfo.DefaultValue;
        }
    }
}
