using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandAdjustment : MonoBehaviour
{
    [SerializeField] private HandAdjustmentController handAdjustmentController;
    [SerializeField] private Text text;

    public enum hand { leftHand, rightHand }
    public hand handSelection;

    [HideInInspector] public VRPlayerController player;
    private bool startAdjusting, adjustingHand, doneAdjusting;
    private int originInt;

    private void OnEnable()
    {
        switch (handSelection)
        {
            case hand.leftHand:
                PrintPositioningInfo(0);
                break;
            case hand.rightHand:
                PrintPositioningInfo(1);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (handSelection)
        {
            case hand.leftHand:
                if (other.gameObject.CompareTag("Left Controller Origin")) { originInt++; }
                break;
            case hand.rightHand:
                if (other.gameObject.CompareTag("Right Controller Origin")) { originInt++; }
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (handSelection)
        {
            case hand.leftHand:
                if (other.gameObject.CompareTag("Left Controller Origin")) { originInt--; }
                break;
            case hand.rightHand:
                if (other.gameObject.CompareTag("Right Controller Origin")) { originInt--; }
                break;
        }
    }

    private void LateUpdate()
    {
        if (originInt > 0)
        {
            switch (handSelection)
            {
                case hand.leftHand:
                    HandSwitch(0);
                    break;
                case hand.rightHand:
                    HandSwitch(1);
                    break;
            }
        }
    }

    private void HandSwitch(int whichHand)
    {
        //if (!startAdjusting && player.playerComponents.hand[whichHand].grabButtonDown) { HandAdjusting(whichHand); }
        //else if (startAdjusting && !player.playerComponents.hand[whichHand].grabButtonDown) { adjustingHand = true; }
        //else if (adjustingHand && player.playerComponents.hand[whichHand].grabButtonDown) { HandAdjusting(whichHand); }

        //if (doneAdjusting && !player.playerComponents.hand[whichHand].grabButtonDown) 
        //{ 
        //    startAdjusting = false; 
        //    adjustingHand = false;
        //    PrintPositioningInfo(whichHand);
        //    doneAdjusting = false;
        //}
    }

    private void HandAdjusting(int whichHand)
    {
        //startAdjusting = true;
        //GameObject handModel = player.playerComponents.hand[whichHand].handModel;
        //if (!adjustingHand) { handModel.transform.SetParent(transform); }
        //else 
        //{ 
        //    handModel.transform.SetParent(player.playerComponents.hand[whichHand].transform);
        //    doneAdjusting = true;
        //}
    }

    private void PrintPositioningInfo(int whichHand)
    {
        //text.text = "Position" + player.playerComponents.hand[whichHand].handModel.transform.localPosition * 100 + "\n" + "Rotation" + player.playerComponents.hand[whichHand].handModel.transform.localEulerAngles;
    }
}