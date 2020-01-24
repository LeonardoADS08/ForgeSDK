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
    public abstract class LanguageRepository : IEnumerableRepository<Language>
    {
        protected HashSet<Language> _languages = new HashSet<Language>();

        protected override IEnumerable<Language> _items => _languages;
        protected override string _fileLocation => Path.Combine(Application.streamingAssetsPath, "Languages", _fileName);

        public LanguageRepository()
        {
            Load();
        }

        public override bool Add(Language element)
        {
            if (!_languages.Contains(element))
            {
                _languages.Add(element);
                return true;
            }
            else return false;
        }

        public override bool Remove(Language element)
        {
            if (_languages.Contains(element))
            {
                _languages.Remove(element);
                return true;
            }
            else return false;
        }

        protected override bool AreEquals(Language a, Language b) => a.Equals(b);
    }
}
