using ForgeSDK.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Localization
{
    public class LocalizableCode : ICopyable<LocalizableCode>, IEquatable<LocalizableCode>
    {
        public string Code;

        public LocalizableCode() { }

        public LocalizableCode(string code)
        {
            Code = code;
        }

        public LocalizableCode Copy() => new LocalizableCode(Code);

        public bool Equals(LocalizableCode other) => other.Code == Code;

        public override int GetHashCode() => Code.GetHashCode();

    }
}
