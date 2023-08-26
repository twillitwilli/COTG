using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonType : MonoBehaviour
{
    public List<GameObject> dungeonTypeObjects;

    private void Start()
    {
        for (int i = 0; i < dungeonTypeObjects.Count; i++)
        {
            if (LocalGameManager.Instance.dungeonType != i) { Destroy(dungeonTypeObjects[i]); }
        }
    }
}
