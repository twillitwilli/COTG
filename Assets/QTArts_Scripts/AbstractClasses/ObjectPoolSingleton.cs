using System.Collections.Generic;
using UnityEngine;

namespace QTArts.AbstractClasses
{
    public abstract class ObjectPoolSingleton<T> : MonoBehaviour where T : ObjectPoolSingleton<T>
    {
        static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                    Debug.Log(typeof(T).ToString() + " is NULL");

                return _instance;
            }
        }

        [SerializeField]
        GameObject _objectPrefab;

        List<GameObject> _objectPool = new List<GameObject>();

        int _objectIndex;

        void Awake()
        {
            _instance = this as T;
        }

        public GameObject GetObject()
        {
            GameObject newObject;

            if (_objectPool.Count < 1)
            {
                newObject = SpawnNewObject(_objectPrefab);

                return newObject;
            }
                
            else if (!_objectPool[0].activeSelf)
            {
                newObject = _objectPool[0];
                _objectIndex = 0;

                return newObject;
            }

            else
            {
                _objectIndex++;
                _objectIndex = _objectIndex > (_objectPool.Count - 1) ? 0 : _objectIndex;

                newObject = GetObjectFromPool(_objectIndex);

                return newObject;
            }
        }

        GameObject GetObjectFromPool(int poolIdx)
        {
            GameObject objectFromPool = null;

            bool spawnNewObject = _objectPool[poolIdx].activeSelf ? true : false;

            if (spawnNewObject)
                objectFromPool = SpawnNewObject(_objectPrefab);

            else
                objectFromPool = _objectPool[poolIdx];

            if (!objectFromPool.activeSelf)
                objectFromPool.SetActive(true);

            return objectFromPool;
        }

        GameObject SpawnNewObject(GameObject newObject)
        {
            GameObject spawnedObject = Instantiate(newObject);
            _objectPool.Add(spawnedObject);
            spawnedObject.transform.SetParent(transform);

            return spawnedObject;
        }

        public void ReturnObjectToPool(GameObject obj)
        {
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;

            if (obj.activeSelf)
                obj.SetActive(false);
        }
    }
}