using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScrollTrigger : MonoBehaviour
{
    private LocalGameManager _gameManager;
    private PlayerStats _playerStats;
    private PlayerTotalStats _playerTotalStats;

    [SerializeField] private GameObject _scrollParent;

    private int _scrollPrice;

    private void Start()
    {
        _gameManager = LocalGameManager.instance;
        _playerStats = _gameManager.GetPlayerStats();
        _playerTotalStats = _gameManager.GetTotalStats();
    }

    public void SetScrollPrice(int scrollPrice)
    {
        _scrollPrice = scrollPrice;
    }

    private void OnTriggerEnter(Collider other)
    {
        WalletItem wallet;
        if (other.gameObject.TryGetComponent<WalletItem>(out wallet))
        {
            if (_playerStats.GetCurrentGold() >= _scrollPrice)
            {
                AbsorbScrollKnowledge();
                _playerStats.AdjustGoldAmount(-_scrollPrice);
                _playerTotalStats.AdjustStat(PlayerTotalStats.StatType.itemsBought, 0);
            }
        }
    }

    public void AbsorbScrollKnowledge()
    {
        if (_gameManager.inDungeon)
        {
            _playerTotalStats.AdjustStat(PlayerTotalStats.StatType.scrollsAbsorbed, 0);
        }

        Destroy(_scrollParent);
    }
}
