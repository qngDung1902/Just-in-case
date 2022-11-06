using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState {
    protected float xInput;

    protected bool isGrounded;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName) { }

    public override void DoChecks() {
        base.DoChecks();

        isGrounded = core.CollisionSenses.Ground;
    }

    public override void Enter() {
        base.Enter();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        xInput = player.Core.Input.normalInputX;
    }

    public override void PhysicUpdate() {
        base.PhysicUpdate();
    }
}
