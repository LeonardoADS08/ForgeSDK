using ForgeSDK.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Localization
{
    [Serializable]
    public class LocalizableString : ICopyable<LocalizableString>, IEquatable<LocalizableString>
    {
        public string Code;
        public string Value;

        public LocalizableString()
        {
            Code = Value = string.Empty;
        }
        public LocalizableString(string code, string value)
        {
            Code = code;
            Value = value;
        }

        public LocalizableString Copy() => new LocalizableString(Code, Value);

        public bool Equals(LocalizableString other) => other.Code == Code;

        public override int GetHashCode() => Code.GetHashCode();
    }
}
