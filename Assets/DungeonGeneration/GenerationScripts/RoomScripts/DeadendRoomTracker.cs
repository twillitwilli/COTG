using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadendRoomTracker : MonoBehaviour
{
    private void Awake()
    {
        DungeonGenerationV3.Instance.spawnedRooms.deadendRooms.Add(gameObject);
    }
}
