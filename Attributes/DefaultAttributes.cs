using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Attributes
{
    public static partial class DefaultAttributes
    {
        public static List<AttributeInfo> GetAttributes()
        {
            List<AttributeInfo> result = new List<AttributeInfo>();
            Type type = typeof(DefaultAttributes); 
            foreach (var p in type.GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic))
            {
                var attribute = (AttributeInfo)p.GetValue(null);
                if (attribute != null)
                    result.Add(attribute);
            }
            return result;
        }
    }
}
