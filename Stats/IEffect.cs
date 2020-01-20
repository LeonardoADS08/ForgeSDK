using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Stats
{
    public interface IEffect<T>
    {
        EffectStatus Status { get; }
        EffectType Type { get; }
        T Tick(float time, float deltaTime);
        void Start();
        void Stop();
        EventHandler Started { get; set; }
        EventHandler Finished { get; set; }
    }
}
