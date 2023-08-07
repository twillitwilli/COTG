using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRooms : MonoBehaviour
{
    public List<GameObject> dungeonRooms;
    [HideInInspector]
    public int roomCount;

    private void Awake()
    {
        roomCount = dungeonRooms.Count;
    }
}
