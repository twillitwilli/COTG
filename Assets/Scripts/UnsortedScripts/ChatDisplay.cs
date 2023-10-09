using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatDisplay : MonoBehaviour
{
    public GameObject hand;
    public GameObject chatSystem;

    [HideInInspector] 
    public bool 
        chatOpened, 
        isRightHand;
    
    public GameObject spawnedChat { get; set; }

    void Start()
    {
        isRightHand = GetComponentInParent<VRHand>().IsRightHand();
    }

    void FixedUpdate()
    {
        if (ChatManager.Instance.textChat)
        {
            if (!chatOpened && spawnedChat == null)
            {
                if (isRightHand && ChatManager.Instance.chatOnRightHand && Vector3.Angle(hand.transform.up, -Vector3.up) < 45)
                    SpawnChat();

                else if (!isRightHand && !ChatManager.Instance.chatOnRightHand && Vector3.Angle(-hand.transform.up, Vector3.up) < 45)
                    SpawnChat();
            }

            else
            {
                if (isRightHand && ChatManager.Instance.chatOnRightHand && Vector3.Angle(hand.transform.up, -Vector3.up) > 45)
                    CloseChat();

                else if (!isRightHand && !ChatManager.Instance.chatOnRightHand && Vector3.Angle(-hand.transform.up, Vector3.up) > 45)
                    CloseChat();
            }
        }
    }

    void SpawnChat()
    {
        spawnedChat = Instantiate(chatSystem, transform.position, transform.rotation);
        spawnedChat.transform.SetParent(this.transform);
        spawnedChat.transform.localPosition = new Vector3(0, 0, 0);
        spawnedChat.transform.localEulerAngles = new Vector3(0, 0, 0);
        chatOpened = true;
    }

    void CloseChat()
    {
        Destroy(spawnedChat);
        chatOpened = false;
    }
}
