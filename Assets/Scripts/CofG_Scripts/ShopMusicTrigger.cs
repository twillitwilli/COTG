using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMusicTrigger : MonoBehaviour
{
    public bool enteredTrigger;
    public GameObject oppositeTrigger;

    private void OnTriggerEnter(Collider other)
    {
        VRPlayerController player;

        if (other.gameObject.TryGetComponent<VRPlayerController>(out player))
        {
            if (enteredTrigger) 
            {
                AudioController.Instance.ChangeMusic(AudioController.MusicTracks.ShopRoom);
                TriggerSettings();
            }

            else if (!enteredTrigger) 
            {
                AudioController.Instance.ChangeMusic(AudioController.MusicTracks.Forest);
                TriggerSettings();
            }
        }
    }

    private void TriggerSettings()
    {
        oppositeTrigger.SetActive(true);
        gameObject.SetActive(false);
    }
}
