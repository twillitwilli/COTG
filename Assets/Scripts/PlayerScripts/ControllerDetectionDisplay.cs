using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ControllerDetectionDisplay : MonoBehaviour
{
    public bool autoDestroyParent;

    [SerializeField] 
    private GameObject displayParent;

    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private async void Start()
    {
        await Task.Delay(1000);

        ControllerDetection();
    }

    private void ControllerDetection()
    {
        if (ControllerType.Instance.currentController != null)
        {
            text.text = ControllerType.Instance.currentController;
            if (text.text == "Oculus")
            {
                if (autoDestroyParent)
                    DestroyParent();
            }
            else
                text.text = "Unknown Controller Index: " + text.text;
        }
    }

    private void DestroyParent()
    {
        Destroy(displayParent);
    }
}
