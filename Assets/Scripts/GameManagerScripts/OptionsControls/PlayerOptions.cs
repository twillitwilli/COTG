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
            case Options.playmode:
                ChangePlayMode();
                break;

            case Options.primaryHand:
                ChangePrimaryHand();
                break;

            case Options.clockFormat:
                ChangeTimeDisplay();
                break;

            case Options.playerOrientation:
                ChangePlayerOrientation();
                break;

            case Options.playerRotation:
                ChangePlayerRotation();
                break;

            case Options.roomScale:
                ToggleRoomScale();
                break;

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

            case Options.musicVolume:
                ChangeMusicVolume();
                break;

            case Options.sfxVolume:
                ChangeSFXVolume();
                break;

            case Options.adjustHandPositioning:
                AdjustHandPositioning();
                break;

            case Options.changeControllerType:
                ChangeControllerType();
                break;

            case Options.sprintToggle:
                SprintToggle();
                break;

            case Options.playerCalibration:
                PlayerCalibration();
                break;

            case Options.clearHands:
                ClearHands();
                break;

            case Options.openDiscordLink:
                OpenDiscordLink();
                break;

            case Options.creatureSFX:
                ChangeCreatureSFXVolume();
                break;

            case Options.resetHandAlignment:
                ResetHandAlignment();
                break;

            case Options.keyboard:
                KeyboardTyping();
                break;

            case Options.joinCreateServer:
                JoinCreateServer();
                break;

            case Options.chat:
                ChatOptions();
                break;

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

            case Options.backAttachments:
                BackAttachmentAdjustment();
                break;

            case Options.beltAdjustment:
                BeltAdjustment();
                break;

            case Options.sittingBeltOffset:
                BeltOffset();
                break;

            case Options.onScreenText:
                OnScreenTextPosition();
                break;

            case Options.chatHand:
                ChangeChatHand();
                break;

            case Options.cheatOptions:
                CheatOptions();
                break;

            case Options.physicalJumpingToggle:
                PhysicalJumpingToggle();
                break;
        }
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
        if (_player.playerStanding) { _textBox.text = "Playmode:\nStanding"; }
        else _textBox.text = "Playmode:\nSitting";
    }

    private void ChangePrimaryHand()
    {
        if (!_checkStatusOnly)
        {
            if (_player.isLeftHanded) { _player.isLeftHanded = false; }
            else { _player.isLeftHanded = true; }

            _playerComponents.GetHand(0).SetPrimaryHand();
            _playerComponents.GetHand(1).SetPrimaryHand();

            switch (_magicController.GetClassType())
            {
                case MagicController.ClassType.Wizard:
                    if (Wizard.instance != null)
                    {
                        Wizard.instance.GetStaffController().ResetStaff();
                    }
                    break;

                case MagicController.ClassType.Conjurer:
                    if (Conjurer.instance != null)
                    {
                        Conjurer.instance.GetBowController().ResetBow();
                    }
                    break;
            }
        }

        if (!_player.isLeftHanded) { _textBox.text = "Right Handed"; }
        else _textBox.text = "Left Handed";
    }

    private void ChangeTimeDisplay()
    {
        if (!_checkStatusOnly)
        {
            if (_player.militaryTime) { _player.militaryTime = false; }
            else _player.militaryTime = true;
        }
        if (_player.militaryTime) { _textBox.text = "24 Hour Clock"; }
        else _textBox.text = "12 Hour Clock";
    }

    private void ChangePlayerOrientation()
    {
        if (!_checkStatusOnly)
        {
            if (_player.headOrientation) { _player.headOrientation = false; }
            else _player.headOrientation = true;
            _player.OrientationSource();
        }
        if (_player.headOrientation) { _textBox.text = "Orientation:\nHead"; }
        else _textBox.text = "Orientation:\nHand";
    }

    private void ChangePlayerRotation()
    {
        if (!_checkStatusOnly)
        {
            if (_player.snapTurnOn) { _player.snapTurnOn = false; }
            else _player.snapTurnOn = true;
        }
        if (_player.snapTurnOn) { _textBox.text = "Snap Turning"; }
        else _textBox.text = "Smooth Turning";
    }

    private void ToggleRoomScale()
    {
        if (!_checkStatusOnly)
        {
            if (_player.roomScale) { _player.roomScale = false; }
            else _player.roomScale = true;
        }
        if (_player.roomScale) { _textBox.text = "Roomscale:\nOn"; }
        else _textBox.text = "Roomscale:\nOff";
    }

    private void ChangeGrip()
    {
        if (!_checkStatusOnly)
        {
            if (_player.toggleGrip) { _player.toggleGrip = false; }
            else _player.toggleGrip = true;
        }
        if (_player.toggleGrip) { _textBox.text = "Toggle Grip"; }
        else _textBox.text = "Hold Grip";
    }

    private void LeftControllerSensitivty()
    {
        if (!_checkStatusOnly)
        {
            _player.leftJoystickDeadzoneAdjustment += _valueAdjustment;
            if (_player.leftJoystickDeadzoneAdjustment >= .9f) { _player.leftJoystickDeadzoneAdjustment = .9f; }
            else if (_player.leftJoystickDeadzoneAdjustment <= 0) { _player.leftJoystickDeadzoneAdjustment = 0; }
        }
        _textBox.text = "Left Sensitivity:\n" + Mathf.RoundToInt(_player.leftJoystickDeadzoneAdjustment * 100) + "%";
    }

    private void RightControllerSensitivty()
    {
        if (!_checkStatusOnly)
        {
            _player.rightJoystickDeadzoneAdjustment += _valueAdjustment;
            if (_player.rightJoystickDeadzoneAdjustment >= .9f) { _player.rightJoystickDeadzoneAdjustment = .9f; }
            else if (_player.rightJoystickDeadzoneAdjustment <= 0) { _player.rightJoystickDeadzoneAdjustment = 0; }
        }
        _textBox.text = "Right Sensitivity:\n" + Mathf.RoundToInt(_player.rightJoystickDeadzoneAdjustment * 100) + "%";
    }

    private void SmoothTurningAdjustment()
    {
        if (!_checkStatusOnly)
        {
            _player.turnSpeedAdjustment += _valueAdjustment;
            if (_player.turnSpeedAdjustment >= 4f) { _player.turnSpeedAdjustment = 4f; }
            else if (_player.turnSpeedAdjustment <= .1f) { _player.turnSpeedAdjustment = .1f; }
        }
        _textBox.text = "Smooth Turning:\n" + Mathf.RoundToInt(_player.turnSpeedAdjustment * 100) + "%";
    }

    private void SnapTurningAdjustment()
    {
        if (!_checkStatusOnly)
        {
            _player.snapTurnRotationAdjustment += _valueAdjustment;
            if (_player.snapTurnRotationAdjustment >= 90f) { _player.snapTurnRotationAdjustment = 90f; }
            else if (_player.snapTurnRotationAdjustment <= 10f) { _player.snapTurnRotationAdjustment = 10f; }
        }
        _textBox.text = "Snap Turning:\n" + Mathf.RoundToInt(_player.snapTurnRotationAdjustment);
    }

    private void ChangeMusicVolume()
    {
        if (!_checkStatusOnly) { _audioController.AdjustMusicVolume(_valueAdjustment); }

        _textBox.text = "Music: " + Mathf.RoundToInt(_audioController.GetMusicVolume() * 100);
    }

    private void ChangeSFXVolume()
    {
        if (!_checkStatusOnly) { _audioController.AdjustSFXVolume(_valueAdjustment); }

        _textBox.text = "SFX: " + Mathf.RoundToInt(_audioController.GetSFXVolume() * 100);
    }

    private void ChangeCreatureSFXVolume()
    {
        if (!_checkStatusOnly) { _audioController.AdjustCreatureSFXVolume(_valueAdjustment); }

        _textBox.text = "Creature SFX: " + Mathf.RoundToInt(_audioController.GetCreatureSFXVolume() * 100);
    }

    private void AdjustHandPositioning()
    {
        _optionsMenu.OpenHandAdjuster();
        _menu.gameObject.SetActive(false);
    }

    private void ChangeControllerType()
    {
        //auto-detected
    }

    private void ResetHandAlignment()
    {
        foreach (VRPlayerHand hand in _playerComponents.GetBothHands())
        {
            _gameManager.GetControllerType().ResetHandToControllerDefault(hand);
        }
    }

    private void SprintToggle()
    {
        if (!_checkStatusOnly)
        {
            if (_player.toggleSprint) { _player.toggleSprint = false; }
            else _player.toggleSprint = true;
        }

        if (_player.toggleSprint) { _textBox.text = "Toggle Sprint:\nOn"; }
        else _textBox.text = "Toggle Sprint:\nOff";
    }

    private void PlayerCalibration()
    {
        _optionsMenu.OpenPlayerCalibration();

        if (_menu != null && _menu.gameObject != null) { _menu.gameObject.SetActive(false); }
        else { Destroy(gameObject); }
    }

    private void ClearHands()
    {
        _playerComponents.GetHand(0).EmptyHand();
        _playerComponents.GetHand(1).EmptyHand();
    }

    private void KeyboardTyping()
    {
        if (_menu != null)
        {
            if (_stringName == "Erase") { _menu.multiplayerRoomName = null; }
            else { _menu.multiplayerRoomName = _menu.multiplayerRoomName + _stringName; }
            _textBox.text = _menu.multiplayerRoomName;
        }

        else
        {
            if (_stringName == "Erase") { _textBox.text = null; }
            else if (_stringName == "Send")
            {
                _chatManager.ChatMessage("[You] " + _textBox.text);

                if (CoopManager.instance != null) { CoopManager.instance.SendChatMessage(_textBox.text); }
            }

            else { _textBox.text += _stringName; }
        }
    }

    private void JoinCreateServer()
    {
        if (_checkStatusOnly)
        {
            if (CoopManager.instance == null) { _textBox.text = "Join/Create\n" + "Room"; }
            else _textBox.text = "Leave\n" + "Room";
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
        if (_stringName == "PreviousMessage") { _chatManager.DisplayPreviousMessage(); }
        else if (_stringName == "NextMessage") { _chatManager.DisplayNextMessage(); }
        else if (_stringName == "Keyboard")
        {
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
        }
        else if (_stringName == "Erase") { _chatManager.DeleteMessageHistory(); }
    }

    private void ToggleTextChat()
    {
        if (_chatManager.textChat)
        {
            if (_checkStatusOnly) { _textBox.text = "Text Chat:\nEnabled"; }
            else
            {
                _chatManager.textChat = false;
                _textBox.text = "Text Chat:\nDisabled";
            }
        }

        else
        {
            if (_checkStatusOnly) { _textBox.text = "Text Chat:\nDisabled"; }
            else
            {
                _chatManager.textChat = true;
                _textBox.text = "Text Chat:\nEnabled";
            }
        }
    }

    private void ChangeChatHand()
    {
        if (!_chatManager.chatOnRightHand)
        {
            if (_checkStatusOnly) { _textBox.text = "Chat Hand:\nRight Hand"; }
            else
            {
                _chatManager.chatOnRightHand = true;
                _textBox.text = "Chat Hand:\nLeft Hand";
            }
        }

        else
        {
            if (_checkStatusOnly) { _textBox.text = "Chat Hand:\nLeft Hand"; }
            else
            {
                _chatManager.chatOnRightHand = false;
                _textBox.text = "Chat Hand:\nRight Hand";
            }
        }
    }

    private void ToggleDebugChat()
    {
        if (_chatManager.allowDebugMessages)
        {
            if (_checkStatusOnly) { _textBox.text = "Debug Chat:\nEnabled"; }
            else
            {
                _chatManager.allowDebugMessages = false;
                _textBox.text = "Debug Chat:\nDisabled";
            }
        }

        else
        {
            if (_checkStatusOnly) { _textBox.text = "Debug Chat:\nDisabled"; }
            else
            {
                _chatManager.allowDebugMessages = true;
                _textBox.text = "Debug Chat:\nEnabled";
                _chatManager.DebugMessage("Debug messages allowed");
            }
        }
    }

    private void ToggleVoiceChat()
    {
        if (_chatManager.voiceChat)
        {
            if (_checkStatusOnly) { _textBox.text = "Voice Chat:\nn/a"; }
            else
            {
                _chatManager.voiceChat = false;
                _textBox.text = "Voice Chat:\nn/a";
            }
        }

        else
        {
            if (_checkStatusOnly) { _textBox.text = "Voice Chat:\nn/a"; }
            else
            {
                _chatManager.voiceChat = true;
                _textBox.text = "Voice Chat:\nn/a";
            }
        }
    }

    private void ToggleNotifications()
    {
        if (_chatManager.notifications)
        {
            if (_checkStatusOnly) { _textBox.text = "Notifications:\nEnabled"; }
            else
            {
                _chatManager.notifications = false;
                _textBox.text = "Notifications:\nDisabled";
            }
        }

        else
        {
            if (_checkStatusOnly) { _textBox.text = "Notifications:\nDisabled"; }
            else
            {
                _chatManager.notifications = true;
                _textBox.text = "Notifications:\nEnabled";
            }
        }
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
                _textBox.text = "Soft Shadows";
                break;
            case LightShadows.Hard:
                _textBox.text = "Hard Shadows";
                break;
            case LightShadows.None:
                _textBox.text = "No Shadows";
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
                    _textBox.text = "Shadow Quality:\nVery High";
                    break;
                case ShadowResolution.High:
                    _textBox.text = "Shadow Quality:\nHigh";
                    break;
                case ShadowResolution.Medium:
                    _textBox.text = "Shadow Quality:\nMedium";
                    break;
                case ShadowResolution.Low:
                    _textBox.text = "Shadow Quality:\nLow";
                    break;
            }
        }

        else
        {
            switch (_visualSettings.shadowQuality)
            {
                case ShadowResolution.VeryHigh:
                    _visualSettings.shadowQuality = ShadowResolution.High;
                    _textBox.text = "Shadow Quality:\nHigh";
                    break;
                case ShadowResolution.High:
                    _visualSettings.shadowQuality = ShadowResolution.Medium;
                    _textBox.text = "Shadow Quality:\nMedium";
                    break;
                case ShadowResolution.Medium:
                    _visualSettings.shadowQuality = ShadowResolution.Low;
                    _textBox.text = "Shadow Quality:\nLow";
                    break;
                case ShadowResolution.Low:
                    _visualSettings.shadowQuality = ShadowResolution.VeryHigh;
                    _textBox.text = "Shadow Quality:\nVery High";
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

        _textBox.text = "Light Range:\n" + range;
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

        _textBox.text = "Light Brightness:\n" + brightnessPercentage + "%";
    }

    private void AmbientOcclusionToggle()
    {
        if (!_checkStatusOnly)
        {
            if (_postProcessingController.ambientOcclusion) { _postProcessingController.ambientOcclusion = false; }
            else _postProcessingController.ambientOcclusion = true;

            _postProcessingController.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.ambientOcc);
        }

        if (_postProcessingController.ambientOcclusion) { _textBox.text = "Ambient Occlusion On"; }
        else _textBox.text = "Ambient Occlusion Off";
    }

    private void BloomToggle()
    {
        if (!_checkStatusOnly)
        {
            if (_postProcessingController.bloom) { _postProcessingController.bloom = false; }
            else _postProcessingController.bloom = true;

            _postProcessingController.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.bloomEffect);
        }

        if (_postProcessingController.bloom) { _textBox.text = "Bloom On"; }
        else _textBox.text = "Bloom Off";
    }

    private void ColorGradingToggle()
    {
        if (!_checkStatusOnly)
        {
            if (_postProcessingController.colorGrading) { _postProcessingController.colorGrading = false; }
            else _postProcessingController.colorGrading = true;

            _postProcessingController.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.color);
        }

        if (_postProcessingController.colorGrading) { _textBox.text = "Color Grading On"; }
        else _textBox.text = "Color Grading Off";
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

        _textBox.text = "Intensity:\n" + _postProcessingController.AOIntensity;
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

        _textBox.text = "Thickness:\n" + _postProcessingController.thickness;
    }

    private void BloomIntensity()
    {
        if (!_checkStatusOnly)
        {
            _postProcessingController.Bintensity += _valueAdjustment;
            _postProcessingController.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.bloomEffect);
        }

        _textBox.text = "Intensity:\n" + Mathf.RoundToInt(_postProcessingController.Bintensity);
    }

    private void BloomThreshold()
    {
        if (!_checkStatusOnly)
        {
            _postProcessingController.threshold += _valueAdjustment;
            _postProcessingController.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.bloomEffect);
        }

        _textBox.text = "Threshold:\n" + _postProcessingController.threshold;
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

        _textBox.text = "Diffusion:\n" + _postProcessingController.diffusion;
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
                _textBox.text = "Tonemapper:\nNone";
                break;

            case UnityEngine.Rendering.PostProcessing.Tonemapper.Neutral:
                _textBox.text = "Tonemapper:\nNeutral";
                break;

            case UnityEngine.Rendering.PostProcessing.Tonemapper.ACES:
                _textBox.text = "Tonemapper:\nACES";
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

        _textBox.text = "Temperature:\n" + Mathf.RoundToInt(_postProcessingController.temperature);
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

        _textBox.text = "Tint:\n" + Mathf.RoundToInt(_postProcessingController.tint);
    }

    private void ColorGradingPostExposure()
    {
        if (!_checkStatusOnly)
        {
            _postProcessingController.postExposure += _valueAdjustment;
            _postProcessingController.ChangePostProcessingSettings(PostProcessingController.PostEffectAdjustment.color);
        }

        _textBox.text = "Post Exposure:\n" + _postProcessingController.postExposure;
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

        _textBox.text = "Hue Shift:\n" + Mathf.RoundToInt(_postProcessingController.hueShift);
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

        _textBox.text = "Saturation:\n" + Mathf.RoundToInt(_postProcessingController.saturation);
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

        _textBox.text = "Contrast:\n" + Mathf.RoundToInt(_postProcessingController.contrast);
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

        _textBox.text = "Back Attachments:\n" + _playerComponents.backAttachments.transform.localPosition.z;
    }

    private void BeltAdjustment()
    {
        PlayerBelt belt = _playerComponents.belt;

        if (_player.playerStanding)
        {
            if (!_checkStatusOnly)
            {
                belt.heightStandingPlayer += _valueAdjustment;

                if (belt.heightStandingPlayer > 1) { belt.heightStandingPlayer = 1; }
                else if (belt.heightStandingPlayer < 0) { belt.heightStandingPlayer = 0; }
            }

            _textBox.text = "Belt Adjustment:\n" + belt.heightStandingPlayer;
        }

        else
        {
            if (!_checkStatusOnly)
            {
                belt.heightSittingPlayer += _valueAdjustment;

                if (belt.heightSittingPlayer > 1) { belt.heightSittingPlayer = 1; }
                else if (belt.heightSittingPlayer < 0) { belt.heightSittingPlayer = 0; }
            }

            _textBox.text = "Belt Adjustment:\n" + belt.heightSittingPlayer;
        }
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

        _textBox.text = "Crouched Belt Offset:\n" + belt.zAdjustmentForSittingPlayer;
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
            if (!_player.physicalJumping) { _player.physicalJumping = true; }
            else { _player.physicalJumping = false; }
        }
        if (_player.physicalJumping) { _textBox.text = "Physical Jumping:\nOn"; }
        else { _textBox.text = "Physical Jumping:\nOff"; }
    }

    private void OpenDiscordLink()
    {
        Application.OpenURL("https://discord.gg/zycWTnYqCY");
    }
}
