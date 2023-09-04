using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyGroundSFX : MonoBehaviour
{
    [SerializeField] 
    private AudioSource sfxPlayer;
    
    [SerializeField] 
    private AudioClip clip;
    
    private int enteredTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            enteredTrigger++;

            if (enteredTrigger == 1 && !sfxPlayer.isPlaying)
                PlayAudio();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            enteredTrigger--;

            if (enteredTrigger < 0)
                enteredTrigger = 0;
        }
    }

    private void PlayAudio()
    {
        sfxPlayer.clip = clip;
        sfxPlayer.volume = AudioController.Instance.GetCreatureSFXVolume();

        if (!sfxPlayer.isPlaying)
            sfxPlayer.Play();
    }
}
