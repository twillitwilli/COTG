using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableRock : MonoBehaviour
{
    public bool breakRock;
    public bool spawnOnDestroy;
    public GameObject[] spawnObjects;
    public GameObject magicEffect;

    private int rockSpellInt;

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("RockSpell"))
        {
            rockSpellInt++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RockSpell"))
        {
            rockSpellInt++;
        }
    }

    private void LateUpdate()
    {
        if (rockSpellInt > 0)
        {
            Invoke("ResetInt", 4);
        }

        if (rockSpellInt == 8)
        {
            breakRock = true;
            rockSpellInt = 0;
        }

        if (breakRock)
        {
            magicEffect.SetActive(true);
            magicEffect.transform.SetParent(null);

            Invoke("SpawnObjects", 3f);
            Invoke("Break", 3f);
            breakRock = false;
        }
    }

    private void SpawnObjects()
    {
        foreach (GameObject spawnedObjects in spawnObjects)
        {
            Instantiate(spawnedObjects, transform.position, transform.rotation);
        }
    }

    private void Break()
    {
        Destroy(this.gameObject);
    }

    private void ResetInt()
    {
        rockSpellInt = 0;
    }
}
