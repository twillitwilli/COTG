using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCastingTutorial : MonoBehaviour
{
    public GameObject spellProjectile;
    public Transform spellSpawnPoint;

    public void ShootProjectile()
    {
        GameObject newProjectile = Instantiate(spellProjectile, spellSpawnPoint);
    }
}
