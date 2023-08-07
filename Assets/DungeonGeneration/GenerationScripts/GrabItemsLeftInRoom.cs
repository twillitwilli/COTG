using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabItemsLeftInRoom : MonoBehaviour
{
    public GameObject roomParent;
    public Transform roomCenter;
    private float roomSize;

    private void Start()
    {
        roomSize = roomParent.transform.localScale.x;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) { GrabItemsInRoom(); }
    }

    public void GrabItemsInRoom()
    {
        float boxSize = (roomSize / 2);
        Collider[] roomsConnected = Physics.OverlapBox(transform.position, new Vector3(boxSize, boxSize, boxSize), transform.rotation);
        foreach (Collider col in roomsConnected) 
        { 
            if (col.gameObject.CompareTag("Grabbable")) 
            {
                col.gameObject.transform.SetParent(roomParent.transform);
            } 
        }
    }

    //Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(roomSize, roomSize, roomSize));
    }
}
