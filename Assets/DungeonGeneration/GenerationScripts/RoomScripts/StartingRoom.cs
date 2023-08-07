using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingRoom : MonoBehaviour
{
    private void Awake()
    {
        DungeonGenerationV3.instance.spawnedRooms.dungeonRooms.Add(gameObject);
    }
}
