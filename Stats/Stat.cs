using ForgeSDK.Structures.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Stats
{
    public class Stat : FloatRangedValue
    {
        private List<IEffect> _effects = new List<IEffect>();

        public string Name { get; private set; } = string.Empty;

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
