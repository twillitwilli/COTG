using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSelector : MonoBehaviour
{
    [SerializeField] 
    private MagicController.MagicType magicSelection;

    public GameObject[] enableObjs, disableObjs;

    public void ChangeMagic()
    {
        MagicController.Instance.SetToSpecificMagic(magicSelection);

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
