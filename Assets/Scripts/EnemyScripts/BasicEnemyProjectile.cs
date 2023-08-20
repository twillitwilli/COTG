using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyProjectile : MonoBehaviour
{
    private LocalGameManager _gameManager;

    [SerializeField] private string _deathMessage;
    public float attackDamage = 30f, projectileSpeed = 15f, projectileRange = 5f;
    public LayerMask ignoreLayers;
    public GameObject collisionEffect;

    protected Rigidbody rb;
    protected bool rayHit, hitObject, triggerHit, delayDestruction;
    protected VRPlayerController player;

    public virtual void Awake()
    {
        _gameManager = LocalGameManager.instance;
        transform.SetParent(null);
        rb = GetComponent<Rigidbody>();
    }

    public virtual void Start()
    {
        rb.velocity = transform.forward * projectileSpeed;

        if (LocalGameManager.instance.currentGameMode == LocalGameManager.GameMode.master) 
        { 
            attackDamage += (attackDamage * 0.5f); 
        }

        Invoke("DestroyAttack", projectileRange);
    }

    public virtual void FixedUpdate()
    {
        if (!rayHit && !hitObject)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, rb.velocity, out hit, (rb.velocity.magnitude * Time.deltaTime), -ignoreLayers))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    rayHit = true;
                    VRPlayerController player = hit.collider.gameObject.GetComponent<VRPlayerController>();
                    HitPlayer();
                }
                else if (hit.collider)
                {
                    Destroy(gameObject);
                    rayHit = true;
                }
            }
        }
    }

    public virtual void LateUpdate()
    {
        if (hitObject) { CollisionEffect(); }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if(!rayHit && !triggerHit)
        {
            VRPlayerController player;
            if (other.gameObject.TryGetComponent<VRPlayerController>(out player))
            {
                HitPlayer();
                triggerHit = true;
            }
            else if (!hitObject && other.gameObject.CompareTag("Rock") || other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Ground"))
            {
                if (collisionEffect != null) { CollisionEffect(); }
                triggerHit = true;
                Destroy(gameObject);
            }
        }
    }

    public virtual void ReduceSpeed()
    {
        rb.velocity = (transform.forward * projectileSpeed) / 2;
        delayDestruction = true;
        Invoke("DelayDestroyAttack", projectileRange * 2);
    }

    public virtual void HitPlayer()
    {
        _gameManager.GetPlayerStats().AdjustHealth(-attackDamage, _deathMessage);
    }

    public virtual void CollisionEffect()
    {
        GameObject obj = Instantiate(collisionEffect, transform.position, transform.rotation);
        obj.transform.SetParent(null);
    }

    public virtual void DestroyAttack()
    {
        if (!delayDestruction) { Destroy(gameObject); }
    }

    public virtual void DelayDestroyAttack()
    {
        Destroy(gameObject);
    }
}
