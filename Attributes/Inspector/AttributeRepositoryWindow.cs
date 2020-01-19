using ForgeSDK.Extensions.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ForgeSDK.Attributes.Inspector
{
    public class AttributeRepositoryWindow : OdinEditorWindow
    {
        private static AttributeRepositoryWindow _instance;

        [MenuItem("ForgeSDK/Attributes")]
        public static void ShowWindow()
        {
            _instance = GetWindow<AttributeRepositoryWindow>("Attribute Repository");
            _instance.maxSize = new Vector2(1200, 600);
            _instance.minSize = _instance.maxSize;
            _instance.LoadData();
        }

        [TableList]
        public List<AttributeRepositoryWindowElement> Elements = new List<AttributeRepositoryWindowElement>();

        [Button("Refresh")]
        public void Refresh()
        {
            LoadData();
            Repaint();
        }

        [Button("Reload")]
        public void Reload()
        {
            AttributeRepository.PurgeInstance();
            Refresh();
        }

        public void LoadData()
        {
            AttributeRepository.Instance.Load();
            Elements = new List<AttributeRepositoryWindowElement>();
            AttributeRepository.Instance.GetAllElements().ForEach(item =>
            {
                var element = new AttributeRepositoryWindowElement(item);
                element.Updated += new EventHandler<AttributeInfo>((sender, key) => this.Refresh());
                Elements.Add(element);
            });
        }
    }
}
