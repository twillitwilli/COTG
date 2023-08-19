using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingController : MonoBehaviour
{
    private VRPlayerHand _hand;
    private VRPlayerController _player;
    private GrabController _grabController;

    private VRPlayerHand _oppositeHand;
    private GrabController _oppositeHandGrabController;
    private ClimbingController _oppositeHandClimbController;

    private Vector3 _handStartPos, _climbablePrevPos, _movement;
    private bool _canClimbGrab, _canClimbTrigger, _isClimbing, _startPositionHand;

    private Transform _climbableObject;

    private void Start()
    {
        _hand = GetComponent<VRPlayerHand>();
        _player = _hand.GetPlayer();
        _grabController = _hand.GetGrabController();

        _oppositeHand = _hand.GetOppositeHand();
        _oppositeHandGrabController = _hand.GetGrabController();
        _oppositeHandClimbController = _oppositeHandGrabController.GetClimbingController();
    }

    public void GrabClimbable(bool isTrigger, Transform climbableTransform)
    {
        if (!isTrigger) { _canClimbGrab = true; }
        else { _canClimbTrigger = true; ; }

        if (_canClimbGrab && _canClimbTrigger) 
        {
            _isClimbing = true;
            if (_oppositeHand.CheckIfHoldingSpecificItem(10)) { _oppositeHandClimbController.ClimbingReset(); }
            _handStartPos = _hand.transform.position;
            _climbableObject = climbableTransform;
            _climbablePrevPos = _climbableObject.position;
        }
    }

    public void Climbing()
    {
        _player.playerRB.velocity = new Vector3(0, 0, 0);
        //Turn off Player Movement while climbing
        _player.disableMovement = true;
        _player.playerCollider.enabled = false;
        _player.playerRB.useGravity = false;

        //climbing movement
        //First figure out if/how much the climbable object has moved.
        Vector3 climbableObjectMovement = _climbableObject.position - _climbablePrevPos;
        //Adjust the hand's reference position according to the movement of the climbable object.
        _handStartPos += climbableObjectMovement;
        //Then figure out how much the hand has moved in your play space by checking its current position against the adjusted reference position.
        Vector3 handMovement = _hand.transform.position - _handStartPos;
        //Combine the two movement vectors for the total movement to be applied to the player.
        Vector3 playerMovement = climbableObjectMovement - handMovement;
        //Finally, we add the total movement to the player.
        _player.transform.position += playerMovement;
        //Remember where the climbable object was last frame.
        _climbablePrevPos = _climbableObject.position;
    }

    private void ThrustPlayer()
    {

    }

    public void ClimbingReset()
    {
        _canClimbGrab = false;
        _canClimbTrigger = false;
        _isClimbing = false;
        _climbableObject = null;
        _player.ClimbingCheck(); //still climbing check
    }

    public bool IsClimbing()
    {
        return _isClimbing;
    }
}
