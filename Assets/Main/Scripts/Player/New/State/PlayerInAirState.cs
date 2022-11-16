using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    float xInput;
    bool jumpInput, jumpInputStop, isJumping, isGrounded, coyoteTime;


    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player: player, stateMachine: stateMachine, animationName: animationName) { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        CheckCoyoteTime();
        CheckJumpMultiplier();

        xInput = core.Input.normalInputX;
        jumpInput = core.Input.JumpInput;
        jumpInputStop = core.Input.JumpInputStop;
        isGrounded = core.CollisionSenses.Ground;

        if (isGrounded && core.Movement.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if (jumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else
        {
            core.Movement.CheckIfShouldFlip(xInput);
            core.Movement.SetVelocityX(player.Stat.movementVelocity * xInput);
        }

        player.Animator.SetFloat("yVelocity", core.Movement.CurrentVelocity.y);
        player.Animator.SetFloat("xVelocity", Mathf.Abs(core.Movement.CurrentVelocity.x));
    }

    public void StartCoyoteTime() => coyoteTime = true;
    public void SetIsJumping() => isJumping = true;

    void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + player.Stat.coyoteTime)
        {
            coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumps();
        }
    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                core.Movement.SetVelocityY(core.Movement.CurrentVelocity.y * player.Stat.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (core.Movement.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }
}
