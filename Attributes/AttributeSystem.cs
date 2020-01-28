using ForgeSDK.Stats;
using ForgeSDK.Structures.Values;
using ForgeSDK.Tools;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ForgeSDK.Attributes
{

    public sealed class AttributeSystem : SerializedMonoBehaviour
    {
#if UNITY_EDITOR
        private IEnumerable<string> _availableAttributes => AttributeRepository.Instance.GetAllElements()
                                                                                        .Except(_attributes.Values)
                                                                                        .Select(att => att.Name);

        [ShowInInspector, HideLabel, ValueDropdown("_availableAttributes", DropdownTitle = "Attributes availables"), TabGroup("New attribute", order: 1)]
        private string _newAttribute = "Select an attribute";

        [Button("Add Attribute"), TabGroup("New attribute", order: 1)]
        private void AddAttribute()
        {
            var attributes = AttributeRepository.Instance.GetAllElements().ToList();
            var attributeInfo = AttributeRepository.Instance.GetElement(att => att.Name == _newAttribute);
            if (attributeInfo != null && !_attributes.ContainsKey(_newAttribute))
            {
                AddAttribute(attributeInfo);
                _newAttribute = "Select an attribute";
            }
        }
#endif

        private string DATA_PATH => Path.Combine(Application.streamingAssetsPath, "Attributes", string.Format("{0}_{1}.att", gameObject.name, gameObject.GetHashCode()));

        [SerializeField, ShowIf("BoolAttributes"), TabGroup("Attributes"), TableList(AlwaysExpanded = true)]
        private List<Attribute<bool>> _boolAttributes = new List<Attribute<bool>>();
        private bool BoolAttributes => _boolAttributes.Count != 0;

        [SerializeField, ShowIf("StringAttributes"), TabGroup("Attributes"), TableList(AlwaysExpanded = true)]
        private List<Attribute<string>> _stringAttributes = new List<Attribute<string>>();
        private bool StringAttributes => _stringAttributes.Count != 0;

        [SerializeField, ShowIf("IntAttributes"), TabGroup("Attributes"), TableList(AlwaysExpanded = true)]
        private List<Attribute<int>> _intAttributes = new List<Attribute<int>>();
        private bool IntAttributes  => _intAttributes.Count != 0;

        [SerializeField, ShowIf("FloatAttributes"), TabGroup("Attributes"), TableList(AlwaysExpanded = true)]
        private List<Attribute<float>> _floatAttributes = new List<Attribute<float>>();
        private bool FloatAttributes  => _floatAttributes.Count != 0;

        [SerializeField, ShowIf("Vector2Attributes"), TabGroup("Attributes"), TableList(AlwaysExpanded = true)]
        private List<Attribute<Vector2>> _vector2Attributes = new List<Attribute<Vector2>>();
        private bool Vector2Attributes  => _vector2Attributes.Count != 0;

        [SerializeField, ShowIf("Vector3Attributes"), TabGroup("Attributes"), TableList(AlwaysExpanded = true)]
        private List<Attribute<Vector3>> _vector3Attributes = new List<Attribute<Vector3>>();
        private bool Vector3Attributes  => _vector3Attributes.Count != 0;

        [SerializeField, ShowIf("ColorAttributes"), TabGroup("Attributes"), TableList(AlwaysExpanded = true)]
        private List<Attribute<Color>> _colorAttributes = new List<Attribute<Color>>();
        private bool ColorAttributes  => _colorAttributes.Count != 0;

        [SerializeField, ShowIf("IntRangedValueAttributes"), TabGroup("Attributes"), TableList(AlwaysExpanded = true)]
        private List<Attribute<IntRangedValue>> _intRangedValueAttributes = new List<Attribute<IntRangedValue>>();
        private bool IntRangedValueAttributes  => _intRangedValueAttributes.Count != 0;

        [SerializeField, ShowIf("FloatRangedValueAttributes"), TabGroup("Attributes"), TableList(AlwaysExpanded = true)]
        private List<Attribute<FloatRangedValue>> _floatRangedValueAttributes = new List<Attribute<FloatRangedValue>>();
        private bool FloatRangedValueAttributes  => _floatRangedValueAttributes.Count != 0;

        [SerializeField, ShowIf("IntStatAttributes"), TabGroup("Attributes"), TableList(AlwaysExpanded = true)]
        private List<Attribute<IntStat>> _intStatAttributes = new List<Attribute<IntStat>>();
        private bool IntStatAttributes  => _intStatAttributes.Count != 0;

        [SerializeField, ShowIf("FloatStatAttributes"), TabGroup("Attributes"), TableList(AlwaysExpanded = true)]
        private List<Attribute<FloatStat>> _floatStatAttributes = new List<Attribute<FloatStat>>();
        private bool FloatStatAttributes => _floatStatAttributes.Count != 0;

        [OdinSerialize, HideInInspector]
        private Dictionary<string, AttributeInfo> _attributes = new Dictionary<string, AttributeInfo>();

        public void AddAttribute(AttributeInfo attributeInfo)
        {
            if (!_attributes.ContainsKey(attributeInfo.Name))
            {
                _attributes.Add(attributeInfo.Name, attributeInfo);

                switch (attributeInfo.Type)
                {
                    case AttributeType.Bool:
                        _boolAttributes.Add(new Attribute<bool>(attributeInfo));
                        break;
                    case AttributeType.String:
                        _stringAttributes.Add(new Attribute<string>(attributeInfo));
                        break;
                    case AttributeType.Int:
                        _intAttributes.Add(new Attribute<int>(attributeInfo));
                        break;
                    case AttributeType.Float:
                        _floatAttributes.Add(new Attribute<float>(attributeInfo));
                        break;
                    case AttributeType.Vector2:
                        _vector2Attributes.Add(new Attribute<Vector2>(attributeInfo));
                        break;
                    case AttributeType.Vector3:
                        _vector3Attributes.Add(new Attribute<Vector3>(attributeInfo));
                        break;
                    case AttributeType.Color:
                        _colorAttributes.Add(new Attribute<Color>(attributeInfo));
                        break;
                    case AttributeType.IntRangedValue:
                        _intRangedValueAttributes.Add(new Attribute<IntRangedValue>(attributeInfo));
                        break;
                    case AttributeType.FloatRangedValue:
                        _floatRangedValueAttributes.Add(new Attribute<FloatRangedValue>(attributeInfo));
                        break;
                    case AttributeType.FloatStat:
                        _floatStatAttributes.Add(new Attribute<FloatStat>(attributeInfo));
                        break;
                    case AttributeType.IntStat:
                        _intStatAttributes.Add(new Attribute<IntStat>(attributeInfo));
                        break;
                    default:
                        break;
                }
            }
        }

        public Attribute<T> GetAttribute<T>(string attributeName)
        {
            bool attributeExists = _attributes.ContainsKey(attributeName);
            bool validType = CheckAttribute<T>(attributeName);
            if (attributeExists && validType)
            {
                switch (_attributes[attributeName].Type)
                {
                    case AttributeType.Bool:
                        return (Attribute<T>)((object)_boolAttributes.Find(att => att.AttributeInfo.Name == attributeName));
                    case AttributeType.String:
                        return (Attribute<T>)((object)_stringAttributes.Find(att => att.AttributeInfo.Name == attributeName));
                    case AttributeType.Int:
                        return (Attribute<T>)((object)_intAttributes.Find(att => att.AttributeInfo.Name == attributeName));
                    case AttributeType.Float:
                        return (Attribute<T>)((object)_floatAttributes.Find(att => att.AttributeInfo.Name == attributeName));
                    case AttributeType.Vector2:
                        return (Attribute<T>)((object)_vector2Attributes.Find(att => att.AttributeInfo.Name == attributeName));
                    case AttributeType.Vector3:
                        return (Attribute<T>)((object)_vector3Attributes.Find(att => att.AttributeInfo.Name == attributeName));
                    case AttributeType.Color:
                        return (Attribute<T>)((object)_colorAttributes.Find(att => att.AttributeInfo.Name == attributeName));
                    case AttributeType.IntRangedValue:
                        return (Attribute<T>)((object)_intRangedValueAttributes.Find(att => att.AttributeInfo.Name == attributeName));
                    case AttributeType.FloatRangedValue:
                        return (Attribute<T>)((object)_floatRangedValueAttributes.Find(att => att.AttributeInfo.Name == attributeName));
                    case AttributeType.FloatStat:
                        return (Attribute<T>)((object)_floatStatAttributes.Find(att => att.AttributeInfo.Name == attributeName));
                    case AttributeType.IntStat:
                        return (Attribute<T>)((object)_intStatAttributes.Find(att => att.AttributeInfo.Name == attributeName));
                    default:
                        return null;
                }
            }
            else
            {
                if (!attributeExists)
                {
                    string message = string.Format("Trying to get attribute: {0}, but it doesn't exists on this attribute system: {1}", attributeName, gameObject.name);
                    Debug.LogError(message);
                    Logs.SaveLog(message, Logs.GetDirection(), logName: "Attribute System");
                }
                else if (!validType)
                {
                    string message = string.Format("Trying to cast into an invalid type {0}, when it's {1} on {2}", typeof(T).Name, _attributes[attributeName].Type.ToString(), gameObject.name);
                    Debug.LogError(message);
                    Logs.SaveLog(message, Logs.GetDirection(), logName: "Attribute System");
                }
                return null;
            }
        }

        private bool CheckAttribute<T>(string attributeName)
        {
            if (_attributes.ContainsKey(attributeName))
            {
                Type type = typeof(T);
                switch (_attributes[attributeName].Type)
                {
                    case AttributeType.Bool:
                        return type == typeof(bool);
                    case AttributeType.String:
                        return type == typeof(string);
                    case AttributeType.Int:
                        return type == typeof(int);
                    case AttributeType.Float:
                        return type == typeof(float);
                    case AttributeType.Vector2:
                        return type == typeof(Vector2);
                    case AttributeType.Vector3:
                        return type == typeof(Vector3);
                    case AttributeType.Color:
                        return type == typeof(Color);
                    case AttributeType.IntRangedValue:
                        return type == typeof(IntRangedValue);
                    case AttributeType.FloatRangedValue:
                        return type == typeof(FloatRangedValue);
                    case AttributeType.FloatStat:
                        return type == typeof(FloatStat);
                    case AttributeType.IntStat:
                        return type == typeof(IntStat);
                    default:
                        return false;
                }
            }
            else return false;
        }
    }
}
