using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentGoldDisplay : MonoBehaviour
{
    private PlayerStats _playerStats;

    [SerializeField] private Text displayGold;
    [HideInInspector] public VRPlayerController player;
    [HideInInspector] public bool walletInHand;

    private void Start()
    {
        _playerStats = LocalGameManager.instance.GetPlayerStats();
    }

    private void LateUpdate()
    {
        if (walletInHand) { UpdateDisplay(_playerStats.GetCurrentGold()); }
    }

    public void UpdateDisplay(int goldAmount)
    {
        displayGold.text = goldAmount.ToString();
    }
}
