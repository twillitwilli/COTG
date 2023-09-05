using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerOptions : MonoBehaviour
{
    public enum Options 
    { 
        playmode, 
        primaryHand, 
        clockFormat, 
        playerOrientation, 
        playerRotation, 
        roomScale, 
        gripType, 
        leftJoystickSensitivty,
        rightJoystickSensitivty, 
        smoothTurning, 
        snapTurning, 
        musicVolume, 
        adjustHandPositioning, 
        changeControllerType, 
        sfxVolume, 
        sprintToggle,
        playerCalibration, 
        clearHands, 
        openDiscordLink, 
        creatureSFX, 
        resetHandAlignment, 
        keyboard, 
        joinCreateServer, 
        chat, 
        textChat, 
        debugChat,
        voiceChat, 
        notifications, 
        shadowType, 
        shadowQuality, 
        lightRange, 
        lightBrightness, 
        ambientOcclusionToggle, 
        bloomToggle, 
        colorGradingToggle,
        ambientOcclusionIntensity, 
        ambientOcclusionThickness, 
        bloomIntensity, 
        bloomThreshold, 
        bloomDiffusion, 
        colorGradingTonemapper,
        colorGradingTemperature, 
        colorGradingTint, 
        colorGradingPostExposure, 
        colorGradingHueShift, 
        colorGradingSaturation, 
        colorGradingContrast,
        backAttachments, 
        beltAdjustment, 
        sittingBeltOffset, 
        onScreenText, 
        chatHand, 
        cheatOptions, 
        physicalJumpingToggle
    }

    public Options playerOptions;

    [SerializeField] 
    private PlayerMenu _menu;
    
    [SerializeField] 
    private Text _textBox;
    
    [SerializeField] 
    private bool _checkStatusOnly;
    
    [SerializeField] 
    private float _valueAdjustment;
    
    [SerializeField] 
    private string _stringName;
    
    private void Start()
    {        
        if (_checkStatusOnly) { ChangePlayerOptions(); }
    }

    public void ChangePlayerOptions()
    {
        switch (playerOptions)
        {
            /// Main Menu Options ///

            case Options.playmode:
                ChangePlayMode();
                break;

            /// Audio Options ///

            case Options.musicVolume:
                ChangeMusicVolume();
                break;

            case Options.sfxVolume:
                ChangeSFXVolume();
                break;

            case Options.creatureSFX:
                ChangeCreatureSFXVolume();
                break;

            /// Graphics Options ///

            // Lighting Options //

            case Options.shadowType:
                ToggleShadowType();
                break;

            case Options.shadowQuality:
                ToggleShadowQuality();
                break;

            case Options.lightRange:
                AdjustLightingRange();
                break;

            case Options.lightBrightness:
                AdjustLightingBrightness();
                break;

            // Post Processing Settings //

            case Options.ambientOcclusionToggle:
                AmbientOcclusionToggle();
                break;

            case Options.bloomToggle:
                BloomToggle();
                break;

            case Options.colorGradingToggle:
                ColorGradingToggle();
                break;

            case Options.ambientOcclusionIntensity:
                AmbientOcclusionIntensity();
                break;

            case Options.ambientOcclusionThickness:
                AmbientOcclusionThickness();
                break;

            case Options.bloomIntensity:
                BloomIntensity();
                break;

            case Options.bloomThreshold:
                BloomThreshold();
                break;

            case Options.bloomDiffusion:
                BloomDiffusion();
                break;

            case Options.colorGradingTonemapper:
                ColorGradingTonemapper();
                break;

            case Options.colorGradingTemperature:
                ColorGradingTemperature();
                break;

            case Options.colorGradingTint:
                ColorGradingTint();
                break;

            case Options.colorGradingPostExposure:
                ColorGradingPostExposure();
                break;

            case Options.colorGradingHueShift:
                ColorGradingHueShift();
                break;

            case Options.colorGradingSaturation:
                ColorGradingSaturation();
                break;

            case Options.colorGradingContrast:
                ColorGradingContrast();
                break;

            /// Control Settings ///

            // Player Calibration Settings //

            case Options.primaryHand:
                ChangePrimaryHand();
                break;

            case Options.adjustHandPositioning:
                AdjustHandPositioning();
                break;

            case Options.changeControllerType:
                ChangeControllerType();
                break;

            case Options.playerCalibration:
                PlayerCalibration();
                break;

            case Options.clearHands:
                ClearHands();
                break;

            case Options.resetHandAlignment:
                ResetHandAlignment();
                break;

            // Player Attachments //

            case Options.backAttachments:
                BackAttachmentAdjustment();
                break;

            case Options.beltAdjustment:
                BeltAdjustment();
                break;

            case Options.sittingBeltOffset:
                BeltOffset();
                break;

            // Locomotion Settings //

            case Options.playerOrientation:
                ChangePlayerOrientation();
                break;

            case Options.roomScale:
                ToggleRoomScale();
                break;

            case Options.playerRotation:
                ChangePlayerRotation();
                break;

            case Options.sprintToggle:
                SprintToggle();
                break;

            case Options.physicalJumpingToggle:
                PhysicalJumpingToggle();
                break;

            // Controller Options //

            case Options.gripType:
                ChangeGrip();
                break;

            case Options.leftJoystickSensitivty:
                LeftControllerSensitivty();
                break;

            case Options.rightJoystickSensitivty:
                RightControllerSensitivty();
                break;

            case Options.smoothTurning:
                SmoothTurningAdjustment();
                break;

            case Options.snapTurning:
                SnapTurningAdjustment();
                break;

            /// Multiplayer & Notification Settings///

            case Options.textChat:
                ToggleTextChat();
                break;

            case Options.debugChat:
                ToggleDebugChat();
                break;

            case Options.voiceChat:
                ToggleVoiceChat();
                break;

            case Options.notifications:
                ToggleNotifications();
                break;

            case Options.onScreenText:
                OnScreenTextPosition();
                break;

            case Options.chatHand:
                ChangeChatHand();
                break;

            /// Misc Settings ///

            case Options.clockFormat:
                ChangeTimeDisplay();
                break;

            case Options.chat:
                ChatOptions();
                break;

            case Options.openDiscordLink:
                OpenDiscordLink();
                break;

            /// Keyboard Options ///

            case Options.keyboard:
                KeyboardTyping();
                break;

            case Options.joinCreateServer:
                JoinCreateServer();
                break;

            case Options.cheatOptions:
                CheatOptions();
                break;
        }
    }

    private void ChangeText(string newText)
    {
        _textBox.text = newText;
    }

    private void ChangePlayMode()
    {
        VRPlayerController player = LocalGameManager.Instance.player;

        if (!_checkStatusOnly)
        {
            player.heightCheck = true;
            
            if (player.playerStanding)
            {
                player.playerStanding = false;
                player.isCrouched = false;
                player.SittingHeightController();
            }

            else 
                player.playerStanding = true;
        }

        ChangeText(player.playerStanding ? "Playmode:\nStanding" : "Playmode:\nSitting");
    }

    private void ChangePrimaryHand()
    {
        VRPlayerController player = LocalGameManager.Instance.player;

        if (!_checkStatusOnly)
        {
            bool leftHanded = player.isLeftHanded ? false : true;
            player.isLeftHanded = leftHanded;

            Array.ForEach(player.GetPlayerComponents().GetBothHands(), setPrimaryHand => setPrimaryHand.SetPrimaryHand());

            switch (MagicController.Instance.currentClass)
            {
                case MagicController.ClassType.Wizard:
                    Wizard.instance.GetStaffController().ResetStaff();
                    break;

                case MagicController.ClassType.Conjurer:
                    Conjurer.instance.GetBowController().ResetBow();
                    break;
            }
        }

        ChangeText(player.isLeftHanded ? "Left Handed" : "Right Handed");
    }

    private void ChangeTimeDisplay()
    {
        VRPlayerController player = LocalGameManager.Instance.player;

        if (!_checkStatusOnly)
        {
            bool timeFormat = player.militaryTime ? false : true;
            player.militaryTime = timeFormat;
        }

        ChangeText(player.militaryTime ? "Time Format:\n24hr" : "Time Format:\n12hr");
    }

    private void ChangePlayerOrientation()
    {
        VRPlayerController player = LocalGameManager.Instance.player;

        if (!_checkStatusOnly)
        {
            bool playerOrientation = player.headOrientation ? false : true;
            player.headOrientation = playerOrientation;

            player.OrientationSource();
        }

        ChangeText(player.headOrientation ? "Orientation:\nHead" : "Orientation:\nHand");
    }

    private void ChangePlayerRotation()
    {
        VRPlayerController player = LocalGameManager.Instance.player;

        if (!_checkStatusOnly)
        {
            bool snapRotation = player.snapTurnOn ? false : true;
            player.snapTurnOn = snapRotation;
        }

        ChangeText(player.snapTurnOn ? "Snap Turning" : "Smooth Turning");
    }

    private void ToggleRoomScale()
    {
        VRPlayerController player = LocalGameManager.Instance.player;

        if (!_checkStatusOnly)
        {
            bool roomScale = player.roomScale ? false : true;
            player.roomScale = roomScale;
        }

        ChangeText(player.roomScale ? "Roomscale:\nOn" : "Roomscale:\nOff");
    }

    private void ChangeGrip()
    {
        VRPlayerController player = LocalGameManager.Instance.player;

        if (!_checkStatusOnly)
        {
            bool toggleGrip = player.toggleGrip ? false : true;
            player.toggleGrip = toggleGrip;
        }

        ChangeText(player.toggleGrip ? "Toggle Grip" : "Hold Grip");
    }

    private void LeftControllerSensitivty()
    {
        VRPlayerController player = LocalGameManager.Instance.player;

        if (!_checkStatusOnly)
        {
            player.leftJoystickDeadzoneAdjustment += _valueAdjustment;

            if (player.leftJoystickDeadzoneAdjustment >= .9f)
                player.leftJoystickDeadzoneAdjustment = .9f;

            else if (player.leftJoystickDeadzoneAdjustment <= 0)
                player.leftJoystickDeadzoneAdjustment = 0;
        }

        int deadzoneValue = Mathf.RoundToInt(player.leftJoystickDeadzoneAdjustment * 100);

        ChangeText("Left Sensitivity:\n" + deadzoneValue + "%");
    }

    private void RightControllerSensitivty()
    {
        VRPlayerController player = LocalGameManager.Instance.player;

        if (!_checkStatusOnly)
        {
            player.rightJoystickDeadzoneAdjustment += _valueAdjustment;

            if (player.rightJoystickDeadzoneAdjustment >= .9f)
                player.rightJoystickDeadzoneAdjustment = .9f;

            else if (player.rightJoystickDeadzoneAdjustment <= 0)
                player.rightJoystickDeadzoneAdjustment = 0;
        }

        int deadzoneValue = Mathf.RoundToInt(player.rightJoystickDeadzoneAdjustment * 100);

        ChangeText("Right Sensitivity:\n" + deadzoneValue + "%");
    }

    private void SmoothTurningAdjustment()
    {
        VRPlayerController player = LocalGameManager.Instance.player;

        if (!_checkStatusOnly)
        {
            player.turnSpeedAdjustment += _valueAdjustment;

            if (player.turnSpeedAdjustment >= 4f)
                player.turnSpeedAdjustment = 4f;

            else if (player.turnSpeedAdjustment <= .1f)
                player.turnSpeedAdjustment = .1f;
        }

        int turnSpeed = Mathf.RoundToInt(player.turnSpeedAdjustment * 100);

        ChangeText("Smooth Turning:\n" + turnSpeed + "%");
    }

    private void SnapTurningAdjustment()
    {
        VRPlayerController player = LocalGameManager.Instance.player;

        if (!_checkStatusOnly)
        {
            player.snapTurnRotationAdjustment += _valueAdjustment;

            if (player.snapTurnRotationAdjustment >= 90f)
                player.snapTurnRotationAdjustment = 90f;

            else if (player.snapTurnRotationAdjustment <= 10f)
                player.snapTurnRotationAdjustment = 10f;
        }
        
        ChangeText("Snap Turning:\n" + Mathf.RoundToInt(player.snapTurnRotationAdjustment));
    }

    private void ChangeMusicVolume()
    {
        if (!_checkStatusOnly)
            AudioController.Instance.AdjustMusicVolume(_valueAdjustment);

        ChangeText("Music: " + Mathf.RoundToInt(AudioController.Instance.GetMusicVolume() * 100));
    }

    private void ChangeSFXVolume()
    {
        if (!_checkStatusOnly)
            AudioController.Instance.AdjustSFXVolume(_valueAdjustment);

        ChangeText("SFX: " + Mathf.RoundToInt(AudioController.Instance.GetSFXVolume() * 100));
    }

    private void ChangeCreatureSFXVolume()
    {
        if (!_checkStatusOnly)
            AudioController.Instance.AdjustCreatureSFXVolume(_valueAdjustment);

        ChangeText("Creature SFX: " + Mathf.RoundToInt(AudioController.Instance.GetCreatureSFXVolume() * 100));
    }

    private void AdjustHandPositioning()
    {
        OptionsMenu.Instance.OpenHandAdjuster();
        _menu.gameObject.SetActive(false);
    }

    private void ChangeControllerType()
    {
        if (_checkStatusOnly)
            ChangeText("Controller Type:\n" + ControllerType.Instance.controllerFullName);
    }

    private void ResetHandAlignment()
    {
        Array.ForEach(LocalGameManager.Instance.player.GetPlayerComponents().GetBothHands(), resetHands => ControllerType.Instance.ResetHandToControllerDefault(resetHands));
    }

    private void SprintToggle()
    {
        VRPlayerController player = LocalGameManager.Instance.player;

        if (!_checkStatusOnly)
        {
            bool toggleSprint = player.toggleSprint ? false : true;
            player.toggleSprint = toggleSprint;
        }

        ChangeText(player.toggleSprint ? "Toggle Sprint:\nOn" : "Toggle Sprint:\nOff");
    }

    private void PlayerCalibration()
    {
        OptionsMenu.Instance.OpenPlayerCalibration();

        if (_menu != null && _menu.gameObject != null)
            _menu.gameObject.SetActive(false);

        else
            Destroy(gameObject);
    }

    private void ClearHands()
    {
        Array.ForEach(LocalGameManager.Instance.player.GetPlayerComponents().GetBothHands(), emptyHands => emptyHands.EmptyHand());
    }

    private void KeyboardTyping()
    {
        if (_menu != null)
        {
            string roomName = _stringName == "Erase" ? null : _menu.multiplayerRoomName + _stringName;

            ChangeText(roomName);
        }

        else
        {
            switch (_stringName)
            {
                case "Erase":
                    ChangeText(null);
                    break;

                case "Send":
                    ChatManager.Instance.ChatMessage("[You] " + _textBox.text);

                    if (MultiplayerManager.Instance.coop)
                        MultiplayerManager.Instance.GetCoopManager().SendChatMessage(_textBox.text);
                    break;

                default:
                    string newText = _textBox.text += _stringName;
                    ChangeText(newText);
                    break;
            }
        }
    }

    private void JoinCreateServer()
    {
        NetworkManager network = MultiplayerManager.Instance.GetNetworkManager();

        string newText;

        if (_checkStatusOnly)
            newText = !MultiplayerManager.Instance.coop ? "Join/Create\n" + "Room" : "Leave\n" + "Room";

        else
        {
            if (!MultiplayerManager.Instance.coop)
            {
                network.roomName = _menu.multiplayerRoomName;
                network.ConnectToServer();
                newText = "Leave\n" + "Room";
            }

            else
            {
                network.LeaveRoom();
                newText = "Join/Create\n" + "Room";
            }
        }

        ChangeText(newText);
    }

    private void ChatOptions()
    {
        ChatManager chat = ChatManager.Instance;

        switch (_stringName)
        {
            case "PreviousMessage":
                chat.DisplayPreviousMessage();
                break;

            case "NextMessage":
                chat.DisplayNextMessage();
                break;

            case "Keyboard":
                for (int i = 0; i < 2; i++)
                {
                    ChatWindow chatWindow;

                    if (LocalGameManager.Instance.player.GetPlayerComponents().GetHand(i).chatDisplay.chatSystem.TryGetComponent<ChatWindow>(out chatWindow))
                    {
                        if (chatWindow.spawnedKeyboard == null)
                            chatWindow.spawnedKeyboard = Instantiate(MasterManager.playerManager.chatKeyboard, chatWindow.keyboardSpawn);

                        else
                            Destroy(chatWindow.spawnedKeyboard);
                    }
                }
                break;

            case "Erase":
                chat.DeleteMessageHistory();
                break;
        }
    }

    private void ToggleTextChat()
    {
        ChatManager chat = ChatManager.Instance;

        if (!_checkStatusOnly)
        {
            bool textChat = chat.textChat ? false : true;
            chat.textChat = textChat;
        }

        ChangeText(chat.textChat ? "Text Chat:\nEnabled" : "Text Chat:\nDisabled");
    }

    private void ChangeChatHand()
    {
        ChatManager chat = ChatManager.Instance;

        if (!_checkStatusOnly)
        {
            bool chatHand = chat.chatOnRightHand ? false : true;
            chat.chatOnRightHand = chatHand;
        }

        ChangeText(chat.chatOnRightHand ? "Chat Hand:\nRight Hand" : "Chat Hand:\nLeft Hand");
    }

    private void ToggleDebugChat()
    {
        ChatManager chat = ChatManager.Instance;

        if (!_checkStatusOnly)
        {
            bool debugMessages = chat.allowDebugMessages ? false : true;
            chat.allowDebugMessages = debugMessages;
        }

        ChangeText(chat.allowDebugMessages ? "Debug Chat:\nEnabled" : "Debug Chat:\nDisabled");
    }

    private void ToggleVoiceChat()
    {
        ChatManager chat = ChatManager.Instance;

        if (!_checkStatusOnly)
        {
            bool voiceChat = chat.voiceChat ? false : true;
            chat.voiceChat = voiceChat;
        }
        
        ChangeText(chat.voiceChat ? "Voice Chat:\nEnabled" : "Voice Chat:\nDisabled");
    }

    private void ToggleNotifications()
    {
        ChatManager chat = ChatManager.Instance;

        if (!_checkStatusOnly)
        {
            bool notifications = chat.notifications ? false : true;
            chat.notifications = notifications;
        }

        ChangeText(_checkStatusOnly ? "Notifications:\nEnabled" : "Notifications:\nDisabled");
    }

    private void ToggleShadowType()
    {
        VisualSettings visuals = VisualSettings.Instance;

        if (!_checkStatusOnly)
        {
            switch (visuals.shadowSetting)
            {
                case LightShadows.Soft:
                    visuals.shadowSetting = LightShadows.Hard;
                    break;

                case LightShadows.Hard:
                    visuals.shadowSetting = LightShadows.None;
                    break;

                case LightShadows.None:
                    visuals.shadowSetting = LightShadows.Soft;
                    break;
            }

            visuals.ChangeLightSettings(VisualSettings.LightAdjustment.shadowType);
        }

        switch (visuals.shadowSetting)
        {
            case LightShadows.Soft:
                ChangeText("Soft Shadows");
                break;

            case LightShadows.Hard:
                ChangeText("Hard Shadows");
                break;

            case LightShadows.None:
                ChangeText("No Shadows");
                break;
        }
    }

    private void ToggleShadowQuality()
    {
        VisualSettings visuals = VisualSettings.Instance;

        if (_checkStatusOnly)
        {
            switch (visuals.shadowQuality)
            {
                case ShadowResolution.VeryHigh:
                    ChangeText("Shadow Quality:\nVery High");
                    break;

                case ShadowResolution.High:
                    ChangeText("Shadow Quality:\nHigh");
                    break;

                case ShadowResolution.Medium:
                    ChangeText("Shadow Quality:\nMedium");
                    break;

                case ShadowResolution.Low:
                    ChangeText("Shadow Quality:\nLow");
                    break;
            }
        }

        else
        {
            switch (visuals.shadowQuality)
            {
                case ShadowResolution.VeryHigh:
                    visuals.shadowQuality = ShadowResolution.High;
                    ChangeText("Shadow Quality:\nHigh");
                    break;

                case ShadowResolution.High:
                    visuals.shadowQuality = ShadowResolution.Medium;
                    ChangeText("Shadow Quality:\nMedium");
                    break;

                case ShadowResolution.Medium:
                    visuals.shadowQuality = ShadowResolution.Low;
                    ChangeText("Shadow Quality:\nLow");
                    break;

                case ShadowResolution.Low:
                    visuals.shadowQuality = ShadowResolution.VeryHigh;
                    ChangeText("Shadow Quality:\nVery High");
                    break;
            }
            
            visuals.ChangeLightSettings(VisualSettings.LightAdjustment.shadowResolution);
        }
    }

    private void AdjustLightingRange()
    {
        VisualSettings visuals = VisualSettings.Instance;

        if (!_checkStatusOnly)
        {
            visuals.lightRange += _valueAdjustment;

            if (visuals.lightRange > 200)
                visuals.lightRange = 200;

            else if (visuals.lightRange < 0)
                visuals.lightRange = 0;
        }

        int range = Mathf.RoundToInt(visuals.lightRange);

        ChangeText("Light Range:\n" + range);
    }

    private void AdjustLightingBrightness()
    {
        VisualSettings visuals = VisualSettings.Instance;

        if (!_checkStatusOnly)
        {
            visuals.brightness += _valueAdjustment;

            if (visuals.brightness > 2)
                visuals.brightness = 2;

            else if (visuals.brightness < 0)
                visuals.brightness = 0;
        }

        int brightnessPercentage = Mathf.RoundToInt(100 * visuals.brightness);

        ChangeText("Light Brightness:\n" + brightnessPercentage + "%");
    }

    private void AmbientOcclusionToggle()
    {
        PostProcessingController postProcessing = PostProcessingController.Instance;

        if (!_checkStatusOnly)
        {
            bool ambientOcclusion = postProcessing.ambientOcclusion ? false : true;
            postProcessing.ambientOcclusion = ambientOcclusion;

            postProcessing.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.ambientOcc);
        }

        ChangeText(postProcessing.ambientOcclusion ? "Ambient Occlusion On" : "Ambient Occlusion Off");
    }

    private void BloomToggle()
    {
        PostProcessingController postProcessing = PostProcessingController.Instance;

        if (!_checkStatusOnly)
        {
            bool bloom = postProcessing.bloom ? false : true;
            postProcessing.bloom = bloom;

            postProcessing.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.bloomEffect);
        }

        ChangeText(postProcessing.bloom ? "Bloom On" : "Bloom Off");
    }

    private void ColorGradingToggle()
    {
        PostProcessingController postProcessing = PostProcessingController.Instance;

        if (!_checkStatusOnly)
        {
            bool colorGrading = postProcessing.colorGrading ? false : true;
            postProcessing.colorGrading = colorGrading;

            postProcessing.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.color);
        }

        ChangeText(postProcessing.colorGrading ? "Color Grading On" : "Color Grading Off");
    }

    private void AmbientOcclusionIntensity()
    {
        PostProcessingController postProcessing = PostProcessingController.Instance;

        if (!_checkStatusOnly)
        {
            postProcessing.AOIntensity += _valueAdjustment;

            if (postProcessing.AOIntensity > 4)
                postProcessing.AOIntensity = 4;

            else if (postProcessing.AOIntensity < 0)
                postProcessing.AOIntensity = 0;

            postProcessing.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.ambientOcc);
        }

        ChangeText("Intensity:\n" + postProcessing.AOIntensity);
    }

    private void AmbientOcclusionThickness()
    {
        PostProcessingController postProcessing = PostProcessingController.Instance;

        if (!_checkStatusOnly)
        {
            postProcessing.thickness += _valueAdjustment;

            if (postProcessing.thickness > 10)
                postProcessing.thickness = 10;

            else if (postProcessing.thickness < 1)
                postProcessing.thickness = 1;

            postProcessing.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.ambientOcc);
        }

        ChangeText("Thickness:\n" + postProcessing.thickness);
    }

    private void BloomIntensity()
    {
        PostProcessingController postProcessing = PostProcessingController.Instance;

        if (!_checkStatusOnly)
        {
            postProcessing.Bintensity += _valueAdjustment;

            postProcessing.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.bloomEffect);
        }

        ChangeText("Intensity:\n" + Mathf.RoundToInt(postProcessing.Bintensity));
    }

    private void BloomThreshold()
    {
        PostProcessingController postProcessing = PostProcessingController.Instance;

        if (!_checkStatusOnly)
        {
            postProcessing.threshold += _valueAdjustment;

            postProcessing.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.bloomEffect);
        }

        ChangeText("Threshold:\n" + postProcessing.threshold);
    }

    private void BloomDiffusion()
    {
        PostProcessingController postProcessing = PostProcessingController.Instance;

        if (!_checkStatusOnly)
        {
            postProcessing.diffusion += _valueAdjustment;

            if (postProcessing.diffusion > 10)
                postProcessing.diffusion = 10;

            else if (postProcessing.diffusion < 1)
                postProcessing.diffusion = 1;

            postProcessing.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.bloomEffect);
        }

        ChangeText("Diffusion:\n" + postProcessing.diffusion);
    }

    private void ColorGradingTonemapper()
    {
        PostProcessingController postProcessing = PostProcessingController.Instance;

        if (!_checkStatusOnly)
        {
            switch (postProcessing.tonemapping)
            {
                case UnityEngine.Rendering.PostProcessing.Tonemapper.None:
                    postProcessing.tonemapping = UnityEngine.Rendering.PostProcessing.Tonemapper.Neutral;
                    break;

                case UnityEngine.Rendering.PostProcessing.Tonemapper.Neutral:
                    postProcessing.tonemapping = UnityEngine.Rendering.PostProcessing.Tonemapper.ACES;
                    break;

                case UnityEngine.Rendering.PostProcessing.Tonemapper.ACES:
                    postProcessing.tonemapping = UnityEngine.Rendering.PostProcessing.Tonemapper.None;
                    break;
            }

            postProcessing.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.color);
        }

        switch (postProcessing.tonemapping)
        {
            case UnityEngine.Rendering.PostProcessing.Tonemapper.None:
                ChangeText("Tonemapper:\nNone");
                break;

            case UnityEngine.Rendering.PostProcessing.Tonemapper.Neutral:
                ChangeText("Tonemapper:\nNeutral");
                break;

            case UnityEngine.Rendering.PostProcessing.Tonemapper.ACES:
                ChangeText("Tonemapper:\nACES");
                break;
        }
    }

    private void ColorGradingTemperature()
    {
        PostProcessingController postProcessing = PostProcessingController.Instance;

        if (!_checkStatusOnly)
        {
            postProcessing.temperature += _valueAdjustment;

            if (postProcessing.temperature > 100)
                postProcessing.temperature = 100;

            else if (postProcessing.temperature < -100)
                postProcessing.temperature = -100;

            postProcessing.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.color);
        }

        ChangeText("Temperature:\n" + Mathf.RoundToInt(postProcessing.temperature));
    }

    private void ColorGradingTint()
    {
        PostProcessingController postProcessing = PostProcessingController.Instance;

        if (!_checkStatusOnly)
        {
            postProcessing.tint += _valueAdjustment;

            if (postProcessing.tint > 100)
                postProcessing.tint = 100;

            else if (postProcessing.tint < -100)
                postProcessing.tint = -100;

            postProcessing.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.color);
        }

        ChangeText("Tint:\n" + Mathf.RoundToInt(postProcessing.tint));
    }

    private void ColorGradingPostExposure()
    {
        PostProcessingController postProcessing = PostProcessingController.Instance;

        if (!_checkStatusOnly)
        {
            postProcessing.postExposure += _valueAdjustment;

            postProcessing.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.color);
        }

        ChangeText("Post Exposure:\n" + postProcessing.postExposure);
    }

    private void ColorGradingHueShift()
    {
        PostProcessingController postProcessing = PostProcessingController.Instance;

        if (!_checkStatusOnly)
        {
            postProcessing.hueShift += _valueAdjustment;

            if (postProcessing.hueShift > 180)
                postProcessing.hueShift = 180;

            else if (postProcessing.hueShift < 180)
                postProcessing.hueShift = -180;

            postProcessing.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.color);
        }

        ChangeText("Hue Shift:\n" + Mathf.RoundToInt(postProcessing.hueShift));
    }

    private void ColorGradingSaturation()
    {
        PostProcessingController postProcessing = PostProcessingController.Instance;

        if (!_checkStatusOnly)
        {
            postProcessing.saturation += _valueAdjustment;

            if (postProcessing.saturation > 100)
                postProcessing.saturation = 100;

            else if (postProcessing.saturation < -100)
                postProcessing.saturation = -100;

            postProcessing.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.color);
        }

        ChangeText("Saturation:\n" + Mathf.RoundToInt(postProcessing.saturation));
    }

    private void ColorGradingContrast()
    {
        PostProcessingController postProcessing = PostProcessingController.Instance;

        if (!_checkStatusOnly)
        {
            postProcessing.contrast += _valueAdjustment;

            if (postProcessing.contrast > 100)
                postProcessing.contrast = 100;

            else if (postProcessing.contrast < -100)
                postProcessing.contrast = -100;

            postProcessing.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.color);
        }

        ChangeText("Contrast:\n" + Mathf.RoundToInt(postProcessing.contrast));
    }

    private void BackAttachmentAdjustment()
    {
        PlayerComponents playerComponents = LocalGameManager.Instance.player.GetPlayerComponents();

        if (!_checkStatusOnly)
        {
            Vector3 backAttachments = playerComponents.backAttachments.transform.localPosition;
            playerComponents.belt.backAttachments = backAttachments.z + _valueAdjustment;

            if (playerComponents.belt.backAttachments > 1)
                playerComponents.belt.backAttachments = 1;

            else if (playerComponents.belt.backAttachments < -1)
                playerComponents.belt.backAttachments = -1;

            playerComponents.backAttachments.transform.localPosition = new Vector3(0, 0, playerComponents.belt.backAttachments);
        }

        ChangeText("Back Position:\n" + playerComponents.backAttachments.transform.localPosition.z);
    }

    private void BeltAdjustment()
    {
        PlayerBelt belt = LocalGameManager.Instance.player.GetPlayerComponents().belt;

        if (!_checkStatusOnly)
        {
            if (LocalGameManager.Instance.player.playerStanding)
            {
                belt.heightStandingPlayer += _valueAdjustment;

                if (belt.heightStandingPlayer > 1)
                    belt.heightStandingPlayer = 1;

                else if (belt.heightStandingPlayer < 0)
                    belt.heightStandingPlayer = 0;
            }

            else
            {
                if (!_checkStatusOnly)
                {
                    belt.heightSittingPlayer += _valueAdjustment;

                    if (belt.heightSittingPlayer > 1)
                        belt.heightSittingPlayer = 1;

                    else if (belt.heightSittingPlayer < 0)
                        belt.heightSittingPlayer = 0;
                }
            }
        }

        ChangeText(LocalGameManager.Instance.player.playerStanding ? "Belt Adjustment:\n" + belt.heightStandingPlayer : "Belt Adjustment:\n" + belt.heightSittingPlayer);
    }

    private void BeltOffset()
    {
        PlayerBelt belt = LocalGameManager.Instance.player.GetPlayerComponents().belt;

        if (!_checkStatusOnly)
        {
            belt.zAdjustmentForSittingPlayer += _valueAdjustment;

            if (belt.zAdjustmentForSittingPlayer > 1)
                belt.zAdjustmentForSittingPlayer = 1;

            else if (belt.zAdjustmentForSittingPlayer < 0)
                belt.zAdjustmentForSittingPlayer = 0;
        }

        ChangeText("Crouched Belt Offset:\n" + belt.zAdjustmentForSittingPlayer);
    }

    private void OnScreenTextPosition()
    {
        Vector3 currentPos = LocalGameManager.Instance.player.GetPlayerComponents().onScreenText.transform.localPosition;

        if(_stringName == "x")
            currentPos.x += _valueAdjustment;

        else
            currentPos.y += _valueAdjustment;

        LocalGameManager.Instance.player.GetPlayerComponents().onScreenText.transform.localPosition = new Vector3(currentPos.x, currentPos.y, currentPos.z);
    }

    private void CheatOptions()
    {
        if (_textBox.text == "EREBUSGOD")
            LocalGameManager.Instance.ActivateDevMode();
    }

    private void PhysicalJumpingToggle()
    {
        VRPlayerController player = LocalGameManager.Instance.player;

        if (!_checkStatusOnly)
        {
            bool physicalJumping = player.physicalJumping ? false : true;
            player.physicalJumping = physicalJumping;
        }

        ChangeText(player.physicalJumping ? "Physical Jump:\nOn" : "Physical Jump:\nOff");
    }

    private void OpenDiscordLink()
    {
        Application.OpenURL("https://discord.gg/zycWTnYqCY");
    }
}
