using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using ForgeSDK.Localization.Repositories;

namespace ForgeSDK.Localization
{
    [Serializable]
    public class LocalizedString : ISerializationCallbackReceiver 
    {
        [HorizontalGroup(GroupID = "Localizable", Order = 0)]
        public string Code;

        public string Text => LocalizationSystem.Instance.GetText(Code);
#if UNITY_EDITOR
        [ShowInInspector, ReadOnly, HideLabel, HorizontalGroup(GroupID = "Localizable", Order = 1)]
        private string _text;
#endif
        private LocalizableString _localizable;

        public void OnAfterDeserialize() { }

        public void OnBeforeSerialize() 
        {
        #if UNITY_EDITOR
            _localizable = LocalizationSystem.Instance.LocalizationRepository.GetElement(localizable => localizable.Code == Code);
            if (_localizable != null) _text = _localizable.Value;
        #endif
        }
    }
}
