using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingState : GroundedState
{
    
    public StandingState(InputController character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        character.animatorController.Play("Base_idle");
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (character.horizontal != 0)
        {
            stateMachine.ChangeState(character.running);
        }

        if (jump)
        {
            stateMachine.ChangeState(character.jumping);
        }
    }
}
