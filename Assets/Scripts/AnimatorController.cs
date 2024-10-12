using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator animator;
    private CharacterMovement movement;
    private bool happensOnce; 

    public void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<CharacterMovement>();
    }

    public void LateUpdate()
    {
       UpdateAnimator();
    }

    // TODO Fill this in with your animator calls
    void UpdateAnimator()
    {

        animator.SetFloat("CharacterSpeed", movement.GetAnimationSpeed());
        animator.SetBool("IsFalling", !movement.isGrounded);

    }
}
