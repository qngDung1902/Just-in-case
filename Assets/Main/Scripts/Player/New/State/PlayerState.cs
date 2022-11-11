using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState {
    protected Core core;
    protected Player player;
    protected PlayerStateMachine stateMachine;

    protected float startTime;
    protected bool isAnimationFinished, isExitingState;

    string animationName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animationName) {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animationName = animationName;

        core = player.Core;
    }

    public PlayerState() {
    }

    public virtual void Enter() {
        DoChecks();
        // player.Animator.SetBool(animationName, true);
        isAnimationFinished = false;
        isExitingState = false;
    }

    public virtual void Exit() {
        // player.Animator.SetBool(animationName, false);
        isExitingState = true;
    }

    public virtual void LogicUpdate() { }
    public virtual void PhysicUpdate() {
        DoChecks();
    }

    public virtual void DoChecks() { }
    public virtual void AnimationTrigger() { }
    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
