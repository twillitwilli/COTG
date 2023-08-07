using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DungeonManagers/EnvironmentObjects")]
public class EnvironmentManager : ScriptableObject
{
    public enum DungeonType { Forest, Caves, Dungeon }
    public DungeonType dungeonType;

    [Header("Jars")]
    public GameObject[] jars;

    [Header("Forest")]
    public GameObject[] rocks;
}
