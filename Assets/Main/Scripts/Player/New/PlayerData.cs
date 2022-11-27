using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "Data/Player Data/Base Data")]

public class PlayerData : ScriptableObject
{
    [Header("---MOVE STATE---")]
    public float movementVelocity = 1f;
    [Header("---JUMP STATE---")]
    public float jumpVelocity = 1f;
    public int amountOfjumps = 1;
    [Header("---IN AIR STATE---")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;
    [Header("WALL SLIDE STATE")]
    public float wallSlideVelocity = 3f;
    [Header("WALL JUMP STATE")]
    public float wallJumpVelocity = 20;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);
}
