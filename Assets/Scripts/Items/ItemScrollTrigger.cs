using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScrollTrigger : MonoBehaviour
{
    [SerializeField] 
    private GameObject _scrollParent;

    private int _scrollPrice;

    public void SetScrollPrice(int scrollPrice)
    {
        _scrollPrice = scrollPrice;
    }

    private void OnTriggerEnter(Collider other)
    {
        WalletItem wallet;
        if (other.gameObject.TryGetComponent<WalletItem>(out wallet))
        {
            if (PlayerStats.Instance.data.currentGold >= _scrollPrice)
            {
                AbsorbScrollKnowledge();
                PlayerStats.Instance.AdjustSpecificStat(PlayerStats.StatAdjustmentType.gold, -_scrollPrice);
                PlayerTotalStats.Instance.AdjustStats(PlayerTotalStats.StatType.itemsBought);
            }
        }
    }

    public void AbsorbScrollKnowledge()
    {
        switch (LocalGameManager.Instance.currentGameMode)
        {
            case LocalGameManager.GameMode.normal | LocalGameManager.GameMode.master:
                PlayerTotalStats.Instance.AdjustStats(PlayerTotalStats.StatType.scrollsAbsorbed);
                break;
        }

        Destroy(_scrollParent);
    }
}
