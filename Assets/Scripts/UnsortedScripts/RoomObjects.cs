using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObjects : MonoBehaviour
{
    public static RoomObjects instance;
    public List<RoomPrefabs> roomPrefabs;

    private void Awake()
    {
        if (!instance) { instance = this; }
        else { Destroy(gameObject); }
    }
}
