using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected float xInput;


    bool jumpInput, isGrounded;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName) { }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = core.CollisionSenses.Ground;
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetAmountOfJumps();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = core.Input.normalInputX;
        jumpInput = core.Input.JumpInput;
        if (jumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
