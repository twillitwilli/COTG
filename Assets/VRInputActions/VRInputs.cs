// GENERATED AUTOMATICALLY FROM 'Assets/VRInputActions/VRInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @VRInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @VRInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""VRInputs"",
    ""maps"": [
        {
            ""name"": ""Left_Hand"",
            ""id"": ""5d08d00e-5321-442f-a739-63ec6858809a"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""64872231-0f14-4448-9876-aa2f8511ed01"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""98a8a603-5940-4346-9915-1772a9c7cac3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""98145bd3-b83d-4980-96f3-a15f4e7b4b63"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Grab"",
                    ""type"": ""Button"",
                    ""id"": ""205d1d34-0789-45dc-ad4e-c77307bb1db6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Trigger"",
                    ""type"": ""Button"",
                    ""id"": ""b5dffc35-b8e1-4fba-a3c3-ce18784877ec"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Primary_Button"",
                    ""type"": ""Button"",
                    ""id"": ""149ebbc3-c24a-454c-8b7a-bf636eca582b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""JoystickTouch"",
                    ""type"": ""Button"",
                    ""id"": ""59398030-65a2-491c-a071-d5592fcb1257"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e8305988-008b-4c05-ab95-7016816ce3b6"",
                    ""path"": ""<XRController>{LeftHand}/joystick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff00259a-065b-4fd3-9dc2-84932d4d8d55"",
                    ""path"": ""<XRController>{LeftHand}/touchpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a1ce83db-a4f4-46e2-a1be-3bc107a88d3a"",
                    ""path"": ""<WMRSpatialController>{LeftHand}/joystick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f31c970f-f7f2-4e79-b6b0-f800024bee1f"",
                    ""path"": ""<ViveWand>{LeftHand}/trackpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5d74d92-900e-4e3d-9c71-28d9b7cbce57"",
                    ""path"": ""<XRController>{LeftHand}/thumbstickClicked"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e2a7b6b-ffc7-4fed-8ed1-4e9f958d8c2e"",
                    ""path"": ""<XRController>{LeftHand}/touchpadClick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""af8a9e31-4f68-42ad-8562-8f879ff799f6"",
                    ""path"": ""<WMRSpatialController>{LeftHand}/joystickClicked"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dae97cfc-c3c7-4f04-92fe-e0fa98074565"",
                    ""path"": ""<XRController>{LeftHand}/gripPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""78ec9e09-fcef-4484-a245-bb16d478457f"",
                    ""path"": ""<WMRSpatialController>{LeftHand}/gripPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""58baa417-40bf-4e32-b103-5c1911d8ec5d"",
                    ""path"": ""<ViveWand>{LeftHand}/gripPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b80192b9-6943-4303-a4d7-f1589a284644"",
                    ""path"": ""<XRController>{LeftHand}/triggerPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Trigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""686c36ad-e22e-4561-acb0-7eceac3fdf55"",
                    ""path"": ""<WMRSpatialController>{LeftHand}/triggerPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Trigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8ca74f8e-d4b1-4650-92ad-4a6ba53a47b4"",
                    ""path"": ""<ViveWand>{LeftHand}/triggerPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Trigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5baaad9b-dc45-4180-8950-9b752b67e608"",
                    ""path"": ""<XRController>{LeftHand}/secondaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b70da4f4-fee5-462f-9c5c-94973400307c"",
                    ""path"": ""<XRController>{LeftHand}/app"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4f910725-0550-4e23-9f1f-f149dfdc642d"",
                    ""path"": ""<XRController>{LeftHand}/menu"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ec6fc4f-70e9-4c7e-8cf6-78ba62bbe63e"",
                    ""path"": ""<WMRSpatialController>{LeftHand}/menu"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b9b2498-8780-4907-853b-2f35871cf613"",
                    ""path"": ""<ViveWand>{LeftHand}/primary"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1230407f-aeb6-4e64-af5c-ed09b0e21fc6"",
                    ""path"": ""<XRController>{LeftHand}/primaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Primary_Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""26408f4a-0f3b-4a70-87b8-bbe6ed91f8ec"",
                    ""path"": ""<WMRSpatialController>{LeftHand}/touchpadClicked"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Primary_Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4aca2636-53c0-48a2-9dc7-380c9a476838"",
                    ""path"": ""<XRController>{LeftHand}/primaryTouched"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""JoystickTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f0307748-66cf-45a4-a163-6c7ab597d4b5"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""JoystickTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Right_Hand"",
            ""id"": ""9cefcb27-6ca5-4250-82f9-82609efc2889"",
            ""actions"": [
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""9bbe2d98-ec63-4d66-bcce-d9ff8b46fbe6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""a1fcb408-2e2b-40d1-942e-306c88713ee2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CrouchToggle"",
                    ""type"": ""Button"",
                    ""id"": ""6b3ced5a-25ef-4383-922a-1e84f8e772e4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Grab"",
                    ""type"": ""Button"",
                    ""id"": ""29660c6a-812c-4fd9-bfa9-623a77ee6b17"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Trigger"",
                    ""type"": ""Button"",
                    ""id"": ""47896f88-f978-418b-9a7e-cc014ce5c6f3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Primary_Button"",
                    ""type"": ""Button"",
                    ""id"": ""008109f2-08ac-4937-b83c-afd8f765fd2e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""WMRTriggerButtonDown"",
                    ""type"": ""Button"",
                    ""id"": ""8ad53b20-58d1-41b7-ac5d-c472eaf2d4e0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""WMRTriggerButtonUp"",
                    ""type"": ""Button"",
                    ""id"": ""ec1f8160-1253-438c-92f9-758f4f965b8d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""JoystickTouch"",
                    ""type"": ""Button"",
                    ""id"": ""6c0a4d27-4bae-4140-9043-bc4818f880fc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""58cb78a5-c7d1-40cc-b07c-b64a90bb01e5"",
                    ""path"": ""<XRController>{RightHand}/joystick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""420ee3b4-b986-47dd-a01c-d26eaad89a32"",
                    ""path"": ""<XRController>{RightHand}/touchpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e373e2dc-4894-414f-aad9-8ab8c27ade6d"",
                    ""path"": ""<WMRSpatialController>{RightHand}/joystick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""afc6b8e5-65f7-49d1-b7fc-d93a4442aeb5"",
                    ""path"": ""<ViveWand>{RightHand}/trackpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f3864abb-a69b-47c6-b614-c91b69ee0995"",
                    ""path"": ""<XRController>{RightHand}/thumbstickClicked"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""df39461c-d7fe-4efa-9350-82c3c500ceca"",
                    ""path"": ""<XRController>{LeftHand}/touchpadClick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5aa7f0b5-3c8f-41e2-b3b7-9fe1ae7d8061"",
                    ""path"": ""<WMRSpatialController>{RightHand}/joystickClicked"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a262018a-213a-4599-b184-b9e5509a890d"",
                    ""path"": ""<ViveWand>{RightHand}/trackpadPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""18ba16b8-352c-4443-b5e1-d709cbb76bbe"",
                    ""path"": ""<XRController>{RightHand}/secondaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""CrouchToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f3a378a9-ef67-4df3-babe-a2d9ab8907b9"",
                    ""path"": ""<XRController>{RightHand}/app"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""CrouchToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed26b195-e326-4782-a2c0-c23c9b0fc69c"",
                    ""path"": ""<XRController>{RightHand}/menu"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""CrouchToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a1bbed9c-af08-4b4c-a27f-7dec6748b3f6"",
                    ""path"": ""<WMRSpatialController>{RightHand}/menu"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""CrouchToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7b77b661-c706-4f19-8837-e28aaecba17a"",
                    ""path"": ""<ViveWand>{RightHand}/primary"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""CrouchToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ffe0e5fd-debf-4a74-a792-05fb76b78a26"",
                    ""path"": ""<XRController>{RightHand}/gripPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7b52c1fa-da9e-433b-bb64-ece69cfc1c5b"",
                    ""path"": ""<WMRSpatialController>{RightHand}/gripPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f2edb09e-f767-4000-a8bc-f4150cef6fcc"",
                    ""path"": ""<ViveWand>{RightHand}/gripPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b687e9e-f824-4f5b-b6f5-5bc598bbcc81"",
                    ""path"": ""<XRController>{RightHand}/triggerPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Trigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7edcee5-918a-4f85-923b-4464d29f1c13"",
                    ""path"": ""<ViveWand>{RightHand}/triggerPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Trigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e4a94a3a-a4b8-4d24-89b9-07b99c4cd534"",
                    ""path"": ""<XRController>{RightHand}/primaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Primary_Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""644a20b2-4263-4b31-856f-3f3fefb669b3"",
                    ""path"": ""<WMRSpatialController>{RightHand}/touchpadClicked"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""Primary_Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6850f32a-49a3-4724-bdec-da5987cc944d"",
                    ""path"": ""<WMRSpatialController>{RightHand}/triggerPressed"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""WMRTriggerButtonDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a47a6b4c-9176-495a-9b21-b1f3e164eb05"",
                    ""path"": ""<WMRSpatialController>{RightHand}/triggerPressed"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""WMRTriggerButtonUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fff91f74-cd00-40f0-97af-6b490f2a52f6"",
                    ""path"": ""<XRController>{RightHand}/primaryTouched"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VRControls"",
                    ""action"": ""JoystickTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""VRControls"",
            ""bindingGroup"": ""VRControls"",
            ""devices"": [
                {
                    ""devicePath"": ""<XRController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""XboxController"",
            ""bindingGroup"": ""XboxController"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Left_Hand
        m_Left_Hand = asset.FindActionMap("Left_Hand", throwIfNotFound: true);
        m_Left_Hand_Move = m_Left_Hand.FindAction("Move", throwIfNotFound: true);
        m_Left_Hand_Sprint = m_Left_Hand.FindAction("Sprint", throwIfNotFound: true);
        m_Left_Hand_Menu = m_Left_Hand.FindAction("Menu", throwIfNotFound: true);
        m_Left_Hand_Grab = m_Left_Hand.FindAction("Grab", throwIfNotFound: true);
        m_Left_Hand_Trigger = m_Left_Hand.FindAction("Trigger", throwIfNotFound: true);
        m_Left_Hand_Primary_Button = m_Left_Hand.FindAction("Primary_Button", throwIfNotFound: true);
        m_Left_Hand_JoystickTouch = m_Left_Hand.FindAction("JoystickTouch", throwIfNotFound: true);
        // Right_Hand
        m_Right_Hand = asset.FindActionMap("Right_Hand", throwIfNotFound: true);
        m_Right_Hand_Rotate = m_Right_Hand.FindAction("Rotate", throwIfNotFound: true);
        m_Right_Hand_Jump = m_Right_Hand.FindAction("Jump", throwIfNotFound: true);
        m_Right_Hand_CrouchToggle = m_Right_Hand.FindAction("CrouchToggle", throwIfNotFound: true);
        m_Right_Hand_Grab = m_Right_Hand.FindAction("Grab", throwIfNotFound: true);
        m_Right_Hand_Trigger = m_Right_Hand.FindAction("Trigger", throwIfNotFound: true);
        m_Right_Hand_Primary_Button = m_Right_Hand.FindAction("Primary_Button", throwIfNotFound: true);
        m_Right_Hand_WMRTriggerButtonDown = m_Right_Hand.FindAction("WMRTriggerButtonDown", throwIfNotFound: true);
        m_Right_Hand_WMRTriggerButtonUp = m_Right_Hand.FindAction("WMRTriggerButtonUp", throwIfNotFound: true);
        m_Right_Hand_JoystickTouch = m_Right_Hand.FindAction("JoystickTouch", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Left_Hand
    private readonly InputActionMap m_Left_Hand;
    private ILeft_HandActions m_Left_HandActionsCallbackInterface;
    private readonly InputAction m_Left_Hand_Move;
    private readonly InputAction m_Left_Hand_Sprint;
    private readonly InputAction m_Left_Hand_Menu;
    private readonly InputAction m_Left_Hand_Grab;
    private readonly InputAction m_Left_Hand_Trigger;
    private readonly InputAction m_Left_Hand_Primary_Button;
    private readonly InputAction m_Left_Hand_JoystickTouch;
    public struct Left_HandActions
    {
        private @VRInputs m_Wrapper;
        public Left_HandActions(@VRInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Left_Hand_Move;
        public InputAction @Sprint => m_Wrapper.m_Left_Hand_Sprint;
        public InputAction @Menu => m_Wrapper.m_Left_Hand_Menu;
        public InputAction @Grab => m_Wrapper.m_Left_Hand_Grab;
        public InputAction @Trigger => m_Wrapper.m_Left_Hand_Trigger;
        public InputAction @Primary_Button => m_Wrapper.m_Left_Hand_Primary_Button;
        public InputAction @JoystickTouch => m_Wrapper.m_Left_Hand_JoystickTouch;
        public InputActionMap Get() { return m_Wrapper.m_Left_Hand; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Left_HandActions set) { return set.Get(); }
        public void SetCallbacks(ILeft_HandActions instance)
        {
            if (m_Wrapper.m_Left_HandActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnMove;
                @Sprint.started -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnSprint;
                @Menu.started -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnMenu;
                @Menu.performed -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnMenu;
                @Menu.canceled -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnMenu;
                @Grab.started -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnGrab;
                @Grab.performed -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnGrab;
                @Grab.canceled -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnGrab;
                @Trigger.started -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnTrigger;
                @Trigger.performed -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnTrigger;
                @Trigger.canceled -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnTrigger;
                @Primary_Button.started -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnPrimary_Button;
                @Primary_Button.performed -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnPrimary_Button;
                @Primary_Button.canceled -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnPrimary_Button;
                @JoystickTouch.started -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnJoystickTouch;
                @JoystickTouch.performed -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnJoystickTouch;
                @JoystickTouch.canceled -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnJoystickTouch;
            }
            m_Wrapper.m_Left_HandActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Menu.started += instance.OnMenu;
                @Menu.performed += instance.OnMenu;
                @Menu.canceled += instance.OnMenu;
                @Grab.started += instance.OnGrab;
                @Grab.performed += instance.OnGrab;
                @Grab.canceled += instance.OnGrab;
                @Trigger.started += instance.OnTrigger;
                @Trigger.performed += instance.OnTrigger;
                @Trigger.canceled += instance.OnTrigger;
                @Primary_Button.started += instance.OnPrimary_Button;
                @Primary_Button.performed += instance.OnPrimary_Button;
                @Primary_Button.canceled += instance.OnPrimary_Button;
                @JoystickTouch.started += instance.OnJoystickTouch;
                @JoystickTouch.performed += instance.OnJoystickTouch;
                @JoystickTouch.canceled += instance.OnJoystickTouch;
            }
        }
    }
    public Left_HandActions @Left_Hand => new Left_HandActions(this);

    // Right_Hand
    private readonly InputActionMap m_Right_Hand;
    private IRight_HandActions m_Right_HandActionsCallbackInterface;
    private readonly InputAction m_Right_Hand_Rotate;
    private readonly InputAction m_Right_Hand_Jump;
    private readonly InputAction m_Right_Hand_CrouchToggle;
    private readonly InputAction m_Right_Hand_Grab;
    private readonly InputAction m_Right_Hand_Trigger;
    private readonly InputAction m_Right_Hand_Primary_Button;
    private readonly InputAction m_Right_Hand_WMRTriggerButtonDown;
    private readonly InputAction m_Right_Hand_WMRTriggerButtonUp;
    private readonly InputAction m_Right_Hand_JoystickTouch;
    public struct Right_HandActions
    {
        private @VRInputs m_Wrapper;
        public Right_HandActions(@VRInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Rotate => m_Wrapper.m_Right_Hand_Rotate;
        public InputAction @Jump => m_Wrapper.m_Right_Hand_Jump;
        public InputAction @CrouchToggle => m_Wrapper.m_Right_Hand_CrouchToggle;
        public InputAction @Grab => m_Wrapper.m_Right_Hand_Grab;
        public InputAction @Trigger => m_Wrapper.m_Right_Hand_Trigger;
        public InputAction @Primary_Button => m_Wrapper.m_Right_Hand_Primary_Button;
        public InputAction @WMRTriggerButtonDown => m_Wrapper.m_Right_Hand_WMRTriggerButtonDown;
        public InputAction @WMRTriggerButtonUp => m_Wrapper.m_Right_Hand_WMRTriggerButtonUp;
        public InputAction @JoystickTouch => m_Wrapper.m_Right_Hand_JoystickTouch;
        public InputActionMap Get() { return m_Wrapper.m_Right_Hand; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Right_HandActions set) { return set.Get(); }
        public void SetCallbacks(IRight_HandActions instance)
        {
            if (m_Wrapper.m_Right_HandActionsCallbackInterface != null)
            {
                @Rotate.started -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnRotate;
                @Jump.started -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnJump;
                @CrouchToggle.started -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnCrouchToggle;
                @CrouchToggle.performed -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnCrouchToggle;
                @CrouchToggle.canceled -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnCrouchToggle;
                @Grab.started -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnGrab;
                @Grab.performed -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnGrab;
                @Grab.canceled -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnGrab;
                @Trigger.started -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnTrigger;
                @Trigger.performed -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnTrigger;
                @Trigger.canceled -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnTrigger;
                @Primary_Button.started -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnPrimary_Button;
                @Primary_Button.performed -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnPrimary_Button;
                @Primary_Button.canceled -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnPrimary_Button;
                @WMRTriggerButtonDown.started -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnWMRTriggerButtonDown;
                @WMRTriggerButtonDown.performed -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnWMRTriggerButtonDown;
                @WMRTriggerButtonDown.canceled -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnWMRTriggerButtonDown;
                @WMRTriggerButtonUp.started -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnWMRTriggerButtonUp;
                @WMRTriggerButtonUp.performed -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnWMRTriggerButtonUp;
                @WMRTriggerButtonUp.canceled -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnWMRTriggerButtonUp;
                @JoystickTouch.started -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnJoystickTouch;
                @JoystickTouch.performed -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnJoystickTouch;
                @JoystickTouch.canceled -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnJoystickTouch;
            }
            m_Wrapper.m_Right_HandActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @CrouchToggle.started += instance.OnCrouchToggle;
                @CrouchToggle.performed += instance.OnCrouchToggle;
                @CrouchToggle.canceled += instance.OnCrouchToggle;
                @Grab.started += instance.OnGrab;
                @Grab.performed += instance.OnGrab;
                @Grab.canceled += instance.OnGrab;
                @Trigger.started += instance.OnTrigger;
                @Trigger.performed += instance.OnTrigger;
                @Trigger.canceled += instance.OnTrigger;
                @Primary_Button.started += instance.OnPrimary_Button;
                @Primary_Button.performed += instance.OnPrimary_Button;
                @Primary_Button.canceled += instance.OnPrimary_Button;
                @WMRTriggerButtonDown.started += instance.OnWMRTriggerButtonDown;
                @WMRTriggerButtonDown.performed += instance.OnWMRTriggerButtonDown;
                @WMRTriggerButtonDown.canceled += instance.OnWMRTriggerButtonDown;
                @WMRTriggerButtonUp.started += instance.OnWMRTriggerButtonUp;
                @WMRTriggerButtonUp.performed += instance.OnWMRTriggerButtonUp;
                @WMRTriggerButtonUp.canceled += instance.OnWMRTriggerButtonUp;
                @JoystickTouch.started += instance.OnJoystickTouch;
                @JoystickTouch.performed += instance.OnJoystickTouch;
                @JoystickTouch.canceled += instance.OnJoystickTouch;
            }
        }
    }
    public Right_HandActions @Right_Hand => new Right_HandActions(this);
    private int m_VRControlsSchemeIndex = -1;
    public InputControlScheme VRControlsScheme
    {
        get
        {
            if (m_VRControlsSchemeIndex == -1) m_VRControlsSchemeIndex = asset.FindControlSchemeIndex("VRControls");
            return asset.controlSchemes[m_VRControlsSchemeIndex];
        }
    }
    private int m_XboxControllerSchemeIndex = -1;
    public InputControlScheme XboxControllerScheme
    {
        get
        {
            if (m_XboxControllerSchemeIndex == -1) m_XboxControllerSchemeIndex = asset.FindControlSchemeIndex("XboxController");
            return asset.controlSchemes[m_XboxControllerSchemeIndex];
        }
    }
    public interface ILeft_HandActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnMenu(InputAction.CallbackContext context);
        void OnGrab(InputAction.CallbackContext context);
        void OnTrigger(InputAction.CallbackContext context);
        void OnPrimary_Button(InputAction.CallbackContext context);
        void OnJoystickTouch(InputAction.CallbackContext context);
    }
    public interface IRight_HandActions
    {
        void OnRotate(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnCrouchToggle(InputAction.CallbackContext context);
        void OnGrab(InputAction.CallbackContext context);
        void OnTrigger(InputAction.CallbackContext context);
        void OnPrimary_Button(InputAction.CallbackContext context);
        void OnWMRTriggerButtonDown(InputAction.CallbackContext context);
        void OnWMRTriggerButtonUp(InputAction.CallbackContext context);
        void OnJoystickTouch(InputAction.CallbackContext context);
    }
}
