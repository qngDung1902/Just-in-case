using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AnimatorController : MonoBehaviour
{
    public Animator animator;
    private string currentAnimation;

    public void UpdateAnimationState(PlayerState state)
    {
        if (InputController.Instance.playerForm == PlayerForm.BASIC)
        {
            switch (state)
            {
                case PlayerState.IDLE:
                    ChangeAnimation("Base_idle");
                    break;

                case PlayerState.RUN:
                    ChangeAnimation("Base_run");
                    break;

                case PlayerState.DASH:
                    ChangeAnimation("base_dash");
                    break;

                case PlayerState.FALLING:
                    ChangeAnimation("base_falling");
                    break;
            }
        }
        else if (InputController.Instance.playerForm == PlayerForm.DEMON)
        {
            switch (state)
            {
                case PlayerState.TAKEDOWN:
                    ChangeAnimation("player_demon_takingdown");
                    break;

                case PlayerState.IDLE:
                    ChangeAnimation("player_demon_idle");
                    break;

                case PlayerState.RUN:
                    ChangeAnimation("player_demon_run");
                    break;

                case PlayerState.DASH:
                    ChangeAnimation("player_demon_dash");
                    break;

                case PlayerState.FALLING:
                    break;
            }
        }


    }

    private void ChangeAnimation(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;
        {
            animator.Play(newAnimation);
            currentAnimation = newAnimation;
        }
    }

    public void ChangeToDemonAnimator()
    {
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Demon_animator");
    }

    public void Play(string newAnimation)
    {
        ChangeAnimation(newAnimation);
    }
}

public enum PlayerState
{
    TAKEDOWN,
    IDLE,
    RUN,
    DASH,
    FALLING,
}

public enum PlayerForm
{
    BASIC,
    DEMON
}
