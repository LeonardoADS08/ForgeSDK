using ForgeSDK.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ForgeSDK.Localization.Repositories
{
    public abstract class LocalizableCodeRepository : IEnumerableRepository<LocalizableCode>
    {
        protected HashSet<LocalizableCode> _localizableCodes = new HashSet<LocalizableCode>();
        protected override IEnumerable<LocalizableCode> _items => _localizableCodes;

        protected override string _fileLocation => Path.Combine(Application.streamingAssetsPath, "Languages", _fileName);

        public LocalizableCodeRepository()
        {
            Load();
        }

        public override bool Add(LocalizableCode element)
        {
            if (!_localizableCodes.Contains(element))
            {
                _localizableCodes.Add(element);
                return true;
            }
            else return false;
        }

        public override bool Remove(LocalizableCode element)
        {
            if (_localizableCodes.Contains(element))
            {
                _localizableCodes.Remove(element);
                return true;
            }
            else return false;
        }

        protected override bool AreEquals(LocalizableCode a, LocalizableCode b) => a.Equals(b);
    }
}
