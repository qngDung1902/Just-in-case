using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName) { }

    public override void Enter()
    {
        DoChecks();
        player.Animator.SetBool(animationName, true);

        startTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;
    }

    public override void Exit()
    {
        base.Exit();
        player.Animator.SetBool(animationName, false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            if (xInput != 0)
            {
                stateMachine.ChangeState(player.MoveState);
            }
        }
    }
}
