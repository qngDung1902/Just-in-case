using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public Rigidbody2D rigid { get; private set; }

    public int FacingDirection { get; private set; }
    public bool CanSetVelocity { get; set; }

    public Vector2 CurrentVelocity { get; private set; }

    Vector2 workspace;
    int intInput;
    void Awake() {
        rigid = GetComponentInParent<Rigidbody2D>();

        FacingDirection = 1;
        CanSetVelocity = true;
    }

    public void LogicUpdate() {
        CurrentVelocity = rigid.velocity;
    }

    public void ClearVelocity() {
        workspace.Set(0, 0);
        SetFinalVelocity();
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction) {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
    }

    public void SetVelocity(float velocity, Vector2 direction) {
        workspace = direction * velocity;
        SetFinalVelocity();
    }

    public void SetVelocityX(float velocity) {
        workspace.Set(velocity, CurrentVelocity.y);
        SetFinalVelocity();
    }

    public void SetVelocityY(float velocity) {
        workspace.Set(CurrentVelocity.x, velocity);
        SetFinalVelocity();
    }

    public void CheckIfShouldFlip(float xInput) {
        if (xInput != 0f) {
            intInput = xInput > 0 ? 1 : -1;
            if (intInput != FacingDirection) {
                Flip();
            }
        }
    }

    public void Flip() {
        FacingDirection *= -1;
        rigid.transform.Rotate(0f, 180f, 0f);
    }

    public void SetFinalVelocity() {
        if (CanSetVelocity) {
            rigid.velocity = workspace;
            CurrentVelocity = workspace;
        }
    }
}
