using ForgeSDK.AssetManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ForgeSDK.Pool
{
    public class PoolSystem : MonoBehaviour
    {
        #region Singleton
        public static PoolSystem _instance;
        public static PoolSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject gameObject = new GameObject("Singleton_PoolSystem");
                    _instance = gameObject.AddComponent<PoolSystem>();
                }
                return _instance;
            }
        }
        #endregion

        public const int MAX_POOL_SIZE = 100;

        private Dictionary<GameObject, Pool> _pools = new Dictionary<GameObject, Pool>();
        private Dictionary<GameObject, Pool> _instanceLinks = new Dictionary<GameObject, Pool>();
        public Pool CreatePool(GameObject Prefab, int MaxSize = MAX_POOL_SIZE)
        {
            Pool newPool = new Pool(Prefab, MaxSize);
            _pools.Add(Prefab, newPool);
            return newPool;
        }

        public void RemovePool(GameObject Prefab) => _pools.Remove(Prefab);

        public GameObject Spawn(GameObject Prefab, Vector3 Position, Quaternion Rotation, Transform Parent = null)
        {
            if (!_pools.ContainsKey(Prefab))
                _pools.Add(Prefab, new Pool(Prefab, MAX_POOL_SIZE));

            var instance = _pools[Prefab].Spawn(Position, Rotation, Parent);
            _instanceLinks.Add(instance, _pools[Prefab]);
            return instance;
        }

        public GameObject Spawn(GameObject Prefab, Action<GameObject> customInitialization)
        {
            if (!_pools.ContainsKey(Prefab))
                _pools.Add(Prefab, new Pool(Prefab, MAX_POOL_SIZE));

            var instance = _pools[Prefab].Spawn(customInitialization);
            _instanceLinks.Add(instance, _pools[Prefab]);
            return instance;
        }

        public T Spawn<T>(GameObject Prefab, Action<T> customInitialization) where T : MonoBehaviour
        {
            if (!_pools.ContainsKey(Prefab))
                _pools.Add(Prefab, new Pool(Prefab, MAX_POOL_SIZE));

            var instance = _pools[Prefab].Spawn(customInitialization);
            _instanceLinks.Add(instance.gameObject, _pools[Prefab]);
            return instance;
        }

        public void Despawn(GameObject gameObject)
        {
            if (_instanceLinks.ContainsKey(gameObject))
            {
                var pool = _instanceLinks[gameObject];
                pool.Despawn(gameObject);
                _instanceLinks.Remove(gameObject);
            }
            else gameObject.ForgeDestroy();
        }


        public void Despawn(GameObject gameObject, float time)
        {
            if (_instanceLinks.ContainsKey(gameObject))
                StartCoroutine(DespawnCoroutine(gameObject, time));
            else
                gameObject.ForgeDestroy(time);
        }

        private IEnumerator DespawnCoroutine(GameObject gameObject, float time)
        {
            float finalTime = Time.time + time;
            while(Time.time < finalTime)
                yield return new WaitForSeconds(0.1f);

            var pool = _instanceLinks[gameObject];
            pool.Despawn(gameObject);
            _instanceLinks.Remove(gameObject);
        }
        
    }

}
