using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatDisplay : MonoBehaviour
{
    public GameObject hand;
    public GameObject chatSystem;
    [HideInInspector] public bool chatOpened, isRightHand;
    [HideInInspector] public GameObject spawnedChat;

    private ChatManager _chatManager;

    public void Start()
    {
        _chatManager = LocalGameManager.instance.GetChatManager();

        isRightHand = GetComponentInParent<VRPlayerHand>().IsRightHand();
    }

    private void FixedUpdate()
    {
        if (LocalGameManager.instance.GetChatManager().textChat)
        {
            if (!chatOpened && spawnedChat == null)
            {
                if (isRightHand && _chatManager.chatOnRightHand && Vector3.Angle(hand.transform.up, -Vector3.up) < 45) { SpawnChat(); }
                else if (!isRightHand && !_chatManager.chatOnRightHand && Vector3.Angle(-hand.transform.up, Vector3.up) < 45) { SpawnChat(); }
            }
            else
            {
                if (isRightHand && _chatManager.chatOnRightHand && Vector3.Angle(hand.transform.up, -Vector3.up) > 45) { CloseChat(); }
                else if (!isRightHand && !_chatManager.chatOnRightHand && Vector3.Angle(-hand.transform.up, Vector3.up) > 45) { CloseChat(); }
            }
        }
    }

    private void SpawnChat()
    {
        spawnedChat = Instantiate(chatSystem, transform.position, transform.rotation);
        spawnedChat.transform.SetParent(this.transform);
        spawnedChat.transform.localPosition = new Vector3(0, 0, 0);
        spawnedChat.transform.localEulerAngles = new Vector3(0, 0, 0);
        chatOpened = true;
    }

    private void CloseChat()
    {
        Destroy(spawnedChat);
        chatOpened = false;
    }
}
