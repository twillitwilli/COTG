using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForKey : MonoBehaviour
{
    private PlayerStats _playerStats;
    private PlayerTotalStats _totalStats;

    [SerializeField] private KeyController keyController;
    public bool isForChest, justUseHand;
    private int handInt;

    private void Start()
    {
        _playerStats = LocalGameManager.instance.GetPlayerStats();
        _totalStats = LocalGameManager.instance.GetTotalStats();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Left Hand") || other.gameObject.CompareTag("Right Hand"))
        {
            handInt++;

            if (!justUseHand)
            {
                if (_playerStats.GetCurrentKeys() > 0)
                {
                    if (isForChest)
                    {
                        _totalStats.chestsOpened++;
                        _totalStats.AdjustStat(PlayerTotalStats.StatType.chestsOpened, 1);
                    }

                    keyController.UnlockKeyLock();
                    _playerStats.AdjustKeyAmount(-1);
                }
            }
            else { keyController.UnlockKeyLock(); }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Left Hand") || other.gameObject.CompareTag("Right Hand"))
        {
            handInt--;
            if (handInt < 0) { handInt = 0; }
        }
    }
}
