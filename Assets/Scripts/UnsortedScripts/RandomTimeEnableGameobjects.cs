using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTimeEnableGameobjects : MonoBehaviour
{
    public float lowRange, highRange;
    public List<GameObject> objectsToEnable;

    public void Start()
    {
        float randomTime = Random.Range(lowRange, highRange);
        Invoke("EnableObjects", randomTime);
    }

    public void EnableObjects()
    {
        foreach (GameObject obj in objectsToEnable) { obj.SetActive(true); }
    }
}
