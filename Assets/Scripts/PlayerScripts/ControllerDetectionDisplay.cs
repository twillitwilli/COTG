using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerDetectionDisplay : MonoBehaviour
{
    private ControllerType _controllerType;

    public bool autoDestroyParent;
    [SerializeField] private GameObject displayParent;
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Start()
    {
        Invoke("ControllerType", 1);
    }

    private void ControllerType()
    {
        _controllerType = LocalGameManager.Instance.GetControllerType();

        if (_controllerType.currentController != null)
        {
            text.text = _controllerType.currentController;
            if (text.text == "Oculus")
            {
                if (autoDestroyParent) { DestroyParent(); }
            }
            else
            {
                text.text = "Unknown Controller Index: " + text.text;
            }
        }
    }

    private void DestroyParent()
    {
        Destroy(displayParent);
    }
}
