using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> mapBlocks = new List<GameObject>();

    public void RevealMap()
    {
        foreach (GameObject obj in mapBlocks) { Destroy(obj); }
    }
}
