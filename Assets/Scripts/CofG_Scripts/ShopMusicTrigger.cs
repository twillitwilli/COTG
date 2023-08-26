using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMusicTrigger : MonoBehaviour
{
    private AudioController _audioController;

    public bool enteredTrigger;
    public GameObject oppositeTrigger;

    private void Start()
    {
        _audioController = LocalGameManager.Instance.GetAudioController();
    }

    private void OnTriggerEnter(Collider other)
    {
        VRPlayerController player;
        if (other.gameObject.TryGetComponent<VRPlayerController>(out player))
        {
            if (enteredTrigger) 
            {
                _audioController.ChangeMusic(AudioController.MusicTracks.ShopRoom);
                TriggerSettings();
            }
            else if (!enteredTrigger) 
            {
                _audioController.ChangeMusic(AudioController.MusicTracks.Forest);
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
