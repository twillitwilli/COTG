using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        LocalGameManager.Instance.GetAudioController().ChangeMusic(AudioController.MusicTracks.Forest);
    }
}
