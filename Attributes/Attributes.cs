using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Attributes
{
    public class Attributes
    {
        public static List<AttributeInfo> ATTRIBUTES => new List<AttributeInfo>() 
        {
            BOOL_IS_INVULNERABLE,
            BOOL_IS_SHOOTING
        };
        
        public static readonly AttributeInfo BOOL_IS_INVULNERABLE = new AttributeInfo("Invulnerable", AttributeType.Bool);
        public static readonly AttributeInfo BOOL_IS_SHOOTING = new AttributeInfo("Is shooting", AttributeType.Bool);

    }
}
