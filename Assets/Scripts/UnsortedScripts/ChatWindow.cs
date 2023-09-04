using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatWindow : MonoBehaviour
{
    public Text chatDisplay;
    public Transform keyboardSpawn;

    [HideInInspector] 
    public GameObject spawnedKeyboard;

    public void Start()
    {
        if (ChatManager.Instance.displayMessage != null)
        {
            chatDisplay.text = ChatManager.Instance.displayMessage;
        }
    }
}
