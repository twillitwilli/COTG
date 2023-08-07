using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomList : MonoBehaviour
{
    public List<GameObject> rooms;
    [HideInInspector] public int roomCount;

    private void Awake()
    {
        roomCount = rooms.Count;
    }
}
