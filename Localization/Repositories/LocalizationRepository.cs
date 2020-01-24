using ForgeSDK.Extensions.Linq;
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
    public abstract class LocalizationRepository : IEnumerableRepository<LocalizableString>
    {

        protected List<LocalizableString> _localizableStrings = new List<LocalizableString>();
        protected override IEnumerable<LocalizableString> _items => _localizableStrings;

        private LocalizableCodeRepository _localizableCodeRepository;
        private string _language;

        protected override string _fileName => _language;
        protected override string _fileLocation => Path.Combine(Application.streamingAssetsPath, "Languages", _fileName);


        public LocalizationRepository(string language, LocalizableCodeRepository localizableCodeRepository)
        {
            _language = language;
            _localizableCodeRepository = localizableCodeRepository;
            Load();
            var elements = _localizableCodeRepository.GetAllElements();
            elements.Where(code => !_localizableStrings.Exists(ls => ls.Code == code.Code))
                    .ForEach(code => _localizableStrings.Add(new LocalizableString(code.Code, "[NOT_DEFINED]")));
            Save();
        }

        public override bool Add(LocalizableString element)
        {
            if (!_localizableStrings.Contains(element))
            {
                _localizableStrings.Add(element);
                return true;
            }
            else return false;
        }

        public override bool Remove(LocalizableString element)
        {
            if (_localizableStrings.Contains(element))
            {
                _localizableStrings.Remove(element);
                return true;
            }
            else return false;
        }

        protected override bool AreEquals(LocalizableString a, LocalizableString b) => a.Equals(b);
    }
}
