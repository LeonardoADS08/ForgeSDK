using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Attributes
{
    public enum AttributeType
    {
        Bool = 0,
        String = 1,
        Int = 2,
        Float = 3,
        Vector2 = 4,
        Vector3 = 5,
        Color = 6,
        IntRangedValue = 7,
        FloatRangedValue = 8,
        FloatStat = 9,
        IntStat = 10,
        Unknown = 11
    }
}
