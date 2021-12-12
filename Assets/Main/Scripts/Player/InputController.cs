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
        Dash(Vector2.down);
    }

    private void FixedUpdate()
    {
        if (!onDash)
        {
            rigid.velocity = new Vector2(speed * horizontal, rigid.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
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

        horizontal = -1;
        transform.localScale = new Vector2(-1f, 1f);
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
        horizontal = 1;
        transform.localScale = new Vector2(1f, 1f);

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

    public void UpdateByDirection(Vector2 direction)
    {
        if (direction.x < 0)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        else if (direction.x > 0)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
    }

    public void Dash(Vector2 direction)
    {
        if (!onDash && onGround)
        {
            onDash = true;
            playerState = PlayerState.DASH;
            rigid.gravityScale = 0;
            rigid.velocity = Vector2.zero;
            UpdateByDirection(direction);
            tween.Kill(false);
            transform.DOScaleZ(1f, 0.1f).OnStart(() =>
            {
                rigid.AddForce(direction * dashSpeed, ForceMode2D.Impulse);
            }).OnComplete(() =>
            {
                rigid.velocity = Vector2.zero;
                if (horizontal != 0)
                {
                    transform.localScale = new Vector2(horizontal, horizontal);
                }
                tween = DOTween.To(() => rigid.gravityScale, x => rigid.gravityScale = x, 3.2f, 2f);
                onDash = false;

                playerState = PlayerState.FALLING;
            });
        }
    }
}


