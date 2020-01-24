using ForgeSDK.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ForgeSDK.Localization.Repositories
{
    public class JsonLocalizationRepository : LocalizationRepository
    {
        public JsonLocalizationRepository(string language, LocalizableCodeRepository localizableCodeRepository) : base(language, localizableCodeRepository)
        {
            if (!Directory.Exists(Path.GetDirectoryName(_fileLocation)))
                Directory.CreateDirectory(Path.GetDirectoryName(_fileLocation));
            if (!File.Exists(_fileLocation))
                File.Create(_fileLocation);
        }

        protected override string _fileName => base._fileName + ".json";

        public override bool Save()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(_fileLocation, false))
                {
                    string json = JsonConvert.SerializeObject(_localizableStrings);
                    writer.Write(json);
                }
                return true;
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.ToString(), Logs.GetDirection());
                Debug.LogError(ex.ToString());
                return false;
            }
        }

        public override bool Load()
        {
            try
            {
                if (!File.Exists(_fileLocation)) return false;

                using (StreamReader reader = new StreamReader(_fileLocation))
                {
                    string json = reader.ReadToEnd();
                    var elements = JsonConvert.DeserializeObject<List<LocalizableString>>(json);
                    if (elements != null)
                    {
                        _localizableStrings = elements;
                        return true;
                    }
                    else
                    {
                        _localizableStrings = new List<LocalizableString>();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.SaveLog(ex.ToString(), Logs.GetDirection());
                Debug.LogError(ex.ToString());
                return false;
            }
        }
    }
}
