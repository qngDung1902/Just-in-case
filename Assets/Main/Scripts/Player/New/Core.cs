using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {
    public Movement Movement { get; private set; }
    public CollisionSenses CollisionSenses { get; private set; }
    public PlayerInput Input { get; private set; }

    void Awake() {
        Movement = GetComponent<Movement>();
        CollisionSenses = GetComponent<CollisionSenses>();
        Input = GetComponent<PlayerInput>();
    }

    public void LogicUpdate() {
        Movement.LogicUpdate();
    }
}
