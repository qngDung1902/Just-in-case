using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AnimatorController : MonoBehaviour
{
    public Animator animator;
    public InputController inputController;


    public void UpdateAnimationState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.IDLE:
                animator.SetTrigger("idle");
                animator.SetBool("run", false);
                break;

            case PlayerState.RUN:
                animator.SetBool("run", true);
                break;
        }
    }
}
