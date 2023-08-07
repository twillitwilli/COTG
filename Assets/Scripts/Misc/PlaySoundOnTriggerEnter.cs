using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnTriggerEnter : MonoBehaviour
{
    public string tagToLookFor;
    public AudioSource playThisAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagToLookFor))
        {
            PlayAudio();
        }
    }

    public void PlayAudio()
    {
        playThisAudio.Play();
    }

    public void StopAudio()
    {
        playThisAudio.Stop();
    }
}
