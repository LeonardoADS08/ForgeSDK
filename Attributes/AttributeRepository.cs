using ForgeSDK.Extensions.Linq;
using ForgeSDK.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.ForgeSDK.Attributes
{
    public abstract class AttributeRepository : Repository<string>
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
                    _instance.Load();
                }
                return _instance;
            }
        }
        #endregion

        protected HashSet<string> _attributes;
        protected override IEnumerable<string> _items => _attributes;

        public override bool Add(string Element)
        {
            if (Exists(Element))
            {
                _attributes.Add(Element);
                return true;
            }
            else return false;
        }

        public override bool Exists(Func<string, bool> predicate)
        {
            if (predicate == null) return false;

            foreach (var attribute in _attributes)
            {
                if (predicate.Invoke(attribute)) return true;
            }
            return false;
        }

        public bool Exists(string element) => _attributes.Contains(element);

        public override bool Remove(string element)
        {
            if (Exists(element))
            {
                _attributes.Remove(element);
                return true;
            }
            else return false;
        }

        public override int Remove(Func<string, bool> condition)
        {
            if (condition == null) return 0;
            List<string> attributesToDelete = new List<string>();
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

        public override int Update(Func<string, bool> predicate, Action<string> action)
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
