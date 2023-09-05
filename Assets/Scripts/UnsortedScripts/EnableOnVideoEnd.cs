using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EnableOnVideoEnd : MonoBehaviour
{
    [HideInInspector] 
    public VideoPlayer video;
    
    public GameObject objToEnable;
    
    private bool objEnabled;

    private void Awake()
    {
        video = GetComponent<VideoPlayer>();
    }

    private void LateUpdate()
    {
        video.SetDirectAudioVolume(0, AudioController.Instance.GetMusicVolume());

        if (video.clockTime > 5f && !video.isPlaying && !objEnabled) 
        {
            objToEnable.SetActive(true);
            AudioController.Instance.ChangeMusic(AudioController.MusicTracks.Forest);
            objEnabled = true;
        }

        if (!video.isPlaying && !objToEnable.activeSelf && objToEnable)
        {

        }
    }
}
