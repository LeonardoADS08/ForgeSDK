using ForgeSDK.AssetManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ForgeSDK.Pool
{
    public class Pool
    {
        public GameObject Prefab { get; private set; }
        public int MaxSize { get; private set; }
        public int Count => _disabledInstances.Count;
        
        private List<GameObject> _disabledInstances = new List<GameObject>();
        private List<GameObject> _activeInstances = new List<GameObject>();

        public Pool(GameObject prefab, int maxSize)
        {
            Prefab = prefab;
            MaxSize = maxSize;
        }
        
        public GameObject Spawn(Vector3 Position, Quaternion Rotation, Transform Parent = null)
        {
            if (Count != 0)
            {
                var result = _disabledInstances.First();
                _disabledInstances.RemoveAt(0);
                _activeInstances.Add(result);

                result.transform.parent = Parent;
                result.transform.position = Position;
                result.transform.rotation = Rotation;
                result.gameObject.SetActive(true);
                return result;
            }
            else
            {
                var result = MonoBehaviour.Instantiate(Prefab, Position, Rotation, Parent);
                _activeInstances.Add(result);
                return result;
            }
        }

        public GameObject Spawn(Action<GameObject> customInitialization)
        {
            if (Count != 0)
            {
                var result = _disabledInstances.First();
                _disabledInstances.RemoveAt(0);
                _activeInstances.Add(result);
                result.gameObject.SetActive(true);
                customInitialization?.Invoke(result);
                return result;
            }
            else
            {
                var result = MonoBehaviour.Instantiate(Prefab);
                _activeInstances.Add(result);
                customInitialization?.Invoke(result);
                return result;
            }
        }

        public T Spawn<T>(Action<T> customInitialization) where T : MonoBehaviour
        {
            if (Count != 0)
            {
                var result = _disabledInstances.First().GetComponent<T>();
                _disabledInstances.RemoveAt(0);
                _activeInstances.Add(result.gameObject);
                result.gameObject.SetActive(true);
                customInitialization?.Invoke(result);
                return result;
            }
            else
            {
                var result = MonoBehaviour.Instantiate(Prefab).GetComponent<T>();
                _activeInstances.Add(result.gameObject);
                customInitialization?.Invoke(result);
                return result;
            }
        }

        public void Despawn(GameObject gameObject)
        {
            if (Count < MaxSize)
            {
                gameObject.SetActive(false);
                gameObject.transform.parent = null;
                _activeInstances.Add(gameObject);
                _disabledInstances.Add(gameObject);
            }
            else gameObject.ForgeDestroy();
        }
    }
}
