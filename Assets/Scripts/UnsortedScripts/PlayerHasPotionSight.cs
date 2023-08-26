using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHasPotionSight : MonoBehaviour
{
    private void Start()
    {
        if (!LocalGameManager.Instance.player.GetPlayerComponents().dungeonGear.hasPotionSight) { gameObject.SetActive(false); }
    }
}
