using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InputController : MonoBehaviour
{
    public float speed, dashSpeed;
    public float jumpForce;
    [HideInInspector] public float horizontal;
    private Rigidbody2D rigid;
    private bool onDash;
    private void Awake()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!onDash)
        {
            rigid.velocity = new Vector2(speed * horizontal, rigid.velocity.y);
        }
    }

    public void MoveLeft()
    {
        horizontal = -1;
    }
    public void MoveRight()
    {
        horizontal = 1;
    }

    public void Stop()
    {
        horizontal = 0;
    }

    public void Dash(Vector2 direction)
    {
        if (!onDash)
        {
            rigid.gravityScale = 0;
            onDash = true;
            rigid.velocity = Vector2.zero;
            transform.DOScale(Vector2.one, 0.075f).OnStart(() =>
            {
                rigid.AddForce(direction * dashSpeed, ForceMode2D.Impulse);
            }).OnComplete(() =>
            {
                rigid.velocity = Vector2.zero;
                rigid.gravityScale = 3.2f;
                onDash = false;
            });
        }
    }
}
