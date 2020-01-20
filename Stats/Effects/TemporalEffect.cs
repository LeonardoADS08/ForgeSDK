using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Stats.Effects
{
    public class TemporalEffect<T> : Effect<T>
    {
        public TemporalEffect(T value, float time) : base(value, time) { }

        protected override T Variation(float time, float deltaTime)
        {
            if (_elapsedTime > Time) return Value;
            else return default(T);
        }

    }
}
