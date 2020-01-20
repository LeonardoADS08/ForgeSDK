using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Stats
{
    public interface IStat<T>
    {
        void ApplyEffect(IEffect<T> effect);
        void UpdateEffects(float time, float deltaTime);
    }
}
