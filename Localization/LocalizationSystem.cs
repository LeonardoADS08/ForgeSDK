using ForgeSDK.Localization.Repositories;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Localization
{
    [Serializable, InlineProperty]
    public class LocalizationSystem
    {
        #region Singleton
        private static LocalizationSystem _instance;
        public static LocalizationSystem Instance
        {
            get
            {
                if (_instance == null) _instance = new LocalizationSystem();
                return _instance;
            }
        }

        public static void PurgeInstance() => _instance = null;
        #endregion

        public LanguageRepository LanguageRepository;
        public LocalizableCodeRepository LocalizableCodeRepository;
        public LocalizationRepository LocalizationRepository;

        public EventHandler<string> LanguageChanged;

        public LocalizationSystem()
        {
            LanguageRepository = new JsonLanguageRepository();
            LocalizableCodeRepository = new JsonLocalizableCodeRepository();
            LocalizationRepository = new JsonLocalizationRepository("en_EN", LocalizableCodeRepository);
        }

        public void ChangeLanguage(string languageCode)
        {
            if (LanguageRepository.Exists(l => l.Code == languageCode))
            {
                LocalizationRepository = new JsonLocalizationRepository(languageCode, LocalizableCodeRepository);
                LanguageChanged?.Invoke(this, languageCode);
            }
        }

        public string GetText(string code)
        {
            var result = LocalizationRepository.GetElement(ls => ls.Code == code);
            if (result != null) return result.Value;
            else return "[CODE_NOT_FOUND]";
        }

    }
}
