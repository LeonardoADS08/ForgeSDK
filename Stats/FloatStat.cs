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
    public sealed class FloatStat : FloatRangedValue, IStat<float>
    {
        public List<IEffect<float>> Effects = new List<IEffect<float>>();
        public List<IStatModifier<float>> Modifier = new List<IStatModifier<float>>();

        public FloatStat() : base() { }

        public FloatStat(float value, float minValue, float maxValue) : base(value, minValue, maxValue) { }

        public void UpdateEffects(float time, float deltaTime)
        {
            for (int i = 0; i < Effects.Count; i++)
            {
                var effect = Effects[i];
                switch (effect.Status)
                {
                    case EffectStatus.Ready:
                        effect.Start();
                        break;
                    case EffectStatus.Running:
                        this.ApplyVariation(effect.Tick(time, deltaTime));
                        break;
                    case EffectStatus.Finished:
                        Effects.RemoveAt(i);
                        i--;
                        break;
                    default:
                        break;
                }
            }
        }

        public void ApplyEffect(IEffect<float> effect) => Effects.Add(effect);

        protected override float ModifyValue(float quantity)
        {
            for (int i = 0; i < Modifier.Count; i++)
                quantity = Modifier[i].Modify(quantity);
            return base.ModifyValue(quantity);
        }

    }
}
