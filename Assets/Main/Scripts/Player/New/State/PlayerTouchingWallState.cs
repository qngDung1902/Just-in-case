using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName) { }

    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool grabInput;
    protected bool jumpInput;
    protected bool isTouchingLedge;
    protected float xInput;
    protected float yInput;

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = core.CollisionSenses.Ground;
        isTouchingWall = core.CollisionSenses.WallFront;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.Core.Input.normalInputX;
        jumpInput = player.Core.Input.JumpInput;

        if (jumpInput)
        {
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if (isGrounded && !grabInput)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (!isTouchingWall || (xInput != core.Movement.FacingDirection && !grabInput))
        {
            stateMachine.ChangeState(player.InAirState);
        }
        // else if (isTouchingWall && !isTouchingLedge)
        // {
        //     stateMachine.ChangeState(player.LedgeClimbState);
        // }
    }
}
