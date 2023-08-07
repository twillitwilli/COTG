using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[CreateAssetMenu(menuName = "MasterManager/CheckControllerType")]
public class CheckControllerType : ScriptableObject
{
    public List<Vector3> oculusPos, oculusRot, 
        indexPos, indexRot, 
        wmrPos, wmrRot, 
        vivePos, viveRot, 
        quest2Pos, quest2Rot,
        customPos, customRot;
}
