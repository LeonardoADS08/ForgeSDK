using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ForgeSDK.Localization.UI
{
    public class LocalizedUI : MonoBehaviour
    {
        public LocalizedString LocalizableText;

#if UNITY_EDITOR
        [ValidateInput("CheckType", "UIElement must be a valid UI type (Text, TextMeshPro)", InfoMessageType.Error)]
#endif
        [ReadOnly]
        public GameObject UIElement;

        private void OnValidate()
        {
            UIElement = gameObject;
            UpdateText();
        }

        private void Awake()
        {
            LocalizationSystem.Instance.LanguageChanged += LanguageChanged;
            UpdateText();
        }

        private bool CheckType(GameObject element)
        {
            return element != null && (element.GetComponent<Text>() != null || element.GetComponent<TextMeshProUGUI>() != null);
        }

        private void UpdateText()
        {
            if (UIElement == null) return;

            if (UIElement.GetComponent<Text>() != null)
            {
                Text text = UIElement.GetComponent<Text>();
                text.text = LocalizableText.Text;
            }
            else if (UIElement.GetComponent<TextMeshProUGUI>() != null)
            {
                TextMeshProUGUI text = UIElement.GetComponent<TextMeshProUGUI>();
                text.text = LocalizableText.Text;
            }
        }

        private void LanguageChanged(object sender, string language) => UpdateText();
    }
}
