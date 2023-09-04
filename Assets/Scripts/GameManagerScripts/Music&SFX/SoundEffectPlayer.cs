using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectPlayer : MonoBehaviour
{
    private AudioSource _sfxPlayer;

    private void Awake()
    {
        _sfxPlayer = GetComponent<AudioSource>();
    }

    public void PlaySFX(float currentSFXVolume, AudioClip sfxClip, bool attachToObject, Transform attachingObject, float lifeTime)
    {
        _sfxPlayer.volume = currentSFXVolume;
        _sfxPlayer.clip = sfxClip;
        _sfxPlayer.Play();

        if (attachToObject)
            transform.SetParent(attachingObject);

        else
            Destroy(gameObject, lifeTime);
    }

    private void OnDestroy()
    {
        AudioController.Instance.RemoveSFXPlayerFromCache(gameObject);
    }
}
