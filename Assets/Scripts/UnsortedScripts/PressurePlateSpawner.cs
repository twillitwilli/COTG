using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateSpawner : PressurePlateController
{
    public GameObject spawnObject;
    public bool destroyObjectsWhenOffPlate;
    public List<Transform> spawnPoints;
    public List<GameObject> spawnedObjects;

    public override void PlateDown()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (spawnedObjects[i] == null)
            {
                GameObject newObject = Instantiate(spawnObject, spawnPoints[i]);
                newObject.transform.transform.localPosition = new Vector3(0, 0, 0);
                newObject.transform.transform.localEulerAngles = new Vector3(0, 0, 0);
                spawnedObjects[i] = newObject;
            }
        }
    }

    public override void PlateUp()
    {
        if (destroyObjectsWhenOffPlate)
        {
            foreach (GameObject obj in spawnedObjects)
            {
                if (obj != null && obj.GetComponent<DropOnDestroy>()) { obj.GetComponent<DropOnDestroy>().disableDrop = true; }
                Destroy(obj);
            }
        }
    }
}
