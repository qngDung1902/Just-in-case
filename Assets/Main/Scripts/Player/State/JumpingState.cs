using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : State
{
    private float jumpForce = 10;
    private bool grounded;

    public JumpingState(InputController character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        grounded = false;
        Debug.Log(1);
        character.animatorController.Play("base_falling");
        Jump();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (grounded)
        {
            stateMachine.ChangeState(character.standing);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        grounded = CollisionController.Instance.onGround;
    }

    private void Jump()
    {
        character.ApplyJump(jumpForce);
    }
}
