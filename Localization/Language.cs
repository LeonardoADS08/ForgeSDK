using ForgeSDK.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Localization
{
    [Serializable]
    public class Language : ICopyable<Language>, IEquatable<Language>
    {
        public string Code;
        public string Name;

        public Language()
        {
            Name = string.Empty;
            Code = string.Empty;
        }

        public Language(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public Language Copy() => new Language(Name, Code);

        public bool Equals(Language other) => other.Code == Code;

        public override int GetHashCode() => Code.GetHashCode();
    }
}
