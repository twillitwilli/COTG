using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassSelector : MonoBehaviour
{
    [SerializeField] 
    private MagicController.ClassType _classType;

    public GameObject[] enableObjs, disableObjs;

    public void ChangeClass()
    {
        MagicController.Instance.ChangeClass(_classType);

        foreach (GameObject obj in enableObjs) 
        { 
            obj.SetActive(true); 
        }

        foreach (GameObject obj in disableObjs) 
        { 
            obj.SetActive(false); 
        }
    }
}
