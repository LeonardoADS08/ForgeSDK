using ForgeSDK.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ForgeSDK.Stats
{
    public class JsonStatRepository : StatRepository
    {
        protected override string _fileName => "stats.json";

        public override bool Save()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(_fileLocation, false))
                {
                    string json = JsonConvert.SerializeObject(new List<string>(_stats));
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
                    var elements = JsonConvert.DeserializeObject<List<string>>(json);
                    if (elements != null)
                    {
                        _stats = new HashSet<string>(elements);
                        return true;
                    }
                    else
                    {
                        _stats = new HashSet<string>();
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
