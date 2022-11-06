using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState {
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName) { }

    public override void LogicUpdate() {
        base.LogicUpdate();

        core.Movement.CheckIfShouldFlip(xInput);
        core.Movement.SetVelocityX(10 * xInput);
    }
}