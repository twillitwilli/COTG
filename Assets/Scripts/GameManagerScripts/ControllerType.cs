using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System.Threading.Tasks;
using QTArts.AbstractClasses;

public class ControllerType : MonoSingleton<ControllerType>
{
    public string currentController { get; private set; }
    public string controllerFullName { get; private set; }

    CheckControllerType _controllerType;
    VRPlayer _player;

    public enum controllerType 
    { 
        oculusRift, 
        index, 
        wmr, // Not Implemented or Tested
        vive, // Not Implemented or Tested
        quest2, // Not Implemented or Tested
        custom 
    }
    
    public controllerType currentControllerType { get; private set; }
    
    public string controllerName { get; set; }
    
    public int controllerID { get; set; } //0 = oculus, 1 = index, 2 = wmr, 3 = vive

    public int roomID { get; set; }

    public override void Awake()
    {
        base.Awake();

        LocalGameManager.playerCreated += NewPlayerCreated;

        _controllerType = MasterManager.controllerType;

        //CheckController();
    }

    //public void CheckController()
    //{
    //    var rightInput = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    //    if (rightInput.manufacturer == null) { RecheckController(); }
    //    else { currentController = rightInput.manufacturer; }

    //    Debug.Log("controller name: " + rightInput.manufacturer);
    //}

    public async void NewPlayerCreated(VRPlayer player)
    {
        _player = player;

        var rightInput = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (rightInput.manufacturer == null) 
        { 
            await RecheckController(player);
            return;
        }

        else
            currentController = rightInput.manufacturer;

        Debug.Log("controller name: " + rightInput.manufacturer);

        SetController(currentController);
    }

    public async Task RecheckController(VRPlayer player)
    {
        await Task.Delay(3000);

        NewPlayerCreated(player);
    }

    public void OculusReset(VRHand hand)
    {
        if (!hand.IsRightHand())
            HandAlignmentReset(hand, _controllerType.oculusPos[0], _controllerType.oculusRot[0]);

        else
            HandAlignmentReset(hand, _controllerType.oculusPos[1], _controllerType.oculusRot[1]);
    }

    public void IndexReset(VRHand hand)
    {
        if (!hand.IsRightHand())
            HandAlignmentReset(hand, _controllerType.indexPos[0], _controllerType.indexRot[0]);

        else
            HandAlignmentReset(hand, _controllerType.indexPos[1], _controllerType.indexRot[1]);
    }

    public void WMRReset(VRHand hand)
    {
        if (!hand.IsRightHand())
            HandAlignmentReset(hand, _controllerType.wmrPos[0], _controllerType.wmrRot[0]);

        else
            HandAlignmentReset(hand, _controllerType.wmrPos[1], _controllerType.wmrRot[1]);
    }

    public void ViveReset(VRHand hand)
    {
        if (!hand.IsRightHand())
            HandAlignmentReset(hand, _controllerType.vivePos[0], _controllerType.viveRot[0]);

        else
            HandAlignmentReset(hand, _controllerType.vivePos[1], _controllerType.viveRot[1]);
    }

    public void OculusQuest2Reset(VRHand hand)
    {
        if (!hand.IsRightHand())
            HandAlignmentReset(hand, _controllerType.quest2Pos[0], _controllerType.quest2Rot[0]);

        else
            HandAlignmentReset(hand, _controllerType.quest2Pos[1], _controllerType.quest2Rot[1]);
    }

    public void CustomHandAlignment(VRHand hand)
    {
        if (!hand.IsRightHand()) { }
        else { }
    }

    public void HandAlignmentReset(VRHand hand, Vector3 pos, Vector3 rot)
    {
        hand.defaultHandPos = pos;
        hand.defaultHandRot = rot;
        hand.ResetHandAlignment();
    }

    public void SetController(string nameOfController)
    {
        switch (nameOfController)
        {
            case "Oculus":
                currentControllerType = controllerType.oculusRift;
                controllerFullName = "Oculus Rift";
                break;

            case "Valve":
                currentControllerType = controllerType.index;
                controllerFullName = "Valve Index";
                break;

            default: controllerFullName = "Controller Not Supported";
                break;
        }

        PlayerComponents playerComponents = _player.GetPlayerComponents();

        foreach (VRHand hand in playerComponents.GetBothHands())
        {
            ResetHandToControllerDefault(hand);
        }

        playerComponents.GetControllerInputManager().EnableControls();
    }

    public void ResetHandToControllerDefault(VRHand hand)
    {
        switch (currentControllerType)
        {
            case controllerType.oculusRift:
                OculusReset(hand);
                break;

            case controllerType.index:
                IndexReset(hand);
                break;

            case controllerType.wmr:
                WMRReset(hand);
                break;

            case controllerType.vive:
                ViveReset(hand);
                break;

            case controllerType.quest2:
                OculusQuest2Reset(hand);
                break;
        }
    }
}
