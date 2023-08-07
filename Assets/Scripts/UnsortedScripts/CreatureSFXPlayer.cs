using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSFXPlayer : MonoBehaviour
{
    [HideInInspector]
    public AudioSource sfxPlayer;
    public AudioClip clip;

    private bool setCooldownTimer;
    private float cooldownTimer;

    private void Awake()
    {
        sfxPlayer = GetComponent<AudioSource>();
    }

    private void LateUpdate()
    {
        if (!CheckIfAudioPlaying() && RandomPlayCooldown())
        {
            PlayAudio();
        }
    }

    public bool RandomPlayCooldown()
    {
        if (setCooldownTimer)
        {
            cooldownTimer = Random.Range(4f, 8f);
            setCooldownTimer = false;
        }
        if (cooldownTimer > 0) { cooldownTimer -= Time.deltaTime; }
        else if (cooldownTimer <= 0)
        {
            cooldownTimer = 0;
            return true;
        }
        return false;
    }

    public bool CheckIfAudioPlaying()
    {
        if (sfxPlayer.isPlaying) { return true; }
        else return false;
    }

    public void PlayAudio()
    {
        setCooldownTimer = true;
        sfxPlayer.clip = clip;
        sfxPlayer.volume = LocalGameManager.instance.GetAudioController().GetCreatureSFXVolume();
        if (!sfxPlayer.isPlaying) { sfxPlayer.Play(); }
    }
}
