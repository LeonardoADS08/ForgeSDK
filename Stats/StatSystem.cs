using ForgeSDK.Structures.Values;
using ForgeSDK.Tools;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ForgeSDK.Stats
{
    public class StatSystem : MonoBehaviour
    {
#if UNITY_EDITOR
        private IEnumerable<string> _availableStats => StatRepository.Instance.GetAllElements().Except(Stats.Select(t => t.Name));

        [ShowInInspector, HideLabel, ValueDropdown("_availableStats", DropdownTitle = "Stats availables"), TabGroup("New stat", order: 1)]
        private string _newStat = string.Empty;

        [Button("Add Stat"), TabGroup("New stat", order: 1)]
        private void AddStat()
        {
            if (!string.IsNullOrWhiteSpace(_newStat) && !Stats.Exists(stat => stat.Name == _newStat))
            {
                Stats.Add(new Stat(_newStat));
                _newStat = string.Empty;
            }
        }
#endif

        private Dictionary<string, Stat> _stats = new Dictionary<string, Stat>();

        [TabGroup("Stats", order: 0), TableList(AlwaysExpanded = true)]
        public List<Stat> Stats = new List<Stat>();

        public StatSystem() { }

        public StatSystem(List<Stat> stats)
        {
            stats.ForEach(stat => _stats.Add(stat.Name, stat));
        }

        private void Update()
        {
            foreach (var stat in _stats.Values)
            {
                stat.UpdateEffects(Time.time, Time.deltaTime);
            }
        }

        public Stat GetStat(string statName)
        {
            if (_stats.ContainsKey(statName)) return _stats[statName];
            else
            {
                string message = string.Format("Trying to get stat: {0}, but it doesn't exists on this stat system: {1}", statName, gameObject.name);
#if UNITY_EDITOR
                Debug.LogError(message);
#else
                Logs.SaveLog(message, Logs.GetDirection());
#endif
                return null;
            }
        }

    }
}
