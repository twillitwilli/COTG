using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovementController : MonoBehaviour
{
    [SerializeField] private MinionPetController minionController;
    public MinionAnimator minionAnimator;

    public void MoveMinion(Transform target)
    {
        transform.position = Vector3.Lerp(transform.position, target.position, minionController.minionSpeed * Time.deltaTime);
        if (minionAnimator.currentAnimation != MinionAnimator.minionState.walking) { minionAnimator.ChangeAnimation(MinionAnimator.minionState.walking); }
    }
}
