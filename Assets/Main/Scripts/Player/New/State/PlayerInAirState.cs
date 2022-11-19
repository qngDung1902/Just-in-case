using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    float xInput;
    float startWallJumpCoyoteTime;

    bool jumpInput, jumpInputStop, isJumping, isGrounded, coyoteTime;
    bool isTouchingWall, isTouchingWallBack, oldIsTouchingWall, oldIsTouchingWallBack;

    bool wallJumpCoyoteTime;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player: player, stateMachine: stateMachine, animationName: animationName) { }

    public override void Exit()
    {
        base.Exit();

        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;
        isTouchingWall = false;
        isTouchingWallBack = false;
    }
    public override void DoChecks()
    {
        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = core.CollisionSenses.Ground;
        isTouchingWall = core.CollisionSenses.WallFront;
        isTouchingWallBack = core.CollisionSenses.WallBack;

        if (!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    private void CheckWallJumpCoyoteTime()
    {
        if (wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + player.Stat.coyoteTime)
        {
            wallJumpCoyoteTime = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        xInput = core.Input.normalInputX;
        jumpInput = core.Input.JumpInput;
        jumpInputStop = core.Input.JumpInputStop;
        isGrounded = core.CollisionSenses.Ground;
        CheckJumpMultiplier();

        if (isGrounded && core.Movement.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if (jumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if(jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            isTouchingWall = core.CollisionSenses.WallFront;
            // player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            // stateMachine.ChangeState(player.WallJumpState);
        }
        else if(isTouchingWall && xInput == core.Movement.FacingDirection && core.Movement.CurrentVelocity.y <= 0)
        {
            // stateMachine.ChangeState(player.WallSlideState);
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
    public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;
    public void StartWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    }

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
