using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPrefab
{
    public GameObject Prefab;
    public string Path;

    public NetworkPrefab(GameObject obj, string path)
    {
        Prefab = obj;
        Path = path;
    }
}
