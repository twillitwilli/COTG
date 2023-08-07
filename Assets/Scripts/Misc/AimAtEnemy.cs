using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtEnemy : MonoBehaviour
{
    [SerializeField] private Transform target;
    [HideInInspector] public GameObject currentEnemy;

    private void LateUpdate()
    {
        if (currentEnemy != null)
        {
            target.position = currentEnemy.GetComponent<EnemyController>().enemyModelCenter.transform.position;
            AimAtTarget();
        }
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
