using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAreaLoaded : MonoBehaviour
{
    private LocalGameManager _gameManager;
    private PlayerStats _playerStats;
    private AudioController _audioController;
    private PlayerComponents _playerComponents;

    public List<GameObject> turnOnIfReturningPlayer;

    private void Start()
    {
        _gameManager = LocalGameManager.Instance;
        _playerStats = _gameManager.GetPlayerStats();

        Invoke("AreaLoaded", 1);
    }

    public void AreaLoaded()
    {
        _gameManager.AreaLoaded();
        _gameManager.MovePlayer(1);

        _gameManager.GetGearController().GetSpellCasting().canUseSpellCasting = false;

        _playerStats.AdjustMaxHealth(1000);
        _playerStats.AdjustHealth(99999999, " ");
        _playerStats.AdjustArcaneCrystalAmount(-99);

        _audioController.ChangeMusic(AudioController.MusicTracks.Caves);
    }
}
