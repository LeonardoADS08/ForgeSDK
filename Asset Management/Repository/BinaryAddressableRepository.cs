/*
Developer       : Leonardo Arteaga dos Santos
First release   : 03/01/2020
File            : AssetManagement/Repository/BinaryAddressableRepository.cs
Revision        : 1.0.0
Changelog       :   

*/


using ForgeSDK.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ForgeSDK.AssetManagement.Repository
{
    /// <summary>
    /// An implementation of <c>AddressableRepository</c> it uses Binary files as a way to save the data persitently
    /// </summary>
    public class BinaryAddressableRepository : AddressableRepository
    {
        protected override string _fileName => "addressableRepository.forge";

        public override bool Save()
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(_fileLocation, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, _repository);
                stream.Close();
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
                if (!Directory.Exists(_fileLocation)) return false;

                IFormatter formatter = new BinaryFormatter();
                Stream readStream = new FileStream(_fileLocation, FileMode.Open, FileAccess.Read, FileShare.Read);
                _repository = (Dictionary<string, AddressableRepositoryItem>)formatter.Deserialize(readStream);
                readStream.Close();

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
