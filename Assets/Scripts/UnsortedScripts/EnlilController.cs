using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnlilController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject enlil, spawnEffect;
    [SerializeField] private TutorialGuideAnimation enlilAnimations;

    public void EnlilSpawned()
    {
        spawnEffect.SetActive(false);
    }

    public void DespawnEnlil()
    {
        enlilAnimations.ChangeAnimationClip("Levitate");
        ChangeAnimationClip("DespawnEnlil");
    }

    public void DisableSelf()
    {
        gameObject.SetActive(false);
    }

    public void ChangeAnimationClip(string clipName)
    {
        animator.Play(clipName);
    }
}
