using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpikeProjectile : BasicEnemyProjectile
{
    public override void FixedUpdate()
    {
        if (!rayHit && !hitObject)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, rb.velocity, out hit, (rb.velocity.magnitude * Time.deltaTime), -ignoreLayers))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    rayHit = true;
                    HitPlayer();
                }
                else if (hit.collider)
                {
                    rb.velocity = new Vector3(0, 0, 0);
                    rayHit = true;
                }
            }
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (!hitObject && (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Head")))
        {
            if (other.gameObject.GetComponent<VRPlayerController>()) player = other.gameObject.GetComponent<VRPlayerController>();
            else player = GetComponentInParent<VRPlayerController>();
            HitPlayer();
        }
        else if (!hitObject && other.gameObject.CompareTag("Rock") || other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Ground"))
        {
            if (collisionEffect != null) { CollisionEffect(); }
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
