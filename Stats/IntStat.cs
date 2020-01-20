using ForgeSDK.Structures.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Stats
{
    public sealed class IntStat : IntRangedValue, IStat<int>
    {
        public List<IEffect<int>> Effects = new List<IEffect<int>>();
        public List<IStatModifier<int>> Modifier = new List<IStatModifier<int>>();

        public IntStat() : base() { }

        public IntStat(int value, int minValue, int maxValue) : base(value, minValue, maxValue) { }

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

        public void ApplyEffect(IEffect<int> effect) => Effects.Add(effect);

        protected override int ModifyValue(int quantity)
        {
            for (int i = 0; i < Modifier.Count; i++)
                quantity = Modifier[i].Modify(quantity);
            return base.ModifyValue(quantity);
        }

    }
}
