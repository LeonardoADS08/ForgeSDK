/*
Developer       : Leonardo Arteaga dos Santos
First release   : 03/01/2020
File            : AssetManagement/Inspector/AddressableRepositoryWindow.cs
Revision        : 1.0.0
Changelog       :   
*/

using ForgeSDK.AssetManagement.Repository;
using ForgeSDK.Extensions.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ForgeSDK.AssetManagement
{
    /// <summary>
    /// Manage all AssetManagement window through an abstract <c>AddressableRepository</c>.
    /// </summary>
    [InitializeOnLoad]
    public class AddressableRepositoryWindow : OdinEditorWindow
    {
        /// <summary>
        /// Instance is saved in a static field if it's needed in some situation
        /// </summary>
        private static AddressableRepositoryWindow _instance;

        /// <summary>
        /// The OdinEditorWindow is in the toolbar [ForgeAPI -> AssetManagement]
        /// </summary>
        [MenuItem("ForgeSDK/Asset Management")]
        public static void ShowWindow()
        {
            _instance = GetWindow<AddressableRepositoryWindow>("Addressable Repository");
            _instance.LoadData();
        }

        /// <summary>
        /// List of <c>AddressableRepositoryWindowElement</c> where all the data is represented
        /// </summary>
        [TableList]
        public List<AddressableRepositoryWindowElement> Elements = new List<AddressableRepositoryWindowElement>();

        /// <summary>
        /// Force reload data and re-renderize the window
        /// </summary>
        [Button("Refresh")]
        public void Refresh()
        {
            LoadData();
            Repaint();
        }

        /// <summary>
        /// Purge the instance of <c>AddressableRepository</c> and then refresh the window
        /// </summary>
        [Button("Reload")]
        public void Reload()
        {
            AddressableRepository.PurgeInstance();
            Refresh();
        }

        /// <summary>
        /// It load all the data from the <c>AddressableRepository</c> and parse it into a structure valid for be shown,
        /// also it subscribe to <c>AddressableRepositoryWindowElement.Updated</c> EventHandler for check changes and update the window when is needed,
        /// </summary>
        public void LoadData()
        {
            AddressableRepository.Instance.Load();
            Elements = new List<AddressableRepositoryWindowElement>();
            AddressableRepository.Instance.GetAllElements().ForEach(item =>
            {
                var element = new AddressableRepositoryWindowElement(item);
                element.Updated += new EventHandler<string>((sender, key) => this.Refresh());
                Elements.Add(element);
            });
        }
    }
}