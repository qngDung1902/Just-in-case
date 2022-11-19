using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName) { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            core.Movement.SetVelocityY(-player.Stat.wallSlideVelocity);

            // if (grabInput && yInput == 0)
            // {
            //     stateMachine.ChangeState(player.WallGrabState);
            // }
        }

    }
}
