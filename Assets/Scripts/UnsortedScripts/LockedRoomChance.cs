using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedRoomChance : MonoBehaviour
{
    public GameObject rockLock, keyLock;

    private void Start()
    {
        float lockChance = Random.Range(0, 100);
        if (lockChance < 75) 
        {
            int randomLock = Random.Range(0, 100);
            if (randomLock < 70)
            {
                GameObject rockLockObj = Instantiate(rockLock, transform.position, transform.rotation);
                ResetPositioning(rockLockObj);
            }
            else
            {
                GameObject keyLockObj = Instantiate(keyLock, transform.position, transform.rotation);
                keyLockObj.GetComponent<KeyController>().lockedDoor = gameObject;
                ResetPositioning(keyLockObj);
            }
        }
    }

    private void ResetPositioning(GameObject obj)
    {
        obj.transform.SetParent(transform);
        obj.transform.localPosition = new Vector3(0, 0, 0);
        obj.transform.localEulerAngles = new Vector3(0, 0, 0);
        obj.transform.localScale = new Vector3(1, 1, 1);
    }
}
