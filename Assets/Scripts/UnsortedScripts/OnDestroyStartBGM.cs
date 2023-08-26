using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyStartBGM : MonoBehaviour
{
    private void OnDestroy()
    {
        LocalGameManager.Instance.GetAudioController().ChangeMusic(AudioController.MusicTracks.Forest);
    }
}
