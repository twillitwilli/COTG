using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnTrigger : MonoBehaviour
{
    public Transform moveTo;
    public bool onlyObjectsWithTag;
    public string tagToSearchFor;

    private void OnTriggerEnter(Collider other)
    {
        if (onlyObjectsWithTag && other.gameObject.CompareTag(tagToSearchFor)) { MoveObject(other.gameObject); }
        else if (!onlyObjectsWithTag) { MoveObject(other.gameObject); }
    }

    private void MoveObject(GameObject obj)
    {
        obj.transform.position = moveTo.position;
        obj.transform.eulerAngles = moveTo.eulerAngles;
    }
}
