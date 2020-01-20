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

    public sealed class AttributeSystem : MonoBehaviour
    {
#if UNITY_EDITOR
        private IEnumerable<string> _availableAttributes => AttributeRepository.Instance.GetAllElements()
                                                                                        .Select(attribute => attribute.Name)
                                                                                        .Except(Attributes.Select(attribute => attribute.AttributeInfo.Name));

        [ShowInInspector, HideLabel, ValueDropdown("_availableAttributes", DropdownTitle = "Attributes availables"), TabGroup("New attribute", order: 1)]
        private string _newAttribute = string.Empty;

        [Button("Add Attribute"), TabGroup("New attribute", order: 1)]
        private void AddAttribute()
        {
            var attributes = AttributeRepository.Instance.GetAllElements().ToList();
            if (!string.IsNullOrWhiteSpace(_newAttribute) && !Attributes.Exists(attribute => attribute.AttributeInfo.Name == _newAttribute))
            {
                Attributes.Add(new Attribute(attributes.Find(x => x.Name == _newAttribute)));
                _newAttribute = string.Empty;
            }
        }
#endif

        private Dictionary<string, Attribute> _attributes = new Dictionary<string, Attribute>();

        [TabGroup("Attribute", order: 0), TableList(AlwaysExpanded = true)]
        public List<Attribute> Attributes = new List<Attribute>();

        public T GetAttribute<T>(string attributeName)
        {
            if (_attributes.ContainsKey(attributeName))
            {
#if UNITY_EDITOR
                if (typeof(T) != _attributes[attributeName].Value.GetType())
                {
                    string message = string.Format("Trying to get attribute: {0} of type {1} as a {2} on: {3}", attributeName, typeof(T).Name, _attributes[attributeName].Value.GetType().Name, gameObject.name);
                    Debug.LogError(message);
                    Logs.SaveLog(message, Logs.GetDirection(), logName: "Editor debugging");
                }
#endif
                return (T)_attributes[attributeName].Value;
            }
            else
            {
                string message = string.Format("Trying to get attribute: {0}, but it doesn't exists on this attribute system: {1}", attributeName, gameObject.name);
#if UNITY_EDITOR
                Debug.LogError(message);
#else
                Logs.SaveLog(message, Logs.GetDirection());
#endif
                return default(T);
            }
        }

    }
}
