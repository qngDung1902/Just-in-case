using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : State
{
    private float jumpForce = 10;
    private float speedWhenJump = 10;

    public JumpingState(InputController character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        ground = false;
        character.animatorController.Play("base_falling");
        Jump();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (ground && character.horizontal == 0)
        {
            stateMachine.ChangeState(character.idleState);
        }

        if (ground && character.horizontal != 0)
        {
            stateMachine.ChangeState(character.runState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        character.UpdateMovement(speedWhenJump);
        character.UpdateDirection();
    }

    private void Jump()
    {
        character.ApplyJump(jumpForce);
    }
}
