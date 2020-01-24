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
    public class LocalizableCodeElement
    {
        public string Code = string.Empty;

        public EventHandler<LocalizableCode> Updated;

        private LocalizableCodeRepository _localizableCodeRepository => LocalizationSystem.Instance.LocalizableCodeRepository;
        private bool _new = true;
        private LocalizableCode _original = new LocalizableCode();

        private bool ExistsInRepository => _localizableCodeRepository.Exists(l => l.Code == _original.Code);

        public LocalizableCodeElement()  { }

        public LocalizableCodeElement(LocalizableCode localizable)
        {
            Code = localizable.Code;
            _original = localizable;
            _new = false;
        }

        [ButtonGroup("Tools"), Button("Add"), ShowIf("_new")]
        public void Add()
        {
            var newLocalizableCode = new LocalizableCode(Code);
            _localizableCodeRepository.Add(newLocalizableCode);
            _new = !_localizableCodeRepository.Save();
            if (!_new) _original = newLocalizableCode;
        }

        [ButtonGroup("Tools"), Button("Update"), HideIf("_new")]
        public void Update()
        {
            var result = _localizableCodeRepository.Update(item => item.Equals(_original), item =>
            {
                item.Code = Code;
            });
            _localizableCodeRepository.Save();
            if (result != 0) _original = new LocalizableCode(Code);
        }

        [ButtonGroup("Tools"), ShowIf("ExistsInRepository"), Button("Remove")]
        public void Remove()
        {
            _localizableCodeRepository.Remove(item => item.Equals(_original));
            _localizableCodeRepository.Save();
            Updated?.Invoke(this, _original);
        }
    }
}
#endif