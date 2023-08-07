using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnOnDestroy))]
[RequireComponent(typeof(DropOnDestroy))]
public class BreakableObject : MonoBehaviour
{
    private LocalGameManager _gameManager;
    private PlayerTotalStats _playerTotalStats;

    public enum BreakableObjectType { rock, jar, magicSeal }
    public BreakableObjectType objectType;

    public bool breaksOnCollision, canBreakWithBomb, canBreakWithAttack, canBreakWithArrow;
    public float breakingForceForCollision;

    [HideInInspector] public int objectID;

    private SpawnOnDestroy _spawnOnDestroyScript;
    private DropOnDestroy _dropScript;
    private Rigidbody _rb;
    private Collider _colliderOfObject;

    private void Awake()
    {
        _spawnOnDestroyScript = GetComponent<SpawnOnDestroy>();
        _dropScript = GetComponent<DropOnDestroy>();
        if (GetComponent<Rigidbody>())
        {
            _rb = GetComponent<Rigidbody>();
            _rb.isKinematic = true;
            _rb.useGravity = false;
        }
        if (GetComponent<Collider>()) 
        {
            _colliderOfObject = GetComponent<Collider>();
            _colliderOfObject.enabled = false;
        }
    }

    private void Start()
    {
        _gameManager = LocalGameManager.instance;
        _playerTotalStats = _gameManager.GetTotalStats();

        Invoke("DelayCollision", 1);
    }

    private void DelayCollision()
    {
        _colliderOfObject.enabled = true;
        if (GetComponent<Rigidbody>())
        {
            _rb.useGravity = true;
            _rb.isKinematic = false;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (breaksOnCollision && col.relativeVelocity.magnitude >= breakingForceForCollision)
        {
            if (col.gameObject.GetComponent<EnemyController>()) { col.gameObject.GetComponent<EnemyController>().enemyHealth.AdjustHealth(Mathf.RoundToInt(Random.Range(3, 6)), false); }
            BreakObject(true);
        }
    }

    public void BreakObjectWithAttack()
    {
        if (canBreakWithAttack)
        {
            BreakObject(true);
        }
    }

    public void BreakObjectWithBomb()
    {
        if (canBreakWithBomb)
        {
            BreakObject(true);
        }
    }

    public void BreakObjectWithArrow()
    {
        if (canBreakWithArrow)
        {
            BreakObject(true);
        }
    }

    public void BreakObject(bool brokenByCurrentPlayer)
    {
        if (_gameManager.inDungeon && brokenByCurrentPlayer)
        {
            switch (objectType)
            {
                case BreakableObjectType.jar:
                    _playerTotalStats.AdjustStat(PlayerTotalStats.StatType.jarsBroken, 0);
                    break;

                case BreakableObjectType.rock:
                    _playerTotalStats.AdjustStat(PlayerTotalStats.StatType.rocksBroken, 0);
                    break;

                case BreakableObjectType.magicSeal:
                    _playerTotalStats.AdjustStat(PlayerTotalStats.StatType.magicSealsBroken, 0);
                    break;
            }
        }

        _spawnOnDestroyScript.disableSpawn = false;
        _dropScript.disableDrop = false;
        Destroy(gameObject);
    }
}
