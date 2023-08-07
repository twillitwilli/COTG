using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [HideInInspector] public EnemyTracker enemyTracker;
    [HideInInspector] public DropOnDestroy dropOnDestroy;

    public void Awake()
    {
        enemyTracker = GetComponent<EnemyTracker>();
        dropOnDestroy = GetComponent<DropOnDestroy>();
    }

    public void BossDead()
    {
        Destroy(gameObject);
    }
}
