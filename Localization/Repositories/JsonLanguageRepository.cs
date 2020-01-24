﻿using ForgeSDK.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace ForgeSDK.Localization.Repositories
{
    public class JsonLanguageRepository : LanguageRepository
    {
        protected override string _fileName => "languages.json";

        public JsonLanguageRepository()
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
                using (StreamWriter writer = new StreamWriter(_fileLocation))
                {
                    string json = JsonConvert.SerializeObject(_languages.ToList());
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
                    var elements = JsonConvert.DeserializeObject<List<Language>>(json);
                    if (elements != null)
                    {
                        _languages = new HashSet<Language>(elements);
                        return true;
                    }
                    else
                    {
                        _languages = new HashSet<Language>();
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
