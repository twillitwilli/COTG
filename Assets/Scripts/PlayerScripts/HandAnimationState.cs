using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimationState : MonoBehaviour
{
    private Animator _animator;
    private ControllerInputManager.ButtonDown _currentButtonsDown;

    public enum HandState { idle, fist, fingerPoint, thumbsUp, holdingMap, holdingWallet, holdingArcaneBomb, holdingBowString, fingerGun, rockAndRoll, middleFinger, indexFingerDown, holdingBombCrystal, 
        holdingKeyCrystal, crushBombCrystal }

    private HandState _currentHandState;

    [HideInInspector] public int activeGesture;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SwitchHandState(HandState handState)
    {
        _currentHandState = handState;
        switch (_currentHandState)
        {
            //hand gestures
            case HandState.idle:
                ChangeHandGesture(0);

                break;

            case HandState.fist:
                ChangeHandGesture(1);
                break;

            case HandState.fingerPoint:
                ChangeHandGesture(2);
                break;

            case HandState.thumbsUp:
                ChangeHandGesture(3);
                break;

            case HandState.fingerGun:
                ChangeHandGesture(4);
                break;

            case HandState.rockAndRoll:
                ChangeHandGesture(5);
                break;

            case HandState.middleFinger:
                ChangeHandGesture(6);
                break;

            case HandState.indexFingerDown:
                ChangeHandGesture(7);
                break;

            //specific gestures
            case HandState.holdingMap:
                ChangeHandGesture(-1);
                break;

            case HandState.holdingWallet:
                ChangeHandGesture(-2);
                break;

            case HandState.holdingArcaneBomb:
                ChangeHandGesture(-3);
                break;

            case HandState.holdingBowString:
                ChangeHandGesture(-4);
                break;
        }
    }

    private void ChangeHandGesture(int gesture)
    {
        if (activeGesture != gesture)
        {
            _animator.SetInteger("HandState", gesture);
            activeGesture = gesture;
        }
    }

    public HandState GetCurrentHandState()
    {
        return _currentHandState;
    }
}
