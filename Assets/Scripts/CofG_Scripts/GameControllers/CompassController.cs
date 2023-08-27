using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassController : MonoSingleton<CompassController>
{
    public bool hasCompassReveal;

    public List<GameObject> compassIndicators = new List<GameObject>();

    [HideInInspector]
    public GameObject bossRoom, itemRoom, ritualRoom, sacrificeRoom, shopRoom;

    public void CompassReveal()
    {
        foreach (GameObject obj in compassIndicators) { obj.transform.localPosition = new Vector3(0, 3, 0); }
    }

    public void CompassRevealSpecificRoom(GameObject room)
    {
        room.transform.localPosition = new Vector3(0, 3, 0);
    }
}
