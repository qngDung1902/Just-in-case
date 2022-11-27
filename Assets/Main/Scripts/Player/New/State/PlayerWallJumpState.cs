using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName) { }

    public override void Enter()
    {
        base.Enter();

        player.Core.Input.UseJumpInput();
        player.JumpState.ResetAmountOfJumps();
        core.Movement.SetVelocity(player.Stat.wallJumpVelocity, player.Stat.wallJumpAngle, wallJumpDirection);
        core.Movement.CheckIfShouldFlip(wallJumpDirection);
        player.JumpState.DecreaseAmountOfJumps();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.Animator.SetFloat("yVelocity", core.Movement.CurrentVelocity.y);
        player.Animator.SetFloat("xVelocity", Mathf.Abs(core.Movement.CurrentVelocity.x));

        if (Time.time >= startTime + player.Stat.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            wallJumpDirection = -core.Movement.FacingDirection;
        }
        else
        {
            wallJumpDirection = core.Movement.FacingDirection;
        }
    }
}
