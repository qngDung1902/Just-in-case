using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Player : MonoBehaviour
{
    [Header("---CORE COMPONENTS---")]
    public Core Core;
    public Animator Animator;
    public PlayerData Stat;

    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }



    void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, "move");
        JumpState = new PlayerJumpState(this, StateMachine, "jump");
        InAirState = new PlayerInAirState(this, StateMachine, "jump");
        LandState = new PlayerLandState(this, StateMachine, "land");

    }

    void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
    }

    void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicUpdate();
    }

    void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Handles.Label(transform.position, $"{StateMachine.CurrentState}");
        }
    }
#endif
}
