using ForgeSDK.Extensions.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace ForgeSDK.Stats.Inspector
{
    public class StatRepositoryWindow : OdinEditorWindow
    {
        private static StatRepositoryWindow _instance;

        [MenuItem("ForgeSDK/Stats")]
        public static void ShowWindow()
        {
            _instance = GetWindow<StatRepositoryWindow>("Stats Repository");
            _instance.LoadData();
        }

        [TableList]
        public List<StatRepositoryWindowElement> Elements = new List<StatRepositoryWindowElement>();

        [Button("Refresh")]
        public void Refresh()
        {
            LoadData();
            Repaint();
        }

        [Button("Reload")]
        public void Reload()
        {
            StatRepository.PurgeInstance();
            Refresh();
        }

        public void LoadData()
        {
            StatRepository.Instance.Load();
            Elements = new List<StatRepositoryWindowElement>();
            StatRepository.Instance.GetAllElements().ForEach(item =>
            {
                var element = new StatRepositoryWindowElement(item);
                element.Updated += new EventHandler<string>((sender, key) => this.Refresh());
                Elements.Add(element);
            });
        }
    }
}
