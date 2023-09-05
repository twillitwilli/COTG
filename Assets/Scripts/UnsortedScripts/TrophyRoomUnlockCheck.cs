using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyRoomUnlockCheck : MonoBehaviour
{
    private void Start()
    {
        if (PlayerTotalStats.Instance.enemiesKilled > 1)
            gameObject.SetActive(false);
    }
}
