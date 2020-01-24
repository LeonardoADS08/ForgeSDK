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

        #region Default values
        private static List<AttributeInfo> ATTRIBUTES => new List<AttributeInfo>()
        {
            BOOL_IS_INVULNERABLE,
            BOOL_IS_SHOOTING,
            INT_STAT_CHARACTER_HEALTH
        };

        public static readonly AttributeInfo BOOL_IS_INVULNERABLE = new AttributeInfo("Invulnerable", false, AttributeType.Bool);
        public static readonly AttributeInfo BOOL_IS_SHOOTING = new AttributeInfo("Is shooting", false, AttributeType.Bool);
        public static readonly AttributeInfo INT_STAT_CHARACTER_HEALTH = new AttributeInfo("Health", new IntStat(), AttributeType.IntStat);
        #endregion

        protected HashSet<AttributeInfo> _attributes = new HashSet<AttributeInfo>();
        protected override IEnumerable<AttributeInfo> _items => _attributes;

        public AttributeRepository()
        {
            Load();
            var notExists = ATTRIBUTES.Select(att => att.Name).Except(_attributes.Select(att => att.Name));
            ATTRIBUTES.ForEach(att =>
            {
                if (notExists.Contains(att.Name))
                    Add(att);
            });
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
