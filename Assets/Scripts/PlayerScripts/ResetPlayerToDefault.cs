using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerToDefault : MonoBehaviour
{
    private LocalGameManager _gameManager;
    private VRPlayerController _player;
    private PlayerComponents _playerComponents;
    private PlayerStats _playerStats;
    private MagicController _magicController;
    private PlayerCurse _curseController;

    private void Start()
    {
        _gameManager = LocalGameManager.Instance;
        _player = _gameManager.player;
        _playerComponents = _player.GetPlayerComponents();
        _playerStats = _gameManager.GetPlayerStats();
        _magicController = _gameManager.GetMagicController();
        _gearController = _gameManager.GetGearController();
        _curseController = _gameManager.GetCurseController();
    }

    public void ResetPlayer(bool playerDied)
    {
        _playerComponents.GetEyeManager().EyesClosing();

        //reset curses
        _curseController.ToggleCurseImmunity(false);
        _curseController.RemoveCurse();

        //reset drops
        _playerStats.AdjustGoldAmount(-999999);
        _playerStats.AdjustArcaneCrystalAmount(-999999);
        _playerStats.AdjustArcaneCrystalAmount(1);
        _playerStats.AdjustKeyAmount(-999999);

        //reset has special item
        _gearController.ResetDungeonGear();

        //reset special stats
        _player.canFly = false;

        //pocket reset
        foreach (VRSockets pocket in _playerComponents.GetAllSockets()) { pocket.EmptyPockets(); }

        //hand reset
        foreach (VRPlayerHand hand in _playerComponents.GetBothHands()) { hand.EmptyHand(); }

        //move player to spawn location
        _player.gameObject.transform.position = LocalGameManager.Instance.GetSpawnLocations()[0].position;
        _player.gameObject.transform.rotation = LocalGameManager.Instance.GetSpawnLocations()[0].rotation;

        //reset music to default
        _gameManager.GetAudioController().ChangeMusic(AudioController.MusicTracks.Forest);

        //Eyes
        _playerComponents.hitEffect.CheckVision();
        _playerComponents.GetEyeManager().EyesOpening();

        //Pet Reset
        _gameManager.GetPetController().GetPet().ResetPet();

        //reset class stats
        

        //reset game manager
        LocalGameManager.Instance.GetEnemyTrackerController().enemyNavMesh.RemoveData();
        LocalGameManager.Instance.GetGameTimer().EndTimer();

        if (playerDied)
        {
            Loader.Load(Loader.Scene.LoadingScreen);
            Invoke("RespawnPlayer", 15);
        }
    }

    public void RespawnPlayer()
    {
        LocalGameManager.Instance.PlayerBackToTitleScreen();
    }    
}
