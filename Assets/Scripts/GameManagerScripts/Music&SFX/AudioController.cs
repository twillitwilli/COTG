using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoSingleton<AudioController>
{
    [SerializeField] 
    private PlayerPrefsSaveData _playerPrefSaveData;

    public enum MusicTracks 
    { 
        Forest, 
        Combat, 
        Boss, 
        ShopRoom, 
        Caves 
    }

    [SerializeField] 
    private GameObject _sfxPlayerObj;

    public float musicVolume { get; private set; }
    public float sfxVolume { get; private set; }
    public float creatureSFXVolume { get; private set; }

    private AudioSource _musicPlayer;

    [SerializeField] 
    private AudioClip[] _musicClips, _playerSFXclips;

    private List<GameObject> _sfxPlayers = new List<GameObject>();

    private void Awake()
    {
        LocalGameManager.playerCreated += NewPlayerCreated;

        DefaultAudioSettings();
    }

    public void NewPlayerCreated(VRPlayerController player)
    {
        _musicPlayer = player.GetPlayerComponents().GetMusicPlayer();
    }

    public void DefaultAudioSettings()
    {
        musicVolume = 1;
        sfxVolume = 1;
        creatureSFXVolume = 1;
    }

    public void SaveVolumeStats()
    {
        PlayerPrefs.SetFloat("BGM", GetMusicVolume());
        PlayerPrefs.SetFloat("SFX", GetSFXVolume());
        PlayerPrefs.SetFloat("CreatureSFX", GetCreatureSFXVolume());
    }

    public void LoadVolumeStats()
    {
        float loadMusicVolme = PlayerPrefsSaveData.Instance.CheckIfSaveFileExists("BGM") ? PlayerPrefs.GetFloat("BGM") : 1;
        musicVolume = loadMusicVolme;

        float loadSFXVolume = PlayerPrefsSaveData.Instance.CheckIfSaveFileExists("SFX") ? PlayerPrefs.GetFloat("SFX") : 1;
        sfxVolume = loadSFXVolume;

        float loadCreatureVolume = PlayerPrefsSaveData.Instance.CheckIfSaveFileExists("CreatureSFX") ? PlayerPrefs.GetFloat("CreatureSFX") : 1;
        creatureSFXVolume = loadCreatureVolume;
    }

    public void ChangeMusic(MusicTracks whichTrack)
    {
        int trackIdx = (int)whichTrack;

        if (trackIdx == -1)
            Debug.Log("Track Index Error");

        else
        {
            _musicPlayer = GetComponent<AudioSource>();
            _musicPlayer.clip = _musicClips[trackIdx];
            _musicPlayer.Play();
        }
    }

    public void StopMusic()
    {
        _musicPlayer.Stop();
    }

    public void AdjustMusicVolume(float valueAdjustment)
    {
        musicVolume += valueAdjustment;

        if (musicVolume > 1)
            musicVolume = 1;

        else if (musicVolume < 0)
            musicVolume = 0;

        _musicPlayer.volume = musicVolume;
    }

    public int GetMusicVolume() { return Mathf.RoundToInt(musicVolume * 100); }

    public void PlaySFXClip(Vector3 spawnLocation, AudioClip sfxClip, bool attachToObject, Transform attachingObject, float lifeTime)
    {
        GameObject newSFX = Instantiate(_sfxPlayerObj, spawnLocation, transform.rotation);
        _sfxPlayers.Add(newSFX);
        newSFX.GetComponent<SoundEffectPlayer>().PlaySFX(sfxVolume, sfxClip, attachToObject, attachingObject, lifeTime);
    }

    public void AdjustSFXVolume(float valueAdjustment)
    {
        sfxVolume += valueAdjustment;

        if (sfxVolume >= 1)
            sfxVolume = 1;

        else if (sfxVolume <= 0)
            sfxVolume = 0;
    }

    public int GetSFXVolume() { return Mathf.RoundToInt(sfxVolume * 100); }

    public void RemoveSFXPlayerFromCache(GameObject removeSFXPlayer)
    {
        _sfxPlayers.Remove(removeSFXPlayer);
    }

    public void DestroyAllSFXPlayers()
    {
        if (_sfxPlayers.Count > 0)
        {
            foreach (GameObject obj in _sfxPlayers)
            {
                Destroy(obj);
            }
        }
    }

    public void AdjustCreatureSFXVolume(float valueAdjustment)
    {
        creatureSFXVolume += valueAdjustment;

        if (creatureSFXVolume >= 1)
            creatureSFXVolume = 1;

        else if (creatureSFXVolume <= 0)
            creatureSFXVolume = 0;
    }

    public int GetCreatureSFXVolume() { return Mathf.RoundToInt(creatureSFXVolume * 100); }

    public void SetCreatureSFXVolume(AudioSource creatureSFXPlayer)
    {
        creatureSFXPlayer.volume = creatureSFXVolume;
    }
}
