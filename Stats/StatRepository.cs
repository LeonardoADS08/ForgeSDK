using ForgeSDK.Extensions.Linq;
using ForgeSDK.Repositories;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Stats
{
    public abstract class StatRepository : Repository<string>
    {
        #region Singleton
        private static StatRepository _instance;
        public static StatRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new JsonStatRepository();
                }
                return _instance;
            }
        }

        public static void PurgeInstance() => _instance = null;
        #endregion

        protected HashSet<string> _stats = new HashSet<string>();
        protected override IEnumerable<string> _items => _stats;

        public StatRepository()
        {
            Load();
            var notExists = Stats.STATS.Except(_stats);
            notExists.ForEach(stat => Add(stat));
            Save();
        }

        public override bool Add(string Element)
        {
            if (!Exists(Element))
            {
                _stats.Add(Element);
                return true;
            }
            else return false;
        }

        public override bool Exists(Func<string, bool> predicate)
        {
            if (predicate == null) return false;

            foreach (var stat in _stats)
            {
                if (predicate.Invoke(stat)) return true;
            }
            return false;
        }

        public bool Exists(string element) => _stats.Contains(element);

        public override bool Remove(string element)
        {
            if (Exists(element))
            {
                _stats.Remove(element);
                return true;
            }
            else return false;
        }

        public override int Remove(Func<string, bool> condition)
        {
            if (condition == null) return 0;
            List<string> statsToDelete = new List<string>();
            foreach (var stat in _stats)
            {
                if (condition.Invoke(stat))
                {
                    statsToDelete.Add(stat);
                }
            }

            statsToDelete.ForEach(attribute => Remove(attribute));
            return statsToDelete.Count;
        }

        public override int Update(Func<string, bool> predicate, Action<string> action)
        {
            if (predicate == null || action == null) return 0;
            int count = 0;
            _stats
                .Where(item => predicate(item))
                .ForEach(original =>
                {
                    var modified = original;
                    action(modified);

                    if (original == modified)
                    {
                        _stats.Remove(original);
                        _stats.Add(modified);
                        count++;
                    }
                    else if (Exists(modified))
                    {
                        Remove(original);
                        _stats.Add(modified);
                        count++;
                    }
                });
            return count;
        }
    }
}
