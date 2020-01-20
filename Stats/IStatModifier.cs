using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Stats
{
    public interface IStatModifier<T>
    {
        T Modify(T value);
    }
}
