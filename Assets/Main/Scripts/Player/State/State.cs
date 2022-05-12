using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected InputController character;
    protected StateMachine stateMachine;

    public bool jump { get; set; }
    public bool ground { get; set; }

    protected State(InputController character, StateMachine stateMachine)
    {
        this.character = character;
        this.stateMachine = stateMachine;
    }
    public virtual void Enter()
    {

    }

    public virtual void HandleInput()
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicUpdate()
    {

    }

    public virtual void Exit()
    {

    }
}
