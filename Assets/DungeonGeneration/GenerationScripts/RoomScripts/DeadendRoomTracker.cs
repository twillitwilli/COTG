using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadendRoomTracker : MonoBehaviour
{
    private void Awake()
    {
        DungeonGenerationV3.instance.spawnedRooms.deadendRooms.Add(gameObject);
    }
}
