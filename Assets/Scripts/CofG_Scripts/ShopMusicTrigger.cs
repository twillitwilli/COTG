using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMusicTrigger : MonoBehaviour
{
    public bool enteredTrigger;
    public GameObject oppositeTrigger;

    void OnTriggerEnter(Collider other)
    {
        VRPlayer player;

        if (other.gameObject.TryGetComponent<VRPlayer>(out player))
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

    void TriggerSettings()
    {
        oppositeTrigger.SetActive(true);
        gameObject.SetActive(false);
    }
}
