using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAnimator : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimation(bool moving)
    {
        animator.SetBool("Fly Forward", moving);
    }
}
