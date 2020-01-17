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

        [ButtonGroup("Tools"), Button("Add"), ShowIf("_new")]
        public void Add()
        {
            var newAttribute = new AttributeInfo(Name, Type);
            var repo = AttributeRepository.Instance;
            repo.Add(newAttribute);
            _new = !repo.Save();
            if (!_new) _originalAtttribute = newAttribute;
        }

        [ButtonGroup("Tools"), Button("Update"), HideIf("_new")]
        public void Update()
        {
            var repo = AttributeRepository.Instance;
            var result = repo.Update(item => item.Name == _originalAtttribute.Name, item => item.Name = Name);
            repo.Save();
            if (result != 0) _originalAtttribute = new AttributeInfo(Name, Type);
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
