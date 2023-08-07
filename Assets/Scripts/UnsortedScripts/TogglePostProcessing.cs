using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePostProcessing : MonoBehaviour
{
    public bool on;

    private void Start()
    {
        LocalGameManager.instance.GetPostProcessingController().TogglePostProcessing(on);
    }
}
