using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAnimator : MonoBehaviour
{
    public MinionPetController minionController;
    public MinionMovementController minionModel;

    public enum minionState 
    { 
        idle, 
        walking, 
        attacking, 
        dying
    }
    
    [HideInInspector] 
    public minionState currentAnimation;
    
    public string idleClip, walkingClip, attackingClip, dyingClip;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimation(minionState whichAnimation)
    {
        currentAnimation = whichAnimation;

        switch (currentAnimation)
        {
            case minionState.idle:
                ChangeAnimationClip(idleClip);
                break;

            case minionState.walking:
                ChangeAnimationClip(walkingClip);
                break;

            case minionState.attacking:
                ChangeAnimationClip(attackingClip);
                break;

            case minionState.dying:
                ChangeAnimationClip(dyingClip);
                break;
        }
    }

    public void ChangeAnimationClip(string clipName)
    {
        animator.Play(clipName);
    }

    public void ShootProjectile()
    {
        GameObject newProjectile = Instantiate(MasterManager.playerMagicController.petAttack[MagicController.Instance.magicIdx], minionController.spellSpawnLocation);
        newProjectile.transform.SetParent(null);

        BasicProjectile projectileSettings = newProjectile.GetComponent<BasicProjectile>();

        projectileSettings.minionProjectile = true;
        projectileSettings.attackDamage = minionController.MinionAttackDamage();
        projectileSettings.projectileRange = PlayerStats.Instance.GetAttackRange();
        projectileSettings.aimAssist = PlayerStats.Instance.GetAimAssist();
        projectileSettings.disableCrit = true;
    }

    public void DoneAttacking()
    {
        minionController.minionAttacking = false;
    }

    public void Dead()
    {
        Destroy(minionController.gameObject);
        Destroy(minionModel.gameObject);
    }
}
