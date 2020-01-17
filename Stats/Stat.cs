using ForgeSDK.Structures.Values;
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
    [Serializable, InlineProperty]
    public class Stat : FloatRangedValue
    {
        [SerializeField, ReadOnly, HideLabel, SuffixLabel("Stat name", true)]
        private string _name = string.Empty;

        public string Name { get => _name; }

        private List<IEffect> _effects = new List<IEffect>();

        public Stat() : base() { }
        public Stat(string name) : base() => _name = name;
        public Stat(float value, float minValue, float maxValue) : base(value, minValue, maxValue) { }
        public Stat(string name, float value, float minValue, float maxValue) : base(value, minValue, maxValue) => _name = name;

        public void UpdateEffects(float time, float deltaTime)
        {
            for (int i = 0; i < _effects.Count; i++)
            {
                var effect = _effects[i];
                switch (effect.Status)
                {
                    case EffectStatus.Ready:
                        effect.Start();
                        break;
                    case EffectStatus.Running:
                        this.ApplyVariation(effect.Tick(time, deltaTime));
                        break;
                    case EffectStatus.Finished:
                        _effects.RemoveAt(i);
                        i--;
                        break;
                    default:
                        break;
                }
            }
        }

        public void ApplyEffect(IEffect effect) => _effects.Add(effect);

    }
}
