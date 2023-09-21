using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QTArts.Interfaces;

public class TutorialGuideAnimation : MonoBehaviour, iCooldownable
{
    public float cooldownTimer { get; set; }

    [SerializeField] 
    private Animator animator;

    [SerializeField] 
    private Transform projectileSpawn;

    [SerializeField] 
    private GameObject projectilePrefab;

    public void LateUpdate()
    {
        if (CooldownDone())
            Attack();
    }

    public bool CooldownDone(bool setTimer = false, float cooldownTime = 0)
    {
        if (setTimer)
            cooldownTimer = cooldownTime;

        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;

        else
            return true;

        return false;
    }

    public void Attack()
    {
        ChangeAnimationClip("Attack1");
    }

    public void DisableSelf()
    {
        gameObject.SetActive(false);
    }

    public void Idle()
    {
        ChangeAnimationClip("Idle");

        CooldownDone(true, 1);
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
