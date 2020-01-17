using ForgeSDK.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ForgeSDK.Attributes
{
    public class JsonAttributeRepository : AttributeRepository
    {
        protected override string _fileName => "attributes.json";

        public override bool Save()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(_fileLocation, false))
                {
                    string json = JsonConvert.SerializeObject(_attributes.ToList());
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
                    var elements = JsonConvert.DeserializeObject<List<AttributeInfo>>(json);
                    if (elements != null)
                    {
                        _attributes = new HashSet<AttributeInfo>(elements);
                        return true;
                    }
                    else
                    {
                        _attributes = new HashSet<AttributeInfo>();
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
