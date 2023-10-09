using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScrollTrigger : MonoBehaviour
{
    [SerializeField] 
    GameObject _scrollParent;

    int _scrollPrice;

    public void SetScrollPrice(int scrollPrice)
    {
        _scrollPrice = scrollPrice;
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerItemGrabbable grabbableItem;
        if (other.gameObject.TryGetComponent<PlayerItemGrabbable>(out grabbableItem))
        {
            if (grabbableItem.grabbableItem == ItemPoolManager.GrabbableItem.wallet && PlayerStats.Instance.data.currentGold >= _scrollPrice)
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
