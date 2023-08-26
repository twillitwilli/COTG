using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameManager : MonoBehaviour
{
    public GameObject gameManager;

    private void Awake()
    {
        if (!LocalGameManager.Instance) 
        {
            GameObject newManager = Instantiate(gameManager);
            newManager.transform.SetParent(null);
            DontDestroyOnLoad(newManager);
            newManager.transform.position = new Vector3(0, 0, 0);
            newManager.transform.localEulerAngles = new Vector3(0, 0, 0);
            newManager.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
