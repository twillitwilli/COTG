using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingRoom : MonoBehaviour
{
    private void Awake()
    {
        DungeonGenerationV3.Instance.spawnedRooms.dungeonRooms.Add(gameObject);
    }
}
