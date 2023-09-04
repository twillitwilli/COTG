using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        AudioController.Instance.ChangeMusic(AudioController.MusicTracks.Forest);
    }
}
