using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateTrigger : MonoBehaviour
{
    [SerializeField] private PressurePlateController plateController;
    public enum PressurePlateState { idle, goingDown, goingUp, down }
    public bool staysDownWhenActive;

    [HideInInspector]
    public int pressurePlateState; //0 = idle, 1 = down, 2 = going down, 3 = going up

    private Animator animator;
    private int somethingOnPlate;
    private bool platePressedDown;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Grabbable") || other.gameObject.CompareTag("Big Grabbable") || other.gameObject.CompareTag("Heavy Grabbable")))
        {
            somethingOnPlate++;
            if (!platePressedDown)
            {
                ChangeAnimationState(PressurePlateState.goingDown);
                platePressedDown = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Grabbable") || other.gameObject.CompareTag("Big Grabbable") || other.gameObject.CompareTag("Heavy Grabbable")))
        {
            somethingOnPlate--;
            if (somethingOnPlate <= 0)
            {
                somethingOnPlate = 0;
                if (!staysDownWhenActive) 
                { 
                    ChangeAnimationState(PressurePlateState.goingUp);
                    platePressedDown = false;
                }
            }
        }
    }

    public void PlateDown()
    {
        plateController.PlateDown();
        ChangeAnimationState(PressurePlateState.down);
    }

    public void PlateUp()
    {
        plateController.PlateUp();
        ChangeAnimationState(PressurePlateState.idle);
    }

    private void ChangeAnimationState(PressurePlateState plateState)
    {
        switch (plateState)
        {
            case PressurePlateState.idle:
                animator.Play("Idle");
                pressurePlateState = 0;
                break;
            case PressurePlateState.goingDown:
                animator.Play("PlateDown");
                pressurePlateState = 2;
                break;
            case PressurePlateState.goingUp:
                animator.Play("PlateUp");
                pressurePlateState = 3;
                break;
            case PressurePlateState.down:
                animator.Play("PlateStayDown");
                pressurePlateState = 1;
                break;
        }
    }
}
