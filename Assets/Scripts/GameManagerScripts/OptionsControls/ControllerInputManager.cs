using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerInputManager : MonoBehaviour
{
    private ControllerType _controllerType;

    [SerializeField] private VRPlayerController _player;
    [SerializeField] private VRPlayerHand[] _hands;

    private PlayerComponents _playerComponents;
    private HandAnimationState _handAnimator;

    [System.Flags] public enum ButtonDown { trigger = 1, grab = 2, thumbDown = 4 }
    private ButtonDown _currentButtonsDown;

    public VRInputs controls;
    private Vector2 _leftPos, _rightPos;
    private bool _canUseJumpButton = true, _triggerDown, _grabDown, _crouchButtonDown;

    public void EnableControls()
    {
        controls = new VRInputs();

        //left controls
        controls.Left_Hand.Move.performed += ctx => _leftPos = ctx.ReadValue<Vector2>();
        controls.Left_Hand.Move.canceled += ctx => _leftPos = Vector2.zero;

        controls.Left_Hand.Sprint.performed += ctx => LeftJoystickClick();

        controls.Left_Hand.Grab.performed += ctx => GrabButton(0, true);
        controls.Left_Hand.Grab.canceled += ctx => GrabButton(0, false);

        controls.Left_Hand.Trigger.performed += ctx => TriggerButton(0, true);
        controls.Left_Hand.Trigger.canceled += ctx => TriggerButton(0, false);

        controls.Left_Hand.Menu.performed += ctx => MenuButton();

        controls.Left_Hand.Primary_Button.performed += ctx => PrimaryButtonDown(0, true);
        controls.Left_Hand.Primary_Button.canceled += ctx => PrimaryButtonDown(0, false);

        controls.Left_Hand.JoystickTouch.performed += ctx => JoystickTouched(0, true);
        controls.Left_Hand.JoystickTouch.canceled += ctx => JoystickTouched(0, false);

        //right controls
        controls.Right_Hand.Rotate.performed += ctx => _rightPos = ctx.ReadValue<Vector2>();
        controls.Right_Hand.Rotate.canceled += ctx => _rightPos = Vector2.zero;

        controls.Right_Hand.Jump.started += ctx => RightJoystickClick(true);
        controls.Right_Hand.Jump.canceled += ctx => RightJoystickClick(false);

        controls.Right_Hand.Grab.performed += ctx => GrabButton(1, true);
        controls.Right_Hand.Grab.canceled += ctx => GrabButton(1, false);

        controls.Right_Hand.CrouchToggle.performed += ctx => CrouchToggle(true);
        controls.Right_Hand.CrouchToggle.canceled += ctx => CrouchToggle(false);

        controls.Right_Hand.Primary_Button.performed += ctx => PrimaryButtonDown(1, true);
        controls.Right_Hand.Primary_Button.canceled += ctx => PrimaryButtonDown(1, false);

        controls.Right_Hand.JoystickTouch.performed += ctx => JoystickTouched(1, true);
        controls.Right_Hand.JoystickTouch.canceled += ctx => JoystickTouched(1, false);

        _controllerType = LocalGameManager.Instance.GetControllerType();

        //specific to controllers
        switch (_controllerType.currentControllerType)
        {
            case ControllerType.controllerType.oculusRift:
                controls.Right_Hand.Trigger.performed += ctx => TriggerButton(1, true);
                controls.Right_Hand.Trigger.canceled += ctx => TriggerButton(1, false);
                break;

            case ControllerType.controllerType.index:
                controls.Right_Hand.Trigger.performed += ctx => TriggerButton(1, true);
                controls.Right_Hand.Trigger.canceled += ctx => TriggerButton(1, false);
                break;

            case ControllerType.controllerType.wmr:
                controls.Right_Hand.WMRTriggerButtonDown.performed += ctx => TriggerButton(1, true);
                controls.Right_Hand.WMRTriggerButtonUp.canceled += ctx => TriggerButton(1, false);
                break;
        }

        controls.Enable();
        _canUseJumpButton = true;
        PrimaryButtonDown(1, true);
        PrimaryButtonDown(0, false);
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        _player.LeftJoystickController(_leftPos);
        _player.RightJoystickController(_rightPos);
    }

    private void LeftJoystickClick()
    {
        if (_player.sprintEnabled) { _player.SprintController(); }
    }

    private void RightJoystickClick(bool jumpButtonDown)
    {
        if (_canUseJumpButton && !_player.physicalJumping)
        {
            _canUseJumpButton = false;
            if (!_player.canFly && _player.jumpControllerOn) { _player.JumpController(); }
        }

        if (_player.canFly) { _player.FlightController(jumpButtonDown); }

        if (!jumpButtonDown) { _canUseJumpButton = true; }
    }

    private void JoystickTouched(int hand, bool touched)
    {

        
    }

    private void GrabButton(int hand, bool grabButtonDown)
    {
        if (VerifyButtonStatus(_grabDown, grabButtonDown))
        {
            _hands[hand].GetGrabController().UseGrabController(grabButtonDown);
        } 
    }

    private void TriggerButton(int hand, bool triggerButtonDown)
    {
        if (VerifyButtonStatus(_triggerDown, triggerButtonDown))
        {
            if (!_triggerDown && triggerButtonDown) { _triggerDown = true; }
            else if (_triggerDown && !triggerButtonDown) { _triggerDown = false; }

            if (!_player.selectingClass && !_player.menuSpawned)
            {
                _playerComponents.GetHand(hand).GetGrabController().UseTriggerController(triggerButtonDown);
            }

            else if (_player.selectingClass || !_player.selectingClass && triggerButtonDown && _player.menuSpawned || LocalGameManager.Instance.currentGameMode == LocalGameManager.GameMode.inLobby && triggerButtonDown)
            {
                _hands[hand].GetMenuRaycast().ShootRaycast();
                return;
            }
        }
    }

    private void MenuButton()
    {
        if (PlayerMenu.instance == null && !_player.selectingClass && !_hands[0].GetGrabController().CheckIfHoldingAnything() && !_hands[1].GetGrabController().CheckIfHoldingAnything())
        {
            LocalGameManager.Instance.GetOptionsMenu().OpenMenu();
        }
        else if (PlayerMenu.instance != null)
        {
            Destroy(PlayerMenu.instance.gameObject);
        }
    }

    private void PrimaryButtonDown(int hand, bool buttonDown)
    {
        if (hand == 1)
        {
            if (buttonDown) { _player.DashController(true); }
            else { _player.DashController(false); }
        }
    }

    private void CrouchToggle(bool buttonDown)
    {
        if (VerifyButtonStatus(buttonDown, _crouchButtonDown))
        {
            if (!_crouchButtonDown && buttonDown) { _crouchButtonDown = true; }
            else if (_crouchButtonDown && !buttonDown) { _crouchButtonDown = false; }
            if (_crouchButtonDown && buttonDown)
                if (!_player.playerStanding && buttonDown)
                {
                    _player.SittingHeightController();
                }
                else if (!_player.playerStanding && !buttonDown && _player.isCrouched)
                {
                    Debug.Log("Crouch button up");
                    _player.sittingPlayerAnim.SetBool("isCrouched", false);
                    _player.CrouchController(false);
                }
        }
    }

    private bool VerifyButtonStatus(bool buttonStateDown, bool currentlyPressingButtonDown)
    {
        if (currentlyPressingButtonDown && buttonStateDown) { return false; }
        else if (!currentlyPressingButtonDown && !buttonStateDown) { return false; }
        else return true;
    }

    public ButtonDown GetCurrentButtonsDown()
    {
        return _currentButtonsDown;
    }
}
