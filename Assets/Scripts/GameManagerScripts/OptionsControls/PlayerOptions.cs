using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerOptions : MonoBehaviour
{
    public enum Options { playmode, primaryHand, clockFormat, playerOrientation, playerRotation, roomScale, gripType, leftJoystickSensitivty,
        rightJoystickSensitivty, smoothTurning, snapTurning, musicVolume, adjustHandPositioning, changeControllerType, sfxVolume, sprintToggle,
        playerCalibration, clearHands, openDiscordLink, creatureSFX, resetHandAlignment, keyboard, joinCreateServer, chat, textChat, debugChat,
        voiceChat, notifications, shadowType, shadowQuality, lightRange, lightBrightness, ambientOcclusionToggle, bloomToggle, colorGradingToggle,
        ambientOcclusionIntensity, ambientOcclusionThickness, bloomIntensity, bloomThreshold, bloomDiffusion, colorGradingTonemapper,
        colorGradingTemperature, colorGradingTint, colorGradingPostExposure, colorGradingHueShift, colorGradingSaturation, colorGradingContrast,
        backAttachments, beltAdjustment, sittingBeltOffset, onScreenText, chatHand, cheatOptions, physicalJumpingToggle
    }

    public Options playerOptions;

    [SerializeField] private PlayerMenu _menu;
    [SerializeField] private Text _textBox;
    [SerializeField] private bool _checkStatusOnly;
    [SerializeField] private float _valueAdjustment;
    [SerializeField] private string _stringName;

    private LocalGameManager _gameManager;
    private OptionsMenu _optionsMenu;
    private AudioController _audioController;
    private VisualSettings _visualSettings;
    private PostProcessingController _postProcessingController;

    private NetworkManager _networkManager;
    private ChatManager _chatManager;

    private VRPlayerController _player;
    private PlayerComponents _playerComponents;
    private MagicController _magicController;
    
    private int _controllerIndx;

    private void Start()
    {
        _gameManager = LocalGameManager.instance;
        _optionsMenu = _gameManager.GetOptionsMenu();
        _audioController = _gameManager.GetAudioController();
        _visualSettings = _gameManager.GetVisualSettings();
        _postProcessingController = _gameManager.GetPostProcessingController();

        _networkManager = _gameManager.GetNetworkManager();
        _chatManager = _gameManager.GetChatManager();

        _player = _gameManager.player;
        _playerComponents = _player.GetPlayerComponents();
        _magicController = _gameManager.GetMagicController();
        
        if (_checkStatusOnly) { ChangePlayerOptions(); }
    }

    public void ChangePlayerOptions()
    {
        _gameManager.GetControllerType().controllerID = _controllerIndx;

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
        if (!_checkStatusOnly)
        {
            _player.heightCheck = true;
            if (_player.playerStanding)
            {
                _player.playerStanding = false;
                _player.isCrouched = false;
                _player.SittingHeightController();
            }
            else _player.playerStanding = true;
        }

        ChangeText(_player.playerStanding ? "Playmode:\nStanding" : "Playmode:\nSitting");
    }

    private void ChangePrimaryHand()
    {
        if (!_checkStatusOnly)
        {
            bool leftHanded = _player.isLeftHanded ? false : true;
            _player.isLeftHanded = leftHanded;

            Array.ForEach(_playerComponents.GetBothHands(), setPrimaryHand => setPrimaryHand.SetPrimaryHand());

            switch (_magicController.GetClassType())
            {
                case MagicController.ClassType.Wizard:
                    Wizard.instance.GetStaffController().ResetStaff();
                    break;

                case MagicController.ClassType.Conjurer:
                    Conjurer.instance.GetBowController().ResetBow();
                    break;
            }
        }

        ChangeText(_player.isLeftHanded ? "Left Handed" : "Right Handed");
    }

    private void ChangeTimeDisplay()
    {
        if (!_checkStatusOnly)
        {
            bool timeFormat = _player.militaryTime ? false : true;
            _player.militaryTime = timeFormat;
        }

        ChangeText(_player.militaryTime ? "Time Format:\n24hr" : "Time Format:\n12hr");
    }

    private void ChangePlayerOrientation()
    {
        if (!_checkStatusOnly)
        {
            bool playerOrientation = _player.headOrientation ? false : true;
            _player.headOrientation = playerOrientation;

            _player.OrientationSource();
        }

        ChangeText(_player.headOrientation ? "Orientation:\nHead" : "Orientation:\nHand");
    }

    private void ChangePlayerRotation()
    {
        if (!_checkStatusOnly)
        {
            bool snapRotation = _player.snapTurnOn ? false : true;
            _player.snapTurnOn = snapRotation;
        }

        ChangeText(_player.snapTurnOn ? "Snap Turning" : "Smooth Turning");
    }

    private void ToggleRoomScale()
    {
        if (!_checkStatusOnly)
        {
            bool roomScale = _player.roomScale ? false : true;
            _player.roomScale = roomScale;
        }

        ChangeText(_player.roomScale ? "Roomscale:\nOn" : "Roomscale:\nOff");
    }

    private void ChangeGrip()
    {
        if (!_checkStatusOnly)
        {
            bool toggleGrip = _player.toggleGrip ? false : true;
            _player.toggleGrip = toggleGrip;
        }

        ChangeText(_player.toggleGrip ? "Toggle Grip" : "Hold Grip");
    }

    private void LeftControllerSensitivty()
    {
        if (!_checkStatusOnly)
        {
            _player.leftJoystickDeadzoneAdjustment += _valueAdjustment;

            if (_player.leftJoystickDeadzoneAdjustment >= .9f) { _player.leftJoystickDeadzoneAdjustment = .9f; }
            else if (_player.leftJoystickDeadzoneAdjustment <= 0) { _player.leftJoystickDeadzoneAdjustment = 0; }
        }

        int deadzoneValue = Mathf.RoundToInt(_player.leftJoystickDeadzoneAdjustment * 100);
        ChangeText("Left Sensitivity:\n" + deadzoneValue + "%");
    }

    private void RightControllerSensitivty()
    {
        if (!_checkStatusOnly)
        {
            _player.rightJoystickDeadzoneAdjustment += _valueAdjustment;

            if (_player.rightJoystickDeadzoneAdjustment >= .9f) { _player.rightJoystickDeadzoneAdjustment = .9f; }
            else if (_player.rightJoystickDeadzoneAdjustment <= 0) { _player.rightJoystickDeadzoneAdjustment = 0; }
        }

        int deadzoneValue = Mathf.RoundToInt(_player.rightJoystickDeadzoneAdjustment * 100);
        ChangeText("Right Sensitivity:\n" + deadzoneValue + "%");
    }

    private void SmoothTurningAdjustment()
    {
        if (!_checkStatusOnly)
        {
            _player.turnSpeedAdjustment += _valueAdjustment;

            if (_player.turnSpeedAdjustment >= 4f) { _player.turnSpeedAdjustment = 4f; }
            else if (_player.turnSpeedAdjustment <= .1f) { _player.turnSpeedAdjustment = .1f; }
        }

        int turnSpeed = Mathf.RoundToInt(_player.turnSpeedAdjustment * 100);
        ChangeText("Smooth Turning:\n" + turnSpeed + "%");
    }

    private void SnapTurningAdjustment()
    {
        if (!_checkStatusOnly)
        {
            _player.snapTurnRotationAdjustment += _valueAdjustment;

            if (_player.snapTurnRotationAdjustment >= 90f) { _player.snapTurnRotationAdjustment = 90f; }
            else if (_player.snapTurnRotationAdjustment <= 10f) { _player.snapTurnRotationAdjustment = 10f; }
        }
        
        ChangeText("Snap Turning:\n" + Mathf.RoundToInt(_player.snapTurnRotationAdjustment));
    }

    private void ChangeMusicVolume()
    {
        if (!_checkStatusOnly) { _audioController.AdjustMusicVolume(_valueAdjustment); }

        ChangeText("Music: " + Mathf.RoundToInt(_audioController.GetMusicVolume() * 100));
    }

    private void ChangeSFXVolume()
    {
        if (!_checkStatusOnly) { _audioController.AdjustSFXVolume(_valueAdjustment); }

        ChangeText("SFX: " + Mathf.RoundToInt(_audioController.GetSFXVolume() * 100));
    }

    private void ChangeCreatureSFXVolume()
    {
        if (!_checkStatusOnly) { _audioController.AdjustCreatureSFXVolume(_valueAdjustment); }

        ChangeText("Creature SFX: " + Mathf.RoundToInt(_audioController.GetCreatureSFXVolume() * 100));
    }

    private void AdjustHandPositioning()
    {
        _optionsMenu.OpenHandAdjuster();
        _menu.gameObject.SetActive(false);
    }

    private void ChangeControllerType()
    {
        if (_checkStatusOnly)
        {
            ChangeText("Controller Type:\n" + _gameManager.GetControllerType().controllerFullName);
        }
    }

    private void ResetHandAlignment()
    {
        Array.ForEach(_playerComponents.GetBothHands(), resetHands => _gameManager.GetControllerType().ResetHandToControllerDefault(resetHands));
    }

    private void SprintToggle()
    {
        if (!_checkStatusOnly)
        {
            bool toggleSprint = _player.toggleSprint ? false : true;
            _player.toggleSprint = toggleSprint;
        }

        ChangeText(_player.toggleSprint ? "Toggle Sprint:\nOn" : "Toggle Sprint:\nOff");
    }

    private void PlayerCalibration()
    {
        _optionsMenu.OpenPlayerCalibration();

        if (_menu != null && _menu.gameObject != null) { _menu.gameObject.SetActive(false); }
        else { Destroy(gameObject); }
    }

    private void ClearHands()
    {
        Array.ForEach(_playerComponents.GetBothHands(), emptyHands => emptyHands.EmptyHand());
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
                    _chatManager.ChatMessage("[You] " + _textBox.text);

                    if (CoopManager.instance != null) { CoopManager.instance.SendChatMessage(_textBox.text); }
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
        if (_checkStatusOnly)
        {
            string newText = CoopManager.instance == null ? "Join/Create\n" + "Room" : "Leave\n" + "Room";
        }

        else
        {
            if (CoopManager.instance == null)
            {
                _networkManager.roomName = _menu.multiplayerRoomName;
                _networkManager.ConnectToServer();
                _textBox.text = "Leave\n" + "Room";
            }

            else
            {
                _networkManager.LeaveRoom();
                _textBox.text = "Join/Create\n" + "Room";
            }
        }
    }

    private void ChatOptions()
    {
        switch (_stringName)
        {
            case "PreviousMessage":
                _chatManager.DisplayPreviousMessage();
                break;

            case "NextMessage":
                _chatManager.DisplayNextMessage();
                break;

            case "Keyboard":
                for (int i = 0; i < 2; i++)
                {
                    ChatWindow chatWindow;
                    if (_playerComponents.GetHand(i).chatDisplay.chatSystem.TryGetComponent<ChatWindow>(out chatWindow))
                    {
                        if (chatWindow.spawnedKeyboard == null)
                        {
                            chatWindow.spawnedKeyboard = Instantiate(MasterManager.playerManager.chatKeyboard, chatWindow.keyboardSpawn);
                        }
                        else { Destroy(chatWindow.spawnedKeyboard); }
                    }
                }
                break;

            case "Erase":
                _chatManager.DeleteMessageHistory();
                break;
        }
    }

    private void ToggleTextChat()
    {
        if (!_checkStatusOnly)
        {
            bool textChat = _chatManager.textChat ? false : true;
            _chatManager.textChat = textChat;
        }

        ChangeText(_chatManager.textChat ? "Text Chat:\nEnabled" : "Text Chat:\nDisabled");
    }

    private void ChangeChatHand()
    {
        if (!_checkStatusOnly)
        {
            bool chatHand = _chatManager.chatOnRightHand ? false : true;
            _chatManager.chatOnRightHand = chatHand;
        }

        ChangeText(_chatManager.chatOnRightHand ? "Chat Hand:\nRight Hand" : "Chat Hand:\nLeft Hand");
    }

    private void ToggleDebugChat()
    {
        if (!_checkStatusOnly)
        {
            bool debugMessages = _chatManager.allowDebugMessages ? false : true;
            _chatManager.allowDebugMessages = debugMessages;
        }

        ChangeText(_chatManager.allowDebugMessages ? "Debug Chat:\nEnabled" : "Debug Chat:\nDisabled");
    }

    private void ToggleVoiceChat()
    {
        if (!_checkStatusOnly)
        {
            bool voiceChat = _chatManager.voiceChat ? false : true;
            _chatManager.voiceChat = voiceChat;
        }
        
        ChangeText(_chatManager.voiceChat ? "Voice Chat:\nEnabled" : "Voice Chat:\nDisabled");
    }

    private void ToggleNotifications()
    {
        if (!_checkStatusOnly)
        {
            bool notifications = _chatManager.notifications ? false : true;
            _chatManager.notifications = notifications;
        }

        ChangeText(_checkStatusOnly ? "Notifications:\nEnabled" : "Notifications:\nDisabled");
    }

    private void ToggleShadowType()
    {
        if (!_checkStatusOnly)
        {
            switch (_visualSettings.shadowSetting)
            {
                case LightShadows.Soft:
                    _visualSettings.shadowSetting = LightShadows.Hard;
                    break;

                case LightShadows.Hard:
                    _visualSettings.shadowSetting = LightShadows.None;
                    break;

                case LightShadows.None:
                    _visualSettings.shadowSetting = LightShadows.Soft;
                    break;
            }

            _visualSettings.ChangeLightSettings(VisualSettings.LightAdjustment.shadowType);
        }

        switch (_visualSettings.shadowSetting)
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
        if (_checkStatusOnly)
        {
            switch (_visualSettings.shadowQuality)
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
            switch (_visualSettings.shadowQuality)
            {
                case ShadowResolution.VeryHigh:
                    _visualSettings.shadowQuality = ShadowResolution.High;
                    ChangeText("Shadow Quality:\nHigh");
                    break;

                case ShadowResolution.High:
                    _visualSettings.shadowQuality = ShadowResolution.Medium;
                    ChangeText("Shadow Quality:\nMedium");
                    break;

                case ShadowResolution.Medium:
                    _visualSettings.shadowQuality = ShadowResolution.Low;
                    ChangeText("Shadow Quality:\nLow");
                    break;

                case ShadowResolution.Low:
                    _visualSettings.shadowQuality = ShadowResolution.VeryHigh;
                    ChangeText("Shadow Quality:\nVery High");
                    break;
            }
            
            _visualSettings.ChangeLightSettings(VisualSettings.LightAdjustment.shadowResolution);
        }
    }

    private void AdjustLightingRange()
    {
        if (!_checkStatusOnly)
        {
            _visualSettings.lightRange += _valueAdjustment;

            if (_visualSettings.lightRange > 200) { _visualSettings.lightRange = 200; }
            else if (_visualSettings.lightRange < 0) { _visualSettings.lightRange = 0; }
        }

        int range = Mathf.RoundToInt(_visualSettings.lightRange);

        ChangeText("Light Range:\n" + range);
    }

    private void AdjustLightingBrightness()
    {
        if (!_checkStatusOnly)
        {
            _visualSettings.brightness += _valueAdjustment;

            if (_visualSettings.brightness > 2) { _visualSettings.brightness = 2; }
            else if (_visualSettings.brightness < 0) { _visualSettings.brightness = 0; }
        }

        int brightnessPercentage = Mathf.RoundToInt(100 * _visualSettings.brightness);

        ChangeText("Light Brightness:\n" + brightnessPercentage + "%");
    }

    private void AmbientOcclusionToggle()
    {
        if (!_checkStatusOnly)
        {
            bool ambientOcclusion = _postProcessingController.ambientOcclusion ? false : true;
            _postProcessingController.ambientOcclusion = ambientOcclusion;

            _postProcessingController.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.ambientOcc);
        }

        ChangeText(_postProcessingController.ambientOcclusion ? "Ambient Occlusion On" : "Ambient Occlusion Off");
    }

    private void BloomToggle()
    {
        if (!_checkStatusOnly)
        {
            bool bloom = _postProcessingController.bloom ? false : true;
            _postProcessingController.bloom = bloom;

            _postProcessingController.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.bloomEffect);
        }

        ChangeText(_postProcessingController.bloom ? "Bloom On" : "Bloom Off");
    }

    private void ColorGradingToggle()
    {
        if (!_checkStatusOnly)
        {
            bool colorGrading = _postProcessingController.colorGrading ? false : true;
            _postProcessingController.colorGrading = colorGrading;

            _postProcessingController.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.color);
        }

        ChangeText(_postProcessingController.colorGrading ? "Color Grading On" : "Color Grading Off");
    }

    private void AmbientOcclusionIntensity()
    {
        if (!_checkStatusOnly)
        {
            _postProcessingController.AOIntensity += _valueAdjustment;

            if (_postProcessingController.AOIntensity > 4) { _postProcessingController.AOIntensity = 4; }
            else if (_postProcessingController.AOIntensity < 0) { _postProcessingController.AOIntensity = 0; }

            _postProcessingController.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.ambientOcc);
        }

        ChangeText("Intensity:\n" + _postProcessingController.AOIntensity);
    }

    private void AmbientOcclusionThickness()
    {
        if (!_checkStatusOnly)
        {
            _postProcessingController.thickness += _valueAdjustment;

            if (_postProcessingController.thickness > 10) { _postProcessingController.thickness = 10; }
            else if (_postProcessingController.thickness < 1) { _postProcessingController.thickness = 1; }

            _postProcessingController.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.ambientOcc);
        }

        ChangeText("Thickness:\n" + _postProcessingController.thickness);
    }

    private void BloomIntensity()
    {
        if (!_checkStatusOnly)
        {
            _postProcessingController.Bintensity += _valueAdjustment;

            _postProcessingController.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.bloomEffect);
        }

        ChangeText("Intensity:\n" + Mathf.RoundToInt(_postProcessingController.Bintensity));
    }

    private void BloomThreshold()
    {
        if (!_checkStatusOnly)
        {
            _postProcessingController.threshold += _valueAdjustment;

            _postProcessingController.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.bloomEffect);
        }

        ChangeText("Threshold:\n" + _postProcessingController.threshold);
    }

    private void BloomDiffusion()
    {
        if (!_checkStatusOnly)
        {
            _postProcessingController.diffusion += _valueAdjustment;

            if (_postProcessingController.diffusion > 10) { _postProcessingController.diffusion = 10; }
            else if (_postProcessingController.diffusion < 1) { _postProcessingController.diffusion = 1; }

            _postProcessingController.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.bloomEffect);
        }

        ChangeText("Diffusion:\n" + _postProcessingController.diffusion);
    }

    private void ColorGradingTonemapper()
    {
        if (!_checkStatusOnly)
        {
            switch (_postProcessingController.tonemapping)
            {
                case UnityEngine.Rendering.PostProcessing.Tonemapper.None:
                    _postProcessingController.tonemapping = UnityEngine.Rendering.PostProcessing.Tonemapper.Neutral;
                    break;

                case UnityEngine.Rendering.PostProcessing.Tonemapper.Neutral:
                    _postProcessingController.tonemapping = UnityEngine.Rendering.PostProcessing.Tonemapper.ACES;
                    break;

                case UnityEngine.Rendering.PostProcessing.Tonemapper.ACES:
                    _postProcessingController.tonemapping = UnityEngine.Rendering.PostProcessing.Tonemapper.None;
                    break;
            }

            _postProcessingController.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.color);
        }

        switch (_postProcessingController.tonemapping)
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
        if (!_checkStatusOnly)
        {
            _postProcessingController.temperature += _valueAdjustment;

            if (_postProcessingController.temperature > 100) { _postProcessingController.temperature = 100; }
            else if (_postProcessingController.temperature < -100) { _postProcessingController.temperature = -100; }

            _postProcessingController.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.color);
        }

        ChangeText("Temperature:\n" + Mathf.RoundToInt(_postProcessingController.temperature));
    }

    private void ColorGradingTint()
    {
        if (!_checkStatusOnly)
        {
            _postProcessingController.tint += _valueAdjustment;

            if (_postProcessingController.tint > 100) { _postProcessingController.tint = 100; }
            else if (_postProcessingController.tint < -100) { _postProcessingController.tint = -100; }

            _postProcessingController.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.color);
        }

        ChangeText("Tint:\n" + Mathf.RoundToInt(_postProcessingController.tint));
    }

    private void ColorGradingPostExposure()
    {
        if (!_checkStatusOnly)
        {
            _postProcessingController.postExposure += _valueAdjustment;

            _postProcessingController.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.color);
        }

        ChangeText("Post Exposure:\n" + _postProcessingController.postExposure);
    }

    private void ColorGradingHueShift()
    {
        if (!_checkStatusOnly)
        {
            _postProcessingController.hueShift += _valueAdjustment;

            if (_postProcessingController.hueShift > 180) { _postProcessingController.hueShift = 180; }
            else if (_postProcessingController.hueShift < 180) { _postProcessingController.hueShift = -180; }

            _postProcessingController.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.color);
        }

        ChangeText("Hue Shift:\n" + Mathf.RoundToInt(_postProcessingController.hueShift));
    }

    private void ColorGradingSaturation()
    {
        if (!_checkStatusOnly)
        {
            _postProcessingController.saturation += _valueAdjustment;

            if (_postProcessingController.saturation > 100) { _postProcessingController.saturation = 100; }
            else if (_postProcessingController.saturation < -100) { _postProcessingController.saturation = -100; }

            _postProcessingController.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.color);
        }

        ChangeText("Saturation:\n" + Mathf.RoundToInt(_postProcessingController.saturation));
    }

    private void ColorGradingContrast()
    {
        if (!_checkStatusOnly)
        {
            _postProcessingController.contrast += _valueAdjustment;

            if (_postProcessingController.contrast > 100) { _postProcessingController.contrast = 100; }
            else if (_postProcessingController.contrast < -100) { _postProcessingController.contrast = -100; }

            _postProcessingController.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.color);
        }

        ChangeText("Contrast:\n" + Mathf.RoundToInt(_postProcessingController.contrast));
    }

    private void BackAttachmentAdjustment()
    {
        if (!_checkStatusOnly)
        {
            Vector3 backAttachments = _playerComponents.backAttachments.transform.localPosition;
            _playerComponents.belt.backAttachments = backAttachments.z + _valueAdjustment;

            if (_playerComponents.belt.backAttachments > 1) { _playerComponents.belt.backAttachments = 1; }
            else if (_playerComponents.belt.backAttachments < -1) { _playerComponents.belt.backAttachments = -1; }

            _playerComponents.backAttachments.transform.localPosition = new Vector3(0, 0, _playerComponents.belt.backAttachments);
        }

        ChangeText("Back Position:\n" + _playerComponents.backAttachments.transform.localPosition.z);
    }

    private void BeltAdjustment()
    {
        PlayerBelt belt = _playerComponents.belt;

        if (!_checkStatusOnly)
        {
            if (_player.playerStanding)
            {
                belt.heightStandingPlayer += _valueAdjustment;

                if (belt.heightStandingPlayer > 1) { belt.heightStandingPlayer = 1; }
                else if (belt.heightStandingPlayer < 0) { belt.heightStandingPlayer = 0; }
            }

            else
            {
                if (!_checkStatusOnly)
                {
                    belt.heightSittingPlayer += _valueAdjustment;

                    if (belt.heightSittingPlayer > 1) { belt.heightSittingPlayer = 1; }
                    else if (belt.heightSittingPlayer < 0) { belt.heightSittingPlayer = 0; }
                }
            }
        }

        ChangeText(_player.playerStanding ? "Belt Adjustment:\n" + belt.heightStandingPlayer : "Belt Adjustment:\n" + belt.heightSittingPlayer);
    }

    private void BeltOffset()
    {
        PlayerBelt belt = _playerComponents.belt;

        if (!_checkStatusOnly)
        {
            belt.zAdjustmentForSittingPlayer += _valueAdjustment;

            if (belt.zAdjustmentForSittingPlayer > 1) { belt.zAdjustmentForSittingPlayer = 1; }
            else if (belt.zAdjustmentForSittingPlayer < 0) { belt.zAdjustmentForSittingPlayer = 0; }
        }

        ChangeText("Crouched Belt Offset:\n" + belt.zAdjustmentForSittingPlayer);
    }

    private void OnScreenTextPosition()
    {
        Vector3 currentPos = _playerComponents.onScreenText.transform.localPosition;

        if(_stringName == "x") { currentPos.x += _valueAdjustment; }
        else { currentPos.y += _valueAdjustment; }

        _playerComponents.onScreenText.transform.localPosition = new Vector3(currentPos.x, currentPos.y, currentPos.z);
    }

    private void CheatOptions()
    {
        if (_textBox.text == "EREBUSGOD") { LocalGameManager.instance.ActivateDevMode(); }
    }

    private void PhysicalJumpingToggle()
    {
        if (!_checkStatusOnly)
        {
            bool physicalJumping = _player.physicalJumping ? false : true;
            _player.physicalJumping = physicalJumping;
        }

        ChangeText(_player.physicalJumping ? "Physical Jump:\nOn" : "Physical Jump:\nOff");
    }

    private void OpenDiscordLink()
    {
        Application.OpenURL("https://discord.gg/zycWTnYqCY");
    }
}
