using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InputController : SingletonMonoBehaviour<InputController>
{
    public float speed, dashSpeed;
    public float jumpForce;
    public PlayerState state;
    public PlayerState playerState
    {
        get
        {
            return state;
        }
        set
        {
            state = value;
            animatorController.UpdateAnimationState(state);
        }
    }
    public AnimatorController animatorController;

    [HideInInspector] public SpriteRenderer spriteRenderer;
    private Rigidbody2D rigid;
    private CollisionController playerCollision;
    [HideInInspector] public float horizontal;
    private bool onDash;
    public bool onGround => CollisionController.Instance.onGround;

    private Tween tween;
    public override void Awake()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        state = PlayerState.IDLE;

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
        if (onDash)
        {
            return;
        }
        if (playerState != PlayerState.RUN && onGround)
        {
            playerState = PlayerState.RUN;
        }

        transform.localScale = new Vector2(-1f, 1f);
        horizontal = -1;
    }

    public void MoveRight()
    {
        if (onDash)
        {
            return;
        }
        if (playerState != PlayerState.RUN && onGround)
        {
            playerState = PlayerState.RUN;
        }
        transform.localScale = new Vector2(1f, 1f);

        horizontal = 1;
    }

    public void Stop()
    {
        if (onDash)
        {
            return;
        }
        if (state != PlayerState.IDLE && onGround)
        {
            playerState = PlayerState.IDLE;
        }

        horizontal = 0;
    }

    public void Dash(Vector2 direction)
    {
        if (!onDash && onGround)
        {
            playerState = PlayerState.DASH;
            tween.Kill(false);
            rigid.gravityScale = 0;
            onDash = true;
            rigid.velocity = Vector2.zero;
            transform.DOScale(Vector2.one, 0.1f).OnStart(() =>
            {
                rigid.AddForce(direction * dashSpeed, ForceMode2D.Impulse);
            }).OnComplete(() =>
            {
                rigid.velocity = Vector2.zero;
                tween = DOTween.To(() => rigid.gravityScale, x => rigid.gravityScale = x, 3.2f, 2f);
                onDash = false;

                playerState = PlayerState.FALLING;
            });
        }
    }
}


