using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : SingletonMonoBehaviour<CollisionController>
{
    public LayerMask groundLayer;
    public bool onGround;

    public Vector2 collisionRadius;
    public Vector2 bottomOffset;



    void Update()
    {
        onGround = Physics2D.OverlapArea((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var positions = new Vector2[] { bottomOffset };
        Gizmos.DrawWireCube((Vector2)transform.position + bottomOffset, collisionRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        {
            if (collision.gameObject.CompareTag(Const.TAG_GROUND) && InputController.Instance.horizontal == 0)
            {
                InputController.Instance.playerState = PlayerState.IDLE;
            }
            else
            {
                InputController.Instance.playerState = PlayerState.RUN;
                InputController.Instance.UpdateByDirection((float)InputController.Instance.horizontal);
            }
        }
    }
}
