using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Attributes
{
    [Serializable]
    public class AttributeInfo
    {
        public string Name;
        public AttributeType Type;

        public AttributeInfo() { }

        public AttributeInfo(string name, AttributeType type)
        {
            Name = name;
            Type = type;
        }
    }
}
