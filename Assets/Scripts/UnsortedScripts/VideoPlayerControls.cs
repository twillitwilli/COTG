using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoPlayerControls : MonoBehaviour
{
    public enum VideoPlayerButtons 
    { 
        play, 
        stop, 
        pause, 
        volume
    }

    public VideoPlayerButtons button;

    [SerializeField] 
    private VideoPlayer _videoPlayer;
    
    [SerializeField] 
    private Text _textDisplay;
    
    [SerializeField] 
    private bool _checkTextOnly;
    
    [SerializeField] 
    private float _valueAdjustment;

    private float _currentVolume;

    private void Start()
    {
        _currentVolume = AudioController.Instance.GetMusicVolume();
        _videoPlayer.SetDirectAudioVolume(0, _currentVolume);
        Play();
    }

    public void VideoPlayerButtonPressed()
    {
        switch (button)
        {
            case VideoPlayerButtons.play:
                Play();
                break;

            case VideoPlayerButtons.stop:
                Stop();
                break;

            case VideoPlayerButtons.pause:
                Pause();
                break;

            case VideoPlayerButtons.volume:
                Volume();
                break;
        }
    }

    private void Play()
    {
        AudioController.Instance.StopMusic();
        _videoPlayer.Play();
    }

    private void Stop()
    {
        _videoPlayer.Stop();
        AudioController.Instance.ChangeMusic(AudioController.MusicTracks.Forest);
    }

    private void Pause()
    {
        _videoPlayer.Pause();
        AudioController.Instance.ChangeMusic(AudioController.MusicTracks.Forest);
    }

    private void Volume()
    {
        if (!_checkTextOnly) 
        {
            _currentVolume += _valueAdjustment;
            _videoPlayer.SetDirectAudioVolume(0, _currentVolume); 
        }
        AdjustTextDisplay("Volume:\n" + Mathf.RoundToInt(_currentVolume * 100) + "%");
    }

    private void AdjustTextDisplay(string displayText)
    {
        _textDisplay.text = displayText;
    }
}
