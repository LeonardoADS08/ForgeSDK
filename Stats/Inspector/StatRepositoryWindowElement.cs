using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ForgeSDK.Stats.Inspector
{
    public class StatRepositoryWindowElement
    {
        [HideLabel]
        public string Name;
        
        [HideInInspector]
        public EventHandler<string> Updated;

        private bool _new;
        private string _originalName;

        public bool ExistsInRepository => StatRepository.Instance.Exists(_originalName);


        public StatRepositoryWindowElement() 
        {
            _new = true;
        }

        public StatRepositoryWindowElement(string name)
        {
            Name = name;
            _originalName = name;
            _new = false;
        }

        [ButtonGroup("Tools"), Button("Add"), ShowIf("_new")]
        public void Add()
        {
            var repo = StatRepository.Instance;
            repo.Add(Name);
            _new = !repo.Save();
            if (!_new) _originalName = Name;
        }

        [ButtonGroup("Tools"), Button("Update"), HideIf("_new")]
        public void Update()
        {
            var repo = StatRepository.Instance;
            var result = repo.Update(item => item == _originalName, item => item = Name);
            repo.Save();
            if (result != 0) _originalName = Name;
        }

        [ButtonGroup("Tools"), ShowIf("ExistsInRepository"), Button("Remove")]
        public void Remove()
        {
            var repo = StatRepository.Instance;
            repo.Remove(_originalName);
            repo.Save();
            Updated?.Invoke(this, _originalName);
        }
    }
}
