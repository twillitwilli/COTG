using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatWindow : MonoBehaviour
{
    public Text chatDisplay;
    public Transform keyboardSpawn;
    [HideInInspector] public GameObject spawnedKeyboard;

    private ChatManager _chatManager;

    public void Start()
    {
        _chatManager = LocalGameManager.instance.GetChatManager();

        if (_chatManager.displayMessage != null)
        {
            chatDisplay.text = _chatManager.displayMessage;
        }
    }
}
