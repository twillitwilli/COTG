using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyStartBGM : MonoBehaviour
{
    private void OnDestroy()
    {
        LocalGameManager.instance.GetAudioController().ChangeMusic(AudioController.MusicTracks.Forest);
    }
}
