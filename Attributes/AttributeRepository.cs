using ForgeSDK.Extensions.Linq;
using ForgeSDK.Repositories;
using ForgeSDK.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Attributes
{
    public abstract class AttributeRepository : IEnumerableRepository<AttributeInfo>
    {
        #region Singleton
        private static AttributeRepository _instance;
        public static AttributeRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new JsonAttributeRepository();
                }
                return _instance;
            }
        }

        public static void PurgeInstance() => _instance = null;
        #endregion

        protected HashSet<AttributeInfo> _attributes = new HashSet<AttributeInfo>();
        protected override IEnumerable<AttributeInfo> _items => _attributes;

        public AttributeRepository()
        {
            Load();
            DefaultAttributes.GetAttributes().ForEach(att => Add(att));
            Save();
        }

        public override bool Add(AttributeInfo Element)
        {
            if (!Exists(Element))
            {
                _attributes.Add(Element);
                return true;
            }
            else return false;
        }

        public override bool Remove(AttributeInfo element)
        {
            if (Exists(element))
            {
                _attributes.Remove(element);
                return true;
            }
            else return false;
        }

        protected override bool AreEquals(AttributeInfo a, AttributeInfo b) => a.Name == b.Name;

    }
}
