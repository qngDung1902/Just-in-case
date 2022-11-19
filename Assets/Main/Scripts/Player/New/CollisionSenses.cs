using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : MonoBehaviour
{
    Core core;
    public LayerMask groundLayer;
    [Header("---GROUND CHECK---")]
    public Transform groundCheck;
    public float groundCheckRadius;
    [Header("===WALL CHECK---")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;

    private void Awake()
    {
        core = GetComponent<Core>();
    }


    public bool Ground
    {
        get => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public bool WallFront
    {
        get => Physics2D.Raycast(wallCheck.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, groundLayer);
    }

    public bool WallBack
    {
        get => Physics2D.Raycast(wallCheck.position, Vector2.right * -core.Movement.FacingDirection, wallCheckDistance, groundLayer);
    }
}
