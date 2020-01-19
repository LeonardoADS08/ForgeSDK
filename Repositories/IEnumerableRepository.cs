using Assets.ForgeSDK.Tools;
using ForgeSDK.Extensions.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgeSDK.Repositories
{
    public abstract class IEnumerableRepository<T> : Repository<T> where T : ICopyable<T>
    {

        public override bool Exists(Func<T, bool> predicate)
        {
            if (predicate == null) return false;

            foreach (var stat in _items)
            {
                if (predicate.Invoke(stat)) return true;
            }
            return false;
        }

        public bool Exists(T element) => _items.Contains(element);

        public override int Remove(Func<T, bool> condition)
        {
            if (condition == null) return 0;
            List<T> statsToDelete = new List<T>();
            foreach (var stat in _items)
            {
                if (condition.Invoke(stat))
                {
                    statsToDelete.Add(stat);
                }
            }

            statsToDelete.ForEach(attribute => Remove(attribute));
            return statsToDelete.Count;
        }

        protected abstract bool AreEquals(T a, T b);

        public override int Update(Func<T, bool> predicate, Action<T> action)
        {
            if (predicate == null || action == null) return 0;
            int count = 0;
            _items.Where(item => predicate(item))
                  .ForEach(original =>
                  {
                      var modified = original;
                      action(modified);
                  
                      if (this.AreEquals(modified, original))
                      {
                          this.Remove(original);
                          this.Add(modified);
                          count++;
                      }
                      else if (Exists(modified))
                      {
                          this.Remove(original);
                          this.Add(modified);
                          count++;
                      }
                  });
            return count;
        }
    }
}
