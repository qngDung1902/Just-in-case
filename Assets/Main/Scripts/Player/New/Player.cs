using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Player : MonoBehaviour
{
    public bool isPlaying;
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
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }

    void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, "move");
        JumpState = new PlayerJumpState(this, StateMachine, "in-air");
        InAirState = new PlayerInAirState(this, StateMachine, "in-air");
        LandState = new PlayerLandState(this, StateMachine, "land");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, "in-air");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, "wall-slide");
    }

    public Vector2 lastCheckpointPosition 
    {
        get
        {
            return new Vector2(PlayerPrefs.GetFloat("checkpointX", 0), PlayerPrefs.GetFloat("checkpointY", 0));
        }
        set
        {
            Vector2 localValue = value;
            PlayerPrefs.SetFloat("checkpointX", localValue.x);
            PlayerPrefs.SetFloat("checkpointY", localValue.y);
        }
    }

    public void Spawn()
    {
        StateMachine.ChangeState(IdleState);
        transform.position = lastCheckpointPosition;
    }

    void Start()
    {
        StateMachine.Initialize(IdleState);
        if (!isPlaying) return; 

        if (GlobalSetting.isContinue)
        {
            transform.position = GlobalSetting.autosaveCheckpoint;
        }
        else
        {
            PlayerDataManager.Score = 0;
            lastCheckpointPosition = transform.position;
        }
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
