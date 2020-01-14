using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Stats.Effects
{
    public class TemporalEffect : Effect
    {
        public TemporalEffect(float value, float time) : base(value, time) { }

        protected override float Variation(float time, float deltaTime)
        {
            if (_elapsedTime > Time) return Value;
            else return 0.0f;
        }
    }
}
