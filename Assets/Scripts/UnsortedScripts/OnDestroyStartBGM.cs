using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyStartBGM : MonoBehaviour
{
    private void OnDestroy()
    {
        AudioController.Instance.ChangeMusic(AudioController.MusicTracks.Forest);
    }
}
