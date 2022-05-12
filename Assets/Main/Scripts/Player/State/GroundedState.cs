using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : State
{
    protected float speed = 4;

    public GroundedState(InputController character, StateMachine stateMachine) : base(character, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        jump = false;
        ground = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        character.UpdateMovement(speed);
        character.UpdateDirection();
    }
}
