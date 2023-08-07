using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGuideAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Cooldown cooldown;
    [SerializeField] private Transform projectileSpawn;
    [SerializeField] private GameObject projectilePrefab;

    public void LateUpdate()
    {
        if (cooldown.CooldownDone()) { Attack(); }
    }

    public void DisableSelf()
    {
        gameObject.SetActive(false);
    }

    public void Idle()
    {
        ChangeAnimationClip("Idle");
        cooldown.setCooldown = true;
    }

    public void Attack()
    {
        ChangeAnimationClip("Attack1");
    }

    public void ShootProjectile()
    {
        GameObject obj = Instantiate(projectilePrefab, projectileSpawn);
    }

    public void ChangeAnimationClip(string clipName)
    {
        animator.Play(clipName);
    }
}
