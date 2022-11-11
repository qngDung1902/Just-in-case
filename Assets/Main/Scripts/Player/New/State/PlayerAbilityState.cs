using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState {
    protected bool isAbilityDone;
    bool isGrounded;
    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName) { }

    public override void DoChecks() {
        base.DoChecks();

        isGrounded = core.CollisionSenses.Ground;
    }

    public override void Enter() {
        base.Enter();

        isAbilityDone = false;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        if (isAbilityDone) {
            if (isGrounded && core.Movement.CurrentVelocity.y < 0.01f) {
                stateMachine.ChangeState(player.IdleState);
            } else {
                stateMachine.ChangeState(player.InAirState);
            }
        }
    }
}
