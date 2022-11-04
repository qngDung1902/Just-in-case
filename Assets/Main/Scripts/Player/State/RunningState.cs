using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : GroundedState
{
    public RunningState(InputController character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        character.animatorController.Play("Base_run");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (character.horizontal == 0)
        {
            stateMachine.ChangeState(character.idleState);
        }

        if (jump)
        {
            stateMachine.ChangeState(character.jumpState);
        }
    }
}
