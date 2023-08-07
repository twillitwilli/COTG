using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPet : MonoBehaviour
{
    [SerializeField] private PlayerPetController petController;
    public PetAnimator petAnimator;
    public FacePlayer facePlayer;
    public FaceObject faceObject;

    [HideInInspector] public bool canMove, pickingUpItem, changedTarget;

    private bool toggleAnimation;

    public void LateUpdate()
    {
        if (canMove)
        {
            if (toggleAnimation) 
            { 
                petAnimator.ChangeAnimation(true);
                toggleAnimation = false;
            }
            if (!pickingUpItem) 
            {
                if (changedTarget) { ToggleFacingDirection(true); }
                MovePet(petController.gameObject.transform);
            }
            else 
            {
                if (petController.petPickup.closestObj != null)
                {
                    if (changedTarget)
                    {
                        faceObject.faceObject = petController.petPickup.closestObj.transform;
                        ToggleFacingDirection(false);
                    }
                    MovePet(petController.petPickup.closestObj.transform);
                }
            }
        }
        else if (!canMove && !toggleAnimation) 
        { 
            petAnimator.ChangeAnimation(false);
            toggleAnimation = true;
        }
    }

    public void MovePet(Transform target)
    {
        transform.position = Vector3.Lerp(transform.position, target.position, petController.petSpeed * Time.deltaTime);
    }

    private void ToggleFacingDirection(bool facingPlayer)
    {
        if (facingPlayer)
        {
            faceObject.enabled = false;
            facePlayer.enabled = true;
        }
        else
        {
            facePlayer.enabled = false;
            faceObject.enabled = true;
        }
        changedTarget = false;
    }
}
