using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenTrigger : MonoBehaviour
{
    private void Start()
    {
        LocalGameManager.instance.inTitleScreen = true;
    }

    private void OnDisable()
    {
        LocalGameManager.instance.inTitleScreen = false;
    }
}
