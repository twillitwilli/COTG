using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPetController : MonoBehaviour
{
    public PlayerPet pet;
    public PetPickup petPickup;
    public float distanceBeforePetWillFollow, petSpeed, petStoppingDistance;

    private bool movePet;

    private void Start()
    {
        pet.gameObject.transform.SetParent(null);
    }

    private void LateUpdate()
    {
        if (!movePet && Vector3.Distance(transform.position, pet.transform.position) > distanceBeforePetWillFollow) 
        {
            pet.canMove = true;
            movePet = true; 
        }
        else if (movePet)
        {
            if (Vector3.Distance(transform.position, pet.transform.position) > 20) 
            { 
                pet.transform.position = transform.position;
                pet.canMove = false;
                movePet = false;
            }
            if (Vector3.Distance(transform.position, pet.transform.position) < petStoppingDistance) 
            {
                pet.canMove = false;
                movePet = false; 
            }
        }
    }

    public void ResetPet()
    {
        petPickup.isHoldingItem = false;
        petPickup.ClearItemChecks();
        for (int i = 0; i < petPickup.spawnLocation.childCount; i++)
        {
            if (petPickup.spawnLocation.transform.GetChild(i).gameObject)
            {
                Destroy(petPickup.spawnLocation.transform.GetChild(i).gameObject);
            }
        }
    }

    private void OnDisable()
    {
        if (pet != null) { Destroy(pet.gameObject); }
    }
}
