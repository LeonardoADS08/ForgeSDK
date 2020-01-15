using ForgeSDK.Tools;
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
        private Dictionary<string, Stat> _stats = new Dictionary<string, Stat>();
        public List<Stat> Stats => new List<Stat>(_stats.Values);

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
                string message = string.Format("Trying to get stat: {0}, but it doesn't exists on this stat system", statName);
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
