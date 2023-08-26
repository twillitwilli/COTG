using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    [HideInInspector] public bool textChat, allowDebugMessages, voiceChat, notifications, chatOnRightHand;
    [HideInInspector] public string displayMessage;
    [HideInInspector] public List<string> messageHistory = new List<string>();
    [HideInInspector] public int totalMessages = -1, currentMessage = -1;

    private void Start()
    {
        if (textChat)
        {
            Invoke("SpawnChat", 3);
        }
    }

    public void DefaultChatSettings()
    {
        textChat = true;
        allowDebugMessages = false;
        voiceChat = false;
        notifications = true;
        chatOnRightHand = false;
    }

    public void DebugMessage(string message)
    {
        if (allowDebugMessages)
        {
            displayMessage = "[Sys] " + message;
            messageHistory.Add("[Sys] " + message);
            totalMessages++;
            currentMessage = totalMessages;
            UpdateDisplayMessage(message);
        }
    }

    public void ChatMessage(string message)
    {
        displayMessage = message;
        messageHistory.Add(message);
        totalMessages++;
        currentMessage = totalMessages;
        UpdateDisplayMessage(message);
    }

    public void DisplayPreviousMessage()
    {
        if (currentMessage > 0)
        {
            currentMessage--;
            displayMessage = messageHistory[currentMessage];
            UpdateDisplayMessage(displayMessage);
        }
    }

    public void DisplayNextMessage()
    {
        if (currentMessage < totalMessages)
        {
            currentMessage++;
            displayMessage = messageHistory[currentMessage];
            UpdateDisplayMessage(displayMessage);
        }
    }

    public void UpdateDisplayMessage(string message)
    {
        foreach (VRPlayerHand hand in LocalGameManager.Instance.player.GetPlayerComponents().GetBothHands())
        {
            if (hand.chatDisplay.chatSystem != null)
            {
                ChatWindow chatWindow = hand.chatDisplay.chatSystem.GetComponent<ChatWindow>();
                chatWindow.chatDisplay.text = message;
            }
        }
    }

    public void DeleteMessageHistory()
    {
        if (messageHistory.Count > 0)
        {
            messageHistory.Clear();
            currentMessage = -1;
            totalMessages = -1;
            displayMessage = null;
        }
    }
}
