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
    public class JsonLocalizableCodeRepository : LocalizableCodeRepository
    {
        protected override string _fileName => "localizableCodes.json";

        public JsonLocalizableCodeRepository()
        {
            if (!Directory.Exists(Path.GetDirectoryName(_fileLocation)))
                Directory.CreateDirectory(Path.GetDirectoryName(_fileLocation));
            if (!File.Exists(_fileLocation))
                File.Create(_fileLocation);
        }

        public override bool Save()
        {
            try
            {
                if (!File.Exists(_fileLocation)) return false;

                using (StreamWriter writer = new StreamWriter(_fileLocation, false))
                {
                    string json = JsonConvert.SerializeObject(_localizableCodes);
                    writer.Write(json);
                    writer.Flush();
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
                    var elements = JsonConvert.DeserializeObject<List<LocalizableCode>>(json);
                    if (elements != null)
                    {
                        _localizableCodes = new HashSet<LocalizableCode>(elements);
                        return true;
                    }
                    else
                    {
                        _localizableCodes = new HashSet<LocalizableCode>();
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
