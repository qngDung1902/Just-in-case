using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : MonoBehaviour {
    [Header("---GROUND CHECK---")]
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;

    public bool Ground {
        get => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
}
