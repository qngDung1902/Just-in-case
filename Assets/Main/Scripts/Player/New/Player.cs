using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [Header("---CORE COMPONENTS---")]
    public Core Core;
    public Animator Animator;

    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }


    void Awake() {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, "move");
    }

    void Start() {
        StateMachine.Initialize(IdleState);
    }

    void Update() {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
    }

    void FixedUpdate() {
        StateMachine.CurrentState.PhysicUpdate();
    }

    void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
