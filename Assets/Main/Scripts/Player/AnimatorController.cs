using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AnimatorController : MonoBehaviour
{
    public Animator animator;

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

            case PlayerState.DASH:
                animator.SetTrigger("dash");
                break;

            case PlayerState.FALLING:
                animator.SetBool("run", false);
                animator.SetTrigger("falling");
                break;
        }
    }
}

public enum PlayerState
{
    IDLE,
    RUN,
    DASH,
    FALLING,

}
