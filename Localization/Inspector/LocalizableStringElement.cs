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
    public class LocalizableStringElement
    {
        public string Code = string.Empty;
        public string Value = string.Empty;

        public EventHandler<LocalizableString> Updated;

        private LocalizationRepository _localizationRepository => LocalizationSystem.Instance.LocalizationRepository;
        private bool _new = true;
        private LocalizableString _original = new LocalizableString();

        private bool ExistsInRepository => _localizationRepository.Exists(l => l.Code == _original.Code);

        public LocalizableStringElement()  { }

        public LocalizableStringElement(LocalizableString localizable)
        {
            Code = localizable.Code;
            Value = localizable.Value;
            _original = localizable;
            _new = false;
        }

        [ButtonGroup("Tools"), Button("Add"), ShowIf("_new")]
        public void Add()
        {
            var newLocalizableCode = new LocalizableString(Code, Value);
            _localizationRepository.Add(newLocalizableCode);
            _new = !_localizationRepository.Save();
            if (!_new) _original = newLocalizableCode;
        }

        [ButtonGroup("Tools"), Button("Update"), HideIf("_new")]
        public void Update()
        {
            var result = _localizationRepository.Update(item => item.Equals(_original), item =>
            {
                item.Code = Code;
                item.Value = Value;
            });
            _localizationRepository.Save();
            if (result != 0) _original = new LocalizableString(Code, Value);
        }

        [ButtonGroup("Tools"), ShowIf("ExistsInRepository"), Button("Remove")]
        public void Remove()
        {
            _localizationRepository.Remove(item => item.Equals(_original));
            _localizationRepository.Save();
            Updated?.Invoke(this, _original);
        }
    }
}
#endif