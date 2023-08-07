using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadInWall : MonoBehaviour
{
    [SerializeField] private EyeManager _eyeManager;

    public GameObject underWaterVision;

    private int _headInWall;
    private bool _blockVision;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Climb Point")) { _blockVision = true; Invoke("DelayCheck", 2); }

        if (other.gameObject.CompareTag("Water")) { underWaterVision.SetActive(true); }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Climb Point")) { _blockVision = false; DelayCheck(); }

        if (other.gameObject.CompareTag("Water")) { underWaterVision.SetActive(false); }
    }

    private void DelayCheck()
    {
        if (_blockVision) _headInWall++;
        else _headInWall--;

        CheckHeadInWall();
    }

    private void CheckHeadInWall()
    {
        if (_headInWall < 0) _headInWall = 0;

        if (_headInWall > 0 && _blockVision) { _eyeManager.EyesClosing(); }
        else { _headInWall = 0; _eyeManager.EyesOpening(); }
    }
}
