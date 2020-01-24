#if UNITY_EDITOR
using ForgeSDK.Localization.Repositories;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Localization.Inspector
{
    [Serializable, InlineProperty]
    public class LanguageElement
    {
        public string Code = string.Empty;
        public string Name = string.Empty;

        public EventHandler<Language> Updated;

        private bool _new = true;
        private Language _original = new Language();

        private LanguageRepository _languageRepository => LocalizationSystem.Instance.LanguageRepository;
        private bool ExistsInRepository => _languageRepository.Exists(l => l.Code == _original.Code);

        public LanguageElement() { }

        public LanguageElement(Language language)
        {
            Code = language.Code;
            Name = language.Name;
            _original = language;
            _new = false;
        }

        [ButtonGroup("Tools"), Button("Add"), ShowIf("_new")]
        public void Add()
        {
            var newLanguage = new Language(Name, Code);
            _languageRepository.Add(newLanguage);
            _new = !_languageRepository.Save();
            if (!_new) _original = newLanguage;
        }

        [ButtonGroup("Tools"), Button("Update"), HideIf("_new")]
        public void Update()
        {
            var result = _languageRepository.Update(item => item.Equals(_original), item =>
            {
                item.Code = Code;
                item.Name = Name;
            });
            _languageRepository.Save();
            if (result != 0) _original = new Language(Name, Code); 
        }

        [ButtonGroup("Tools"), ShowIf("ExistsInRepository"), Button("Remove")]
        public void Remove()
        {
            _languageRepository.Remove(language => language.Equals(_original));
            _languageRepository.Save();
            Updated?.Invoke(this, _original);
        }
    }
}
#endif