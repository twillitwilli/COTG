using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerParticles : MonoBehaviour
{
    public ParticleSystem particle;

    public void PlayParticles()
    {
        particle.Play();
    }

    public void StopParticles()
    {
        particle.Stop();
    }
}
