using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Gui;


public class PlayerInput : MonoBehaviour {
    public LeanJoystick joystickInput;
    public float normalInputX { get => joystickInput.ScaledValue.x; }
}
