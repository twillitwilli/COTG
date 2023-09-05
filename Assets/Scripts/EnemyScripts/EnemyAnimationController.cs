using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] 
    private EnemyController enemyController;

    public enum AnimationState 
    { 
        spawn, 
        idle, 
        walking, 
        running, 
        meleeAttack, 
        rangedAttack, 
        death, 
        turnLeft, 
        turnRight, 
        takingDamage, 
        frozen 
    }
    
    public AnimationState changeAnimation;

    public Animator enemyAnimator;
    public string spawnClip, idleClip, walkingClip, runningClip, turnLeft, turnRight, deathClip, tookDamage, frozenClip;
    public string[] meleeAttackClip, rangedAttackClip;

    [HideInInspector]
    public int currentAttack;

    public void ChangeAnimation(AnimationState animationState)
    {
        switch (animationState)
        {
            case AnimationState.spawn:
                if (spawnClip != null)
                    AnimationClip(spawnClip);
                break;

            case AnimationState.idle:
                AnimationClip(idleClip);
                enemyController.idleAnim = true;
                break;

            case AnimationState.walking:
                AnimationClip(walkingClip);
                break;

            case AnimationState.running:
                AnimationClip(runningClip);
                break;

            case AnimationState.meleeAttack:
                AnimationClip(meleeAttackClip[RandomAttack(true)]);
                break;

            case AnimationState.rangedAttack:
                AnimationClip(rangedAttackClip[RandomAttack(false)]);
                break;

            case AnimationState.death:
                AnimationClip(deathClip);
                break;

            case AnimationState.turnLeft:
                AnimationClip(turnLeft);
                break;

            case AnimationState.turnRight:
                AnimationClip(turnRight);
                break;

            case AnimationState.takingDamage:
                AnimationClip(tookDamage);
                break;

            case AnimationState.frozen:
                AnimationClip(frozenClip);
                break;
        }
    }

    private int RandomAttack(bool melee)
    {
        if (melee)
            currentAttack = Random.Range(0, meleeAttackClip.Length);

        else
            currentAttack = Random.Range(0, rangedAttackClip.Length);

        return currentAttack;
    }

    public void AnimationClip(string animationClipName)
    {
        if (MultiplayerManager.Instance.coop)
            MultiplayerManager.Instance.GetCoopManager().coopEnemyController.ChangeAnimation(enemyController.spawnID, animationClipName);

        enemyAnimator.Play(animationClipName);
    }
}
