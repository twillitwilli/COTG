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
        _playerStats = LocalGameManager.Instance.GetPlayerStats();
        _totalStats = LocalGameManager.Instance.GetTotalStats();
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
                        switch (LocalGameManager.Instance.currentGameMode)
                        {
                            case LocalGameManager.GameMode.master | LocalGameManager.GameMode.normal:
                                _totalStats.AdjustStats(PlayerTotalStats.StatType.chestsOpened);
                                break;
                        }
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
