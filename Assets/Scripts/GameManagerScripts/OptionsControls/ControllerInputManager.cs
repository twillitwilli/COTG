using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerInputManager : MonoBehaviour
{
    [SerializeField] 
    private VRPlayerController _player;

    [SerializeField]
    private GrabController[] _grabControllers;

    public VRInputs controls;

    // Joystick Position
    public Vector2 joystickPosLeft { get; private set; }
    public Vector2 joystickPosRight { get; private set; }


    public void EnableControls()
    {
        controls = new VRInputs();

        // Joystick Position
        controls.Left_Hand.JoystickPosition.performed += ctx => joystickPosLeft = ctx.ReadValue<Vector2>();
        controls.Left_Hand.JoystickPosition.canceled += ctx => joystickPosLeft = Vector2.zero;

        controls.Right_Hand.JoystickPosition.performed += ctx => joystickPosRight = ctx.ReadValue<Vector2>();
        controls.Right_Hand.JoystickPosition.canceled += ctx => joystickPosRight = Vector2.zero;

        // Joystick Click
        controls.Left_Hand.JoystickClick.performed += ctx => JoystickClick(0);

        controls.Right_Hand.JoystickClick.performed += ctx => JoystickClick(1);

        // Grab Button
        controls.Left_Hand.GrabButton.performed += ctx => GrabButton(0);
        controls.Left_Hand.GrabButton.canceled += ctx => GrabButton(0, false);

        controls.Right_Hand.GrabButton.performed += ctx => GrabButton(1);
        controls.Right_Hand.GrabButton.canceled += ctx => GrabButton(1, false);

        // Trigger Button
        controls.Left_Hand.TriggerButton.performed += ctx => TriggerButton(0);
        controls.Left_Hand.TriggerButton.canceled += ctx => TriggerButton(0, false);

        controls.Right_Hand.TriggerButton.performed += ctx => TriggerButton(1);
        controls.Right_Hand.TriggerButton.canceled += ctx => TriggerButton(1, false);

        // Primary Button
        controls.Left_Hand.PrimaryButton.performed += ctx => PrimaryButton(0);

        controls.Right_Hand.PrimaryButton.performed += ctx => PrimaryButton(1);
        controls.Right_Hand.PrimaryButton.canceled += ctx => PrimaryButton(1, false);

        // Secondary Button
        controls.Left_Hand.SecondaryButton.performed += ctx => SecondaryButton(0);

        controls.Right_Hand.SeconaryButton.performed += ctx => SecondaryButton(1);
        controls.Right_Hand.SeconaryButton.canceled += ctx => SecondaryButton(1, false);


        controls.Enable();

        SetPrimaryButtonDown(1, true);
        SetPrimaryButtonDown(0, false);
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        JoystickPosition();
    }

    private void JoystickPosition()
    {
        if (!_player.movementDisabled)
        {
            _player.LeftJoystickController(joystickPosLeft);
            _player.RightJoystickController(joystickPosRight);
        }
    }

    public void JoystickClick(int hand)
    {
        // Left Hand
        if (hand == 0)
        {
            if (_player.sprintEnabled)
                _player.SprintController();
        }

        // Right Hand
        else
        {
            if (!_player.physicalJumping && _player.jumpControllerOn)
                _player.JumpController();
        }
    }

    public void GrabButton(int hand, bool buttonClicked = true)
    {
        _grabControllers[hand].UseGrabController(buttonClicked);
    }

    public void TriggerButton(int hand, bool buttonClicked = true)
    {
        _grabControllers[hand].UseTriggerController(buttonClicked);
    }

    public void PrimaryButton(int hand, bool buttonClicked = true)
    {
        if (hand == 0)
        {

        }

        else _player.DashController(buttonClicked);
    }

    public void SecondaryButton(int hand, bool buttonClicked = true)
    {
        if (hand == 0)
        {
            if (PlayerMenu.Instance == null && CheckIfBothHandsEmpty())
                OptionsMenu.Instance.OpenMenu();

            else if (PlayerMenu.Instance != null)
                PlayerMenu.Instance.ClosePlayerMenu();
        }

        else _player.CrouchController(buttonClicked);
    }

    private void SetPrimaryButtonDown(int hand, bool buttonDown)
    {
        if (hand == 1)
            _player.DashController(buttonDown);
    }

    private bool CheckIfBothHandsEmpty()
    {
        foreach (GrabController grabController in _grabControllers)
        {
            if (grabController.CheckIfHoldingAnything())
                return false;
        }

        return true;
    }
}
