using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EnableOnVideoEnd : MonoBehaviour
{
    [HideInInspector] public VideoPlayer video;
    public GameObject objToEnable;
    private bool objEnabled;

    private AudioController _audioController;

    private void Awake()
    {
        video = GetComponent<VideoPlayer>();
    }

    private void Start()
    {
        _audioController = LocalGameManager.instance.GetAudioController();
    }

    private void LateUpdate()
    {
        video.SetDirectAudioVolume(0, _audioController.GetMusicVolume());
        if (video.clockTime > 5f && !video.isPlaying && !objEnabled) 
        {
            objToEnable.SetActive(true);
            _audioController.ChangeMusic(AudioController.MusicTracks.Forest);
            objEnabled = true;
        }
        if (!video.isPlaying && !objToEnable.activeSelf && objToEnable) { }
    }
}
