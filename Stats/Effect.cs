using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Stats
{
    public abstract class Effect : IEffect
    {
        public EffectStatus Status { get; private set; } = EffectStatus.Ready;
        public EffectType Type { get; private set; } = EffectType.Neutral;

        public EventHandler Started { get; set; }
        public EventHandler Finished { get; set; }
        public float Time { get; private set; }
        public float Value { get; private set; }

        protected float _elapsedTime = 0.0f;
        
        public Effect(float value, float time)
        {
            Time = time;
            Value = value;
        }

        public void Start()
        {
            Status = EffectStatus.Running;
            Started?.Invoke(this, EventArgs.Empty);
        }

        public void Stop()
        {
            Status = EffectStatus.Finished;
            Finished?.Invoke(this, EventArgs.Empty);
        }

        protected abstract float Variation(float time, float deltaTime);

        public float Tick(float time, float deltaTime)
        {
            _elapsedTime += deltaTime;
            if (_elapsedTime > Time) Stop();
            return Variation(time, deltaTime);
        }
    }
}
