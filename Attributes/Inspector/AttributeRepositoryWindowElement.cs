using ForgeSDK.Stats;
using ForgeSDK.Structures.Values;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ForgeSDK.Attributes.Inspector
{
    public class AttributeRepositoryWindowElement
    {
        [HideLabel]
        public string Name;

        public AttributeType Type;

        [HideInInspector]
        public object Value;

#if UNITY_EDITOR
#pragma warning disable CS0414, CS0649

        [ShowInInspector, SerializeField, ShowIf("IsBoolean"), HorizontalGroup(GroupID = "Default value", Order = 0), HideLabel]
        private bool _boolValue = false;
        public bool IsBoolean() => Type == AttributeType.Bool;

        [ShowInInspector, SerializeField, ShowIf("IsString"), HorizontalGroup(GroupID = "Default value", Order = 0), HideLabel]
        private string _stringValue = string.Empty;
        public bool IsString() => Type == AttributeType.String;

        [ShowInInspector, SerializeField, ShowIf("IsInt"), HorizontalGroup(GroupID = "Default value", Order = 0), HideLabel]
        private int _intValue = 0;
        public bool IsInt() => Type == AttributeType.Int;

        [ShowInInspector, SerializeField, ShowIf("IsFloat"), HorizontalGroup(GroupID = "Default value", Order = 0), HideLabel]
        private float _floatValue = 0.0f;
        public bool IsFloat() => Type == AttributeType.Float;

        [ShowInInspector, SerializeField, ShowIf("IsVector2"), HorizontalGroup(GroupID = "Default value", Order = 0), HideLabel]
        private Vector2 _vector2Value = Vector2.zero;
        public bool IsVector2() => Type == AttributeType.Vector2;

        [ShowInInspector, SerializeField, ShowIf("IsVector3"), HorizontalGroup(GroupID = "Default value", Order = 0), HideLabel]
        private Vector3 _vector3Value = Vector3.zero;
        public bool IsVector3() => Type == AttributeType.Vector3;

        [ShowInInspector, SerializeField, ShowIf("IsColor"), HorizontalGroup(GroupID = "Default value", Order = 0), HideLabel]
        private Color _colorValue = Color.white;
        public bool IsColor() => Type == AttributeType.Color;

        [ShowInInspector, SerializeField, ShowIf("IsIntRangedValue"), HorizontalGroup(GroupID = "Default value", Order = 0), HideLabel, InlineProperty]
        private IntRangedValue _intRangedValue = new IntRangedValue();
        public bool IsIntRangedValue() => Type == AttributeType.IntRangedValue;

        [ShowInInspector, SerializeField, ShowIf("IsFloatRangedValue"), HorizontalGroup(GroupID = "Default value", Order = 0), HideLabel, InlineProperty]
        private FloatRangedValue _floatRangedValue = new FloatRangedValue();
        public bool IsFloatRangedValue() => Type == AttributeType.FloatRangedValue;

        [ShowInInspector, SerializeField, ShowIf("IsStat"), HorizontalGroup(GroupID = "Default value", Order = 0), HideLabel, InlineProperty]
        private Stat _statValue = new Stat();
        public bool IsStat() => Type == AttributeType.Stat;

        [ShowInInspector, ShowIf("NotSupportedType"), HorizontalGroup(GroupID = "Default value", Order = 0), HideLabel, ReadOnly]
        private string _notSupportedType = "Not supported Type";
        public bool NotSupportedType() => !IsBoolean() && !IsString() && !IsInt() && !IsFloat() && !IsVector2() && !IsVector3() && !IsColor() && !IsIntRangedValue() && !IsFloatRangedValue() && !IsStat();

#pragma warning restore CS0414, CS0649
#endif

        [HideInInspector]
        public EventHandler<AttributeInfo> Updated;

        private bool _new;
        private AttributeInfo _originalAtttribute;

        public bool ExistsInRepository => AttributeRepository.Instance.Exists(attribute => attribute.Name == _originalAtttribute.Name);


        public AttributeRepositoryWindowElement()
        {
            _new = true;
        }

        public AttributeRepositoryWindowElement(AttributeInfo attribute)
        {
            Name = attribute.Name;
            Type = attribute.Type;
            _originalAtttribute = attribute;
            _new = false;
        }

        private void UpdateDefaultValue()
        {
            switch (Type)
            {
                case AttributeType.Bool:
                    Value = _boolValue;
                    break;
                case AttributeType.String:
                    Value = _stringValue;
                    break;
                case AttributeType.Int:
                    Value = _intValue;
                    break;
                case AttributeType.Float:
                    Value = _floatValue;
                    break;
                case AttributeType.Vector2:
                    Value = _vector2Value;
                    break;
                case AttributeType.Vector3:
                    Value = _vector3Value;
                    break;
                case AttributeType.Color:
                    Value = _colorValue;
                    break;
                case AttributeType.IntRangedValue:
                    Value = _intRangedValue;
                    break;
                case AttributeType.FloatRangedValue:
                    Value = _floatRangedValue;
                    break;
                case AttributeType.Stat:
                    Value = _statValue;
                    break;
                default:
                    break;
            }
        }

        [ButtonGroup("Tools"), Button("Add"), ShowIf("_new")]
        public void Add()
        {
            UpdateDefaultValue();
            var newAttribute = new AttributeInfo(Name, Value, Type);
            var repo = AttributeRepository.Instance;
            repo.Add(newAttribute);
            _new = !repo.Save();
            if (!_new) _originalAtttribute = newAttribute;
        }

        [ButtonGroup("Tools"), Button("Update"), HideIf("_new")]
        public void Update()
        {
            UpdateDefaultValue();
            var repo = AttributeRepository.Instance;
            var result = repo.Update(item => item.Name == _originalAtttribute.Name, item => item.Name = Name);
            repo.Save();
            if (result != 0) _originalAtttribute = new AttributeInfo(Name, Value, Type);
        }

        [ButtonGroup("Tools"), ShowIf("ExistsInRepository"), Button("Remove")]
        public void Remove()
        {
            var repo = AttributeRepository.Instance;
            repo.Remove(attribute => attribute.Name == _originalAtttribute.Name);
            repo.Save();
            Updated?.Invoke(this, _originalAtttribute);
        }
    }
}
