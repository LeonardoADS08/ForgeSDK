#if UNITY_EDITOR
using ForgeSDK.Localization.Repositories;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace ForgeSDK.Localization.Inspector
{
    internal sealed class LocalizationRepositoryWindow : OdinEditorWindow
    {
        private static LocalizationRepositoryWindow _instance;

        [MenuItem("ForgeSDK/Localization")]
        public static void ShowWindow()
        {
            _instance = GetWindow<LocalizationRepositoryWindow>("Attribute Repository");
            _instance.maxSize = new Vector2(1200, 600);
            _instance.minSize = _instance.maxSize;

            _instance.Language = _instance.ExistantLanguages.First().Text;
            _instance.LoadData();
        }

        [HideInInspector]
        public IEnumerable<ValueDropdownItem> ExistantLanguages => LocalizationSystem.Instance
                                                                   .LanguageRepository
                                                                   .GetAllElements()
                                                                   .Select(l => new ValueDropdownItem(l.Name, l.Code));

        [ValueDropdown("ExistantLanguages"), TabGroup("Localization"), OnValueChanged("ChangeLanguage")]
        public string Language;

        [TableList, TabGroup("Localization")]
        public List<LocalizableStringElement> Elements = new List<LocalizableStringElement>();

        [TableList, TabGroup("Languages")]
        public List<LanguageElement> Languages = new List<LanguageElement>();

        [TableList, TabGroup("Codes")]
        public List<LocalizableCodeElement> Codes = new List<LocalizableCodeElement>();

        public void ChangeLanguage()
        {
            LocalizationSystem.Instance.ChangeLanguage(Language);
            Refresh();
        }

        [Button("Refresh")]
        public void Refresh()
        {
            LoadData();
            Repaint();
        }

        [Button("Reload")]
        public void Reload()
        {
            LocalizationSystem.PurgeInstance();
            Refresh();
        }

        public void LoadData()
        {
            Elements = LocalizationSystem.Instance.LocalizationRepository.GetAllElements().Select(l => new LocalizableStringElement(l)).ToList();
            Languages = LocalizationSystem.Instance.LanguageRepository.GetAllElements().Select(l => new LanguageElement(l)).ToList();
            Codes = LocalizationSystem.Instance.LocalizableCodeRepository.GetAllElements().Select(l => new LocalizableCodeElement(l)).ToList();
        }
    }
}

#endif
