using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtPlayer : MonoBehaviour
{
    [SerializeField] private Transform target;
    private VRPlayerController _player;
    [HideInInspector] public EnemyController enemyController;
    [HideInInspector] public Vector3 playerTarget;

    private void Start()
    {
        _player = LocalGameManager.instance.player;
        enemyController = GetComponentInParent<EnemyController>();
    }

    private void Update()
    {
        if (CoopManager.instance == null) { CurrentPlayerIsTarget(); }
        else
        {
            if (enemyController.agroCurrentPlayer) { CurrentPlayerIsTarget(); }
            else { OtherPlayerIsTarget(); }
        }
    }

    public void CurrentPlayerIsTarget()
    {
        playerTarget = _player.playerCollider.center;
        target.position = new Vector3(playerTarget.x, playerTarget.y, playerTarget.z);
        AimAtTarget();
    }

    public void OtherPlayerIsTarget()
    {
        target.position = CoopManager.instance.otherPlayerTargetPos;
        AimAtTarget();
    }

    private void AimAtTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 12f);
    }

    private void SameYLevel()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }
}
