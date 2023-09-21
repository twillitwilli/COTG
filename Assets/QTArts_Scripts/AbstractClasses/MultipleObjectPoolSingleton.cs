using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using QTArts.Classes;
using qtArtsClasses = QTArts.Classes;

namespace QTArts.AbstractClasses
{
    public abstract class MultipleObjectPoolSingleton<T> : MonoBehaviour where T : MultipleObjectPoolSingleton<T>
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
        GameObject[] _objectPrefabs;

        List<qtArtsClasses::Object> _objectPool = new List<qtArtsClasses::Object>();

        void Awake()
        {
            _instance = this as T;
        }

        public async Task<GameObject> GetObject(bool randomObject = true, int specificItem = 9999)
        {
            GameObject newItem;
            int objectID;

            if (specificItem == 9999)
            {
                if (randomObject)
                    objectID = Random.Range(0, _objectPrefabs.Length);

                else
                    objectID = ObjectRaritySelection();
            }

            else
                objectID = specificItem;

            if (_objectPool.Count < 1)
            {
                newItem = SpawnNewObject(_objectPrefabs[objectID]);
                return newItem;
            } 

            else
            {
                int objectIndex = await ObjectFoundInPool(objectID);

                if (objectIndex == 9999)
                    newItem = SpawnNewObject(_objectPrefabs[objectID]);

                else
                    newItem = _objectPool[objectIndex].gameObject;

                await Task.Yield();

                return newItem;
            }
        }

        public virtual int ObjectRaritySelection()
        {
            int newItemID = 0;

            return newItemID;
        }

        async Task<int> ObjectFoundInPool(int objectID)
        {
            for (int i = 0; i < _objectPool.Count; i++)
            {
                if (_objectPool[i].objectData.id == objectID)
                    return i;
            }

            return 9999;
        }

        GameObject SpawnNewObject(GameObject newObject)
        {
            GameObject spawnedObject = Instantiate(newObject);
            _objectPool.Add(spawnedObject.GetComponent<qtArtsClasses::Object>());
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