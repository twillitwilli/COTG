using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForKey : MonoBehaviour
{
    [SerializeField] 
    private KeyController keyController;

    public bool isForChest, justUseHand;
    private int handInt;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Left Hand") || other.gameObject.CompareTag("Right Hand"))
        {
            handInt++;

            if (!justUseHand)
            {
                if (PlayerStats.Instance.data.currentKeys > 0)
                {
                    if (isForChest)
                    {
                        switch (LocalGameManager.Instance.currentGameMode)
                        {
                            case LocalGameManager.GameMode.master | LocalGameManager.GameMode.normal:
                                PlayerTotalStats.Instance.AdjustStats(PlayerTotalStats.StatType.chestsOpened);
                                break;
                        }
                    }

                    keyController.UnlockKeyLock();
                    PlayerStats.Instance.AdjustSpecificStat(PlayerStats.StatAdjustmentType.keys, -1);
                }
            }
            else
                keyController.UnlockKeyLock();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Left Hand") || other.gameObject.CompareTag("Right Hand"))
        {
            handInt--;

            if (handInt < 0)
                handInt = 0;
        }
    }
}
