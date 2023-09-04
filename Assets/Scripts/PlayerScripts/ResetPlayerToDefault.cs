using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ResetPlayerToDefault : MonoBehaviour
{
    public async void ResetPlayer(bool playerDied)
    {
        PlayerComponents playerComponents = LocalGameManager.Instance.player.GetPlayerComponents();

        playerComponents.GetEyeManager().EyesClosing();

        //reset curses
        PlayerCurse.Instance.ToggleCurseImmunity(false);
        PlayerCurse.Instance.RemoveCurse();

        //reset drops
        PlayerStats.Instance.AdjustGoldAmount(-999999);
        PlayerStats.Instance.AdjustArcaneCrystalAmount(-999999);
        PlayerStats.Instance.AdjustArcaneCrystalAmount(1);
        PlayerStats.Instance.AdjustKeyAmount(-999999);

        //reset special stats
        LocalGameManager.Instance.player.canFly = false;

        //pocket reset
        foreach (VRSockets pocket in playerComponents.GetAllSockets()) { pocket.EmptyPockets(); }

        //hand reset
        foreach (VRPlayerHand hand in playerComponents.GetBothHands()) { hand.EmptyHand(); }

        //move player to spawn location
        LocalGameManager.Instance.player.gameObject.transform.position = LocalGameManager.Instance.GetSpawnLocations()[0].position;
        LocalGameManager.Instance.player.gameObject.transform.rotation = LocalGameManager.Instance.GetSpawnLocations()[0].rotation;

        //reset music to default
        AudioController.Instance.ChangeMusic(AudioController.MusicTracks.Forest);

        //Eyes
        playerComponents.hitEffect.CheckVision();
        playerComponents.GetEyeManager().EyesOpening();

        //Pet Reset
        FollowerPetController.Instance.GetPet().ResetPet();

        //reset class stats
        

        //reset game manager
        EnemyTrackerController.Instance.enemyNavMesh.RemoveData();
        GameTimer.Instance.EndTimer();

        if (playerDied)
        {
            Loader.Load(Loader.Scene.LoadingScreen);

            await Task.Delay(15000);

            RespawnPlayer();
        }
    }

    public void RespawnPlayer()
    {
        LocalGameManager.Instance.PlayerBackToTitleScreen();
    }    
}
