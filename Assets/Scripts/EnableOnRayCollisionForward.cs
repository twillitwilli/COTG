using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnRayCollisionForward : MonoBehaviour
{
    public GameObject enableObject;

    public LayerMask ignoreLayers;
    public bool useCustomTag;
    public string customTagToLookFor;

    private Rigidbody rb;
    private bool rayHit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!rayHit)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, rb.velocity, out hit, (rb.velocity.magnitude * Time.deltaTime), -ignoreLayers))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    enableObject.SetActive(true);
                    rayHit = true;
                }

                else if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Ground"))
                {
                    enableObject.SetActive(true);
                    rayHit = true;
                }

                if (useCustomTag && hit.collider.CompareTag(customTagToLookFor))
                {
                    enableObject.SetActive(true);
                    rayHit = true;
                }
            }
        }
    }
}
