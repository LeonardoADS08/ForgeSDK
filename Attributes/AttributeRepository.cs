using ForgeSDK.Extensions.Linq;
using ForgeSDK.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Attributes
{
    public abstract class AttributeRepository : Repository<AttributeInfo>
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
            var notExists = Attributes.ATTRIBUTES.Select(att => att.Name).Except(_attributes.Select(att => att.Name));
            Attributes.ATTRIBUTES.ForEach(att =>
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

        public override bool Exists(Func<AttributeInfo, bool> predicate)
        {
            if (predicate == null) return false;

            foreach (var attribute in _attributes)
            {
                if (predicate.Invoke(attribute)) return true;
            }
            return false;
        }

        public bool Exists(AttributeInfo element) => _attributes.Contains(element);

        public override bool Remove(AttributeInfo element)
        {
            if (Exists(element))
            {
                _attributes.Remove(element);
                return true;
            }
            else return false;
        }

        public override int Remove(Func<AttributeInfo, bool> condition)
        {
            if (condition == null) return 0;
            List<AttributeInfo> attributesToDelete = new List<AttributeInfo>();
            foreach (var attribute in _attributes)
            {
                if (condition.Invoke(attribute))
                {
                    attributesToDelete.Add(attribute);
                }
            }

            attributesToDelete.ForEach(attribute => Remove(attribute));
            return attributesToDelete.Count;
        }

        public override int Update(Func<AttributeInfo, bool> predicate, Action<AttributeInfo> action)
        {
            if (predicate == null || action == null) return 0;
            int count = 0;
            _attributes
                .Where(item => predicate(item))
                .ForEach(original =>
                {
                    var modified = original;
                    action(modified);

                    if (original == modified)
                    {
                        _attributes.Remove(original);
                        _attributes.Add(modified);
                        count++;
                    }
                    else if (Exists(modified))
                    {
                        Remove(original);
                        _attributes.Add(modified);
                        count++;
                    }
                });
            return count;
        }
    }
}
