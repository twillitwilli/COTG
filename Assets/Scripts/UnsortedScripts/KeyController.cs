using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public ChestController chestController;
    public GameObject lockedDoor;
    public GameObject effectWhenUnlocked;

    public void UnlockKeyLock()
    {
        if (effectWhenUnlocked != null) 
        {
            effectWhenUnlocked.SetActive(true);
            effectWhenUnlocked.transform.SetParent(null);
        }
        if (chestController != null) chestController.UnlockChest();
        if (lockedDoor != null) Destroy(lockedDoor);
    }
}
