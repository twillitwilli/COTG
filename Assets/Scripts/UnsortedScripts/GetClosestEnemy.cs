using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetClosestEnemy : MonoBehaviour
{
    public float attackRange;
    private List<GameObject> enemiesAround = new List<GameObject>();
    [HideInInspector] public GameObject currentEnemyTarget;
    private bool foundEnemies;
    private float currentClosestDistance = 1000;
    private GameObject currentEnemyCheck;
    private bool setCooldownTimer;
    private float cooldownTimer;

    private void Start()
    {
        setCooldownTimer = true;
    }

    private void FixedUpdate()
    {
        if (currentEnemyTarget == null && CheckForEnemiesCD())
        {
            Collider[] thingsAround = Physics.OverlapSphere(transform.position, attackRange);
            foreach (Collider findEnemies in thingsAround)
            {
                if (findEnemies.gameObject.CompareTag("Enemy Body")) { enemiesAround.Add(findEnemies.gameObject); foundEnemies = true; }
            }
            setCooldownTimer = true;
        }
        
    }

    private void LateUpdate()
    {
        if (currentEnemyTarget == null && foundEnemies)
        {
            for (int i = 0; i < enemiesAround.Count; i++)
            {
                if (enemiesAround[i].gameObject != null && Vector3.Distance(enemiesAround[i].transform.position, transform.position) < currentClosestDistance)
                {
                    currentEnemyCheck = enemiesAround[i].gameObject;
                }
            }
            enemiesAround.Clear();
            foundEnemies = false;
            currentEnemyTarget = currentEnemyCheck;
        }
    }

    private bool CheckForEnemiesCD()
    {
        if (setCooldownTimer)
        {
            cooldownTimer = 5;
            setCooldownTimer = false;
        }
        if (cooldownTimer > 0) { cooldownTimer -= Time.deltaTime; }
        else if (cooldownTimer <= 0)
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
