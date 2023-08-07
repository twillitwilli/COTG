using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossibleBosses : MonoBehaviour
{
    public static PossibleBosses instance;

    public List<GameObject> bosses;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
