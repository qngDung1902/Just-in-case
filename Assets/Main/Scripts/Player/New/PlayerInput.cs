using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Gui;


public class PlayerInput : MonoBehaviour
{
    public LeanJoystick joystickInput;
    public float normalInputX { get => joystickInput.ScaledValue.x; }

    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }

    float jumpInputStartTime, inputHoldTIme;

    public void UseJumpInput() => JumpInput = false;

    void Update()
    {
        CheckJumpInputHoldTime();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpInput(true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            OnJumpInput(false);
        }
    }

    void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTIme)
        {
            JumpInput = false;
        }
    }
    public void OnJumpInput(bool value)
    {
        if (value)
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }
        else
        {
            JumpInputStop = true;
        }
    }
}
