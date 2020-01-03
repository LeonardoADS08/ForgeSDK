/*
Developer       : Leonardo Arteaga dos Santos
First release   : 03/01/2020
File            : AssetManagement/Repository/JsonAddressableRepository.cs
Revision        : 1.0.0
Changelog       :   

*/

using ForgeSDK.Structures;
using ForgeSDK.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ForgeSDK.AssetManagement.Repository
{
    /// <summary>
    /// An implementation of <c>AddressableRepository</c> it uses json files as a way to save the data persitently
    /// </summary>
    public class JsonAddressableRepository : AddressableRepository
    {
        protected override string _fileName => "addressableRepository.json";

        public override bool Save()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(_fileLocation, false))
                {
                    var values = _repository.Values.Select(x => new Pair<string>(x.Key, x.Reference.AssetGUID)).ToList();
                    string json = JsonConvert.SerializeObject(values);
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
                using (StreamReader reader = new StreamReader(_fileLocation))
                {
                    string json = reader.ReadToEnd();
                    List<Pair<string>> rawData = JsonConvert.DeserializeObject<List<Pair<string>>>(json);
                    
                    _repository.Clear();
                    rawData.ForEach(data => _repository.Add(data.x, new AddressableRepositoryItem(data.x, data.y)));
                }

                if (_repository == null)
                {
                    _repository = new Dictionary<string, AddressableRepositoryItem>();
                    return false;
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
    }
}
