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
                    ""name"": ""JoystickPosition"",
                    ""type"": ""Value"",
                    ""id"": ""64872231-0f14-4448-9876-aa2f8511ed01"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""JoystickClick"",
                    ""type"": ""Button"",
                    ""id"": ""98a8a603-5940-4346-9915-1772a9c7cac3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondaryButton"",
                    ""type"": ""Button"",
                    ""id"": ""98145bd3-b83d-4980-96f3-a15f4e7b4b63"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GrabButton"",
                    ""type"": ""Button"",
                    ""id"": ""205d1d34-0789-45dc-ad4e-c77307bb1db6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TriggerButton"",
                    ""type"": ""Button"",
                    ""id"": ""b5dffc35-b8e1-4fba-a3c3-ce18784877ec"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PrimaryButton"",
                    ""type"": ""Button"",
                    ""id"": ""149ebbc3-c24a-454c-8b7a-bf636eca582b"",
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
                    ""action"": ""JoystickPosition"",
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
                    ""action"": ""JoystickPosition"",
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
                    ""action"": ""JoystickPosition"",
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
                    ""action"": ""JoystickPosition"",
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
                    ""action"": ""JoystickClick"",
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
                    ""action"": ""JoystickClick"",
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
                    ""action"": ""JoystickClick"",
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
                    ""action"": ""GrabButton"",
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
                    ""action"": ""GrabButton"",
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
                    ""action"": ""GrabButton"",
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
                    ""action"": ""TriggerButton"",
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
                    ""action"": ""TriggerButton"",
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
                    ""action"": ""TriggerButton"",
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
                    ""action"": ""SecondaryButton"",
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
                    ""action"": ""SecondaryButton"",
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
                    ""action"": ""SecondaryButton"",
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
                    ""action"": ""SecondaryButton"",
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
                    ""action"": ""SecondaryButton"",
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
                    ""action"": ""PrimaryButton"",
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
                    ""action"": ""PrimaryButton"",
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
                    ""name"": ""JoystickPosition"",
                    ""type"": ""Value"",
                    ""id"": ""9bbe2d98-ec63-4d66-bcce-d9ff8b46fbe6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""JoystickClick"",
                    ""type"": ""Button"",
                    ""id"": ""a1fcb408-2e2b-40d1-942e-306c88713ee2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SeconaryButton"",
                    ""type"": ""Button"",
                    ""id"": ""6b3ced5a-25ef-4383-922a-1e84f8e772e4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GrabButton"",
                    ""type"": ""Button"",
                    ""id"": ""29660c6a-812c-4fd9-bfa9-623a77ee6b17"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TriggerButton"",
                    ""type"": ""Button"",
                    ""id"": ""47896f88-f978-418b-9a7e-cc014ce5c6f3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PrimaryButton"",
                    ""type"": ""Button"",
                    ""id"": ""008109f2-08ac-4937-b83c-afd8f765fd2e"",
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
                    ""action"": ""JoystickPosition"",
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
                    ""action"": ""JoystickPosition"",
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
                    ""action"": ""JoystickPosition"",
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
                    ""action"": ""JoystickPosition"",
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
                    ""action"": ""JoystickClick"",
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
                    ""action"": ""JoystickClick"",
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
                    ""action"": ""JoystickClick"",
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
                    ""action"": ""JoystickClick"",
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
                    ""action"": ""SeconaryButton"",
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
                    ""action"": ""SeconaryButton"",
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
                    ""action"": ""SeconaryButton"",
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
                    ""action"": ""SeconaryButton"",
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
                    ""action"": ""SeconaryButton"",
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
                    ""action"": ""GrabButton"",
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
                    ""action"": ""GrabButton"",
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
                    ""action"": ""GrabButton"",
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
                    ""action"": ""TriggerButton"",
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
                    ""action"": ""TriggerButton"",
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
                    ""action"": ""PrimaryButton"",
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
                    ""action"": ""PrimaryButton"",
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
        m_Left_Hand_JoystickPosition = m_Left_Hand.FindAction("JoystickPosition", throwIfNotFound: true);
        m_Left_Hand_JoystickClick = m_Left_Hand.FindAction("JoystickClick", throwIfNotFound: true);
        m_Left_Hand_SecondaryButton = m_Left_Hand.FindAction("SecondaryButton", throwIfNotFound: true);
        m_Left_Hand_GrabButton = m_Left_Hand.FindAction("GrabButton", throwIfNotFound: true);
        m_Left_Hand_TriggerButton = m_Left_Hand.FindAction("TriggerButton", throwIfNotFound: true);
        m_Left_Hand_PrimaryButton = m_Left_Hand.FindAction("PrimaryButton", throwIfNotFound: true);
        // Right_Hand
        m_Right_Hand = asset.FindActionMap("Right_Hand", throwIfNotFound: true);
        m_Right_Hand_JoystickPosition = m_Right_Hand.FindAction("JoystickPosition", throwIfNotFound: true);
        m_Right_Hand_JoystickClick = m_Right_Hand.FindAction("JoystickClick", throwIfNotFound: true);
        m_Right_Hand_SeconaryButton = m_Right_Hand.FindAction("SeconaryButton", throwIfNotFound: true);
        m_Right_Hand_GrabButton = m_Right_Hand.FindAction("GrabButton", throwIfNotFound: true);
        m_Right_Hand_TriggerButton = m_Right_Hand.FindAction("TriggerButton", throwIfNotFound: true);
        m_Right_Hand_PrimaryButton = m_Right_Hand.FindAction("PrimaryButton", throwIfNotFound: true);
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
    private readonly InputAction m_Left_Hand_JoystickPosition;
    private readonly InputAction m_Left_Hand_JoystickClick;
    private readonly InputAction m_Left_Hand_SecondaryButton;
    private readonly InputAction m_Left_Hand_GrabButton;
    private readonly InputAction m_Left_Hand_TriggerButton;
    private readonly InputAction m_Left_Hand_PrimaryButton;
    public struct Left_HandActions
    {
        private @VRInputs m_Wrapper;
        public Left_HandActions(@VRInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @JoystickPosition => m_Wrapper.m_Left_Hand_JoystickPosition;
        public InputAction @JoystickClick => m_Wrapper.m_Left_Hand_JoystickClick;
        public InputAction @SecondaryButton => m_Wrapper.m_Left_Hand_SecondaryButton;
        public InputAction @GrabButton => m_Wrapper.m_Left_Hand_GrabButton;
        public InputAction @TriggerButton => m_Wrapper.m_Left_Hand_TriggerButton;
        public InputAction @PrimaryButton => m_Wrapper.m_Left_Hand_PrimaryButton;
        public InputActionMap Get() { return m_Wrapper.m_Left_Hand; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Left_HandActions set) { return set.Get(); }
        public void SetCallbacks(ILeft_HandActions instance)
        {
            if (m_Wrapper.m_Left_HandActionsCallbackInterface != null)
            {
                @JoystickPosition.started -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnJoystickPosition;
                @JoystickPosition.performed -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnJoystickPosition;
                @JoystickPosition.canceled -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnJoystickPosition;
                @JoystickClick.started -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnJoystickClick;
                @JoystickClick.performed -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnJoystickClick;
                @JoystickClick.canceled -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnJoystickClick;
                @SecondaryButton.started -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnSecondaryButton;
                @SecondaryButton.performed -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnSecondaryButton;
                @SecondaryButton.canceled -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnSecondaryButton;
                @GrabButton.started -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnGrabButton;
                @GrabButton.performed -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnGrabButton;
                @GrabButton.canceled -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnGrabButton;
                @TriggerButton.started -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnTriggerButton;
                @TriggerButton.performed -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnTriggerButton;
                @TriggerButton.canceled -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnTriggerButton;
                @PrimaryButton.started -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnPrimaryButton;
                @PrimaryButton.performed -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnPrimaryButton;
                @PrimaryButton.canceled -= m_Wrapper.m_Left_HandActionsCallbackInterface.OnPrimaryButton;
            }
            m_Wrapper.m_Left_HandActionsCallbackInterface = instance;
            if (instance != null)
            {
                @JoystickPosition.started += instance.OnJoystickPosition;
                @JoystickPosition.performed += instance.OnJoystickPosition;
                @JoystickPosition.canceled += instance.OnJoystickPosition;
                @JoystickClick.started += instance.OnJoystickClick;
                @JoystickClick.performed += instance.OnJoystickClick;
                @JoystickClick.canceled += instance.OnJoystickClick;
                @SecondaryButton.started += instance.OnSecondaryButton;
                @SecondaryButton.performed += instance.OnSecondaryButton;
                @SecondaryButton.canceled += instance.OnSecondaryButton;
                @GrabButton.started += instance.OnGrabButton;
                @GrabButton.performed += instance.OnGrabButton;
                @GrabButton.canceled += instance.OnGrabButton;
                @TriggerButton.started += instance.OnTriggerButton;
                @TriggerButton.performed += instance.OnTriggerButton;
                @TriggerButton.canceled += instance.OnTriggerButton;
                @PrimaryButton.started += instance.OnPrimaryButton;
                @PrimaryButton.performed += instance.OnPrimaryButton;
                @PrimaryButton.canceled += instance.OnPrimaryButton;
            }
        }
    }
    public Left_HandActions @Left_Hand => new Left_HandActions(this);

    // Right_Hand
    private readonly InputActionMap m_Right_Hand;
    private IRight_HandActions m_Right_HandActionsCallbackInterface;
    private readonly InputAction m_Right_Hand_JoystickPosition;
    private readonly InputAction m_Right_Hand_JoystickClick;
    private readonly InputAction m_Right_Hand_SeconaryButton;
    private readonly InputAction m_Right_Hand_GrabButton;
    private readonly InputAction m_Right_Hand_TriggerButton;
    private readonly InputAction m_Right_Hand_PrimaryButton;
    public struct Right_HandActions
    {
        private @VRInputs m_Wrapper;
        public Right_HandActions(@VRInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @JoystickPosition => m_Wrapper.m_Right_Hand_JoystickPosition;
        public InputAction @JoystickClick => m_Wrapper.m_Right_Hand_JoystickClick;
        public InputAction @SeconaryButton => m_Wrapper.m_Right_Hand_SeconaryButton;
        public InputAction @GrabButton => m_Wrapper.m_Right_Hand_GrabButton;
        public InputAction @TriggerButton => m_Wrapper.m_Right_Hand_TriggerButton;
        public InputAction @PrimaryButton => m_Wrapper.m_Right_Hand_PrimaryButton;
        public InputActionMap Get() { return m_Wrapper.m_Right_Hand; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Right_HandActions set) { return set.Get(); }
        public void SetCallbacks(IRight_HandActions instance)
        {
            if (m_Wrapper.m_Right_HandActionsCallbackInterface != null)
            {
                @JoystickPosition.started -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnJoystickPosition;
                @JoystickPosition.performed -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnJoystickPosition;
                @JoystickPosition.canceled -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnJoystickPosition;
                @JoystickClick.started -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnJoystickClick;
                @JoystickClick.performed -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnJoystickClick;
                @JoystickClick.canceled -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnJoystickClick;
                @SeconaryButton.started -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnSeconaryButton;
                @SeconaryButton.performed -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnSeconaryButton;
                @SeconaryButton.canceled -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnSeconaryButton;
                @GrabButton.started -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnGrabButton;
                @GrabButton.performed -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnGrabButton;
                @GrabButton.canceled -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnGrabButton;
                @TriggerButton.started -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnTriggerButton;
                @TriggerButton.performed -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnTriggerButton;
                @TriggerButton.canceled -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnTriggerButton;
                @PrimaryButton.started -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnPrimaryButton;
                @PrimaryButton.performed -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnPrimaryButton;
                @PrimaryButton.canceled -= m_Wrapper.m_Right_HandActionsCallbackInterface.OnPrimaryButton;
            }
            m_Wrapper.m_Right_HandActionsCallbackInterface = instance;
            if (instance != null)
            {
                @JoystickPosition.started += instance.OnJoystickPosition;
                @JoystickPosition.performed += instance.OnJoystickPosition;
                @JoystickPosition.canceled += instance.OnJoystickPosition;
                @JoystickClick.started += instance.OnJoystickClick;
                @JoystickClick.performed += instance.OnJoystickClick;
                @JoystickClick.canceled += instance.OnJoystickClick;
                @SeconaryButton.started += instance.OnSeconaryButton;
                @SeconaryButton.performed += instance.OnSeconaryButton;
                @SeconaryButton.canceled += instance.OnSeconaryButton;
                @GrabButton.started += instance.OnGrabButton;
                @GrabButton.performed += instance.OnGrabButton;
                @GrabButton.canceled += instance.OnGrabButton;
                @TriggerButton.started += instance.OnTriggerButton;
                @TriggerButton.performed += instance.OnTriggerButton;
                @TriggerButton.canceled += instance.OnTriggerButton;
                @PrimaryButton.started += instance.OnPrimaryButton;
                @PrimaryButton.performed += instance.OnPrimaryButton;
                @PrimaryButton.canceled += instance.OnPrimaryButton;
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
        void OnJoystickPosition(InputAction.CallbackContext context);
        void OnJoystickClick(InputAction.CallbackContext context);
        void OnSecondaryButton(InputAction.CallbackContext context);
        void OnGrabButton(InputAction.CallbackContext context);
        void OnTriggerButton(InputAction.CallbackContext context);
        void OnPrimaryButton(InputAction.CallbackContext context);
    }
    public interface IRight_HandActions
    {
        void OnJoystickPosition(InputAction.CallbackContext context);
        void OnJoystickClick(InputAction.CallbackContext context);
        void OnSeconaryButton(InputAction.CallbackContext context);
        void OnGrabButton(InputAction.CallbackContext context);
        void OnTriggerButton(InputAction.CallbackContext context);
        void OnPrimaryButton(InputAction.CallbackContext context);
    }
}
