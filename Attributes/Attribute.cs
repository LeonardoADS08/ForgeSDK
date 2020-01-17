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
        [ReadOnly]
        public string Name;

#if UNITY_EDITOR
        [PropertyTooltip(TOOLTIP)]
#endif
        public object Value;

        [ReadOnly]
        public AttributeInfo AttributeInfo { get; private set; }

#if UNITY_EDITOR
#pragma warning disable CS0414
        private const string TOOLTIP = "Format for types:\n" +
                                       "Bool: true - false\n" +
                                       "String: \"[TEXT]\"\n" +
                                       "Int: i[NUMBER]\n" +
                                       "Float: f[NUMBER]\n" +
                                       "Vector2: v2[NUMBER, NUMBER]\n" +
                                       "Vector3: v3[NUMBER,NUMBER,NUMBER]\n" +
                                       "Color: c[NUMBER, NUMBER, NUMBER, NUMBER]\n";

        [ShowInInspector, ShowIf("IsBoolean"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private bool _boolValue;
        public bool IsBoolean() => Value != null && Value.GetType() == typeof(bool);

        [ShowInInspector, ShowIf("IsString"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private string _stringValue;
        public bool IsString() => Value != null && Value.GetType() == typeof(string);

        [ShowInInspector, ShowIf("IsInt"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private int _intValue;
        public bool IsInt() => Value != null && Value.GetType() == typeof(int);

        [ShowInInspector, ShowIf("IsFloat"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private float _floatValue;
        public bool IsFloat() => Value != null && Value.GetType() == typeof(float);

        [ShowInInspector, ShowIf("IsVector2"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private Vector2 _vector2Value;
        public bool IsVector2() => Value != null && Value.GetType() == typeof(Vector2);

        [ShowInInspector, ShowIf("IsVector3"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private Vector3 _vector3Value;
        public bool IsVector3() => Value != null && Value.GetType() == typeof(Vector3);

        [ShowInInspector, ShowIf("IsColor"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel]
        private Color _colorValue;
        public bool IsColor() => Value != null && Value.GetType() == typeof(Color);

        [ShowInInspector, ShowIf("NotSupportedType"), HorizontalGroup(GroupID = "Value", Order = 0), HideLabel, ReadOnly]
        private string _notSupportedType;
        public bool NotSupportedType() => !IsBoolean() && !IsString() && !IsInt() && !IsFloat() && !IsVector2() && !IsVector3() && !IsColor();

#pragma warning restore CS0414 
#endif

        public Attribute(string name, object value, AttributeInfo _attributeInfo)
        {
            Name = name;
            Value = value;
            AttributeInfo = _attributeInfo;
#if UNITY_EDITOR
            _boolValue = false;
            _stringValue = string.Empty;
            _intValue = 0;
            _floatValue = 0.0f;
            _vector2Value = Vector2.zero;
            _vector3Value = Vector3.zero;
            _colorValue = Color.white;
            _notSupportedType = "Not supported type.";
#endif
        }
    }
}
