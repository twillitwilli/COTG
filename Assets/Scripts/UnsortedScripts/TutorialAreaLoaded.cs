using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TutorialAreaLoaded : MonoBehaviour
{
    public List<GameObject> turnOnIfReturningPlayer;

    private async void Start()
    {
        await Task.Delay(1000);

        AreaLoaded();
    }

    public void AreaLoaded()
    {
        LocalGameManager.Instance.AreaLoaded();
        LocalGameManager.Instance.MovePlayer(LocalGameManager.SpawnLocation.spawnPoint);

        PlayerStats.Instance.AdjustSpecificStat(PlayerStats.StatAdjustmentType.maxHealth, 1000);
        PlayerStats.Instance.AdjustSpecificStat(PlayerStats.StatAdjustmentType.arcaneCrystals, -99);

        AudioController.Instance.ChangeMusic(AudioController.MusicTracks.Caves);
    }
}
