using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAnimator : MonoBehaviour
{
    private MagicController _magicController;
    private PlayerStats _playerStats;

    public MinionPetController minionController;
    public MinionMovementController minionModel;
    public enum minionState { idle, walking, attacking, dying}
    [HideInInspector] public minionState currentAnimation;
    public string idleClip, walkingClip, attackingClip, dyingClip;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _magicController = LocalGameManager.Instance.GetMagicController();
        _playerStats = LocalGameManager.Instance.GetPlayerStats();
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
        GameObject newProjectile = Instantiate(MasterManager.playerMagicController.petAttack[_magicController.magicIdx], minionController.spellSpawnLocation);
        newProjectile.transform.SetParent(null);
        BasicProjectile projectileSettings = newProjectile.GetComponent<BasicProjectile>();

        projectileSettings.minionProjectile = true;
        projectileSettings.player = minionController.player;
        projectileSettings.attackDamage = minionController.MinionAttackDamage();
        projectileSettings.projectileRange = _playerStats.GetAttackRange();
        projectileSettings.aimAssist = _playerStats.GetAimAssist();
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
