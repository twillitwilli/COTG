using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGearManager : MonoBehaviour
{
    [System.Flags] public enum DungeonGear { map = 1, compass = 2, healthReveal = 4 }
    [SerializeField] private DungeonGear _dungeonGear;

    [SerializeField] private GameObject[] _mapObj;
    [SerializeField] private GameObject _walletObj;
}
