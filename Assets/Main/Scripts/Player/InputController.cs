using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InputController : SingletonMonoBehaviour<InputController>
{
    public float speed, dashSpeed, timeDash;
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
    public GhostController ghostController;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public float horizontal;
    [HideInInspector] public PlayerForm playerForm;
    private CollisionController playerCollision;
    private Rigidbody2D rigid;

    private bool onDash;
    public bool onGround => CollisionController.Instance.onGround;

    private Tween tween;
    public override void Awake()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        state = PlayerState.IDLE;
        playerForm = PlayerForm.BASIC;
    }


    public void MoveLeft()
    {
        if (onGround)
        {
            playerState = PlayerState.RUN;
        }
        rigid.velocity = new Vector2(speed * -1, rigid.velocity.y);
        transform.localScale = new Vector2(-1f, 1f);
        horizontal = -1;
    }

    public void MoveRight()
    {
        if (onGround)
        {
            playerState = PlayerState.RUN;
        }
        rigid.velocity = new Vector2(speed *  1, rigid.velocity.y);
        transform.localScale = new Vector2(1f, 1f);
        horizontal = 1;

    }

    public void Stop()
    {
        playerState = PlayerState.IDLE;
        rigid.velocity = Vector2.zero;
        horizontal = 0;
    }

    public void Jump()
    {
        if (onGround)
        {
            rigid.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            playerState = PlayerState.FALLING;
        }
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

    // public void Dash(Vector2 direction)
    // {
    //     if (playerForm == PlayerForm.DEMON)
    //     {
    //         UpdateByDirection(direction);
    //         playerState = PlayerState.DASH;
    //         return;
    //     }
    //     if (!onDash && onGround)
    //     {
    //         onDash = true;
    //         rigid.gravityScale = 0;
    //         rigid.velocity = Vector2.zero;
    //         playerState = PlayerState.DASH;
    //         UpdateByDirection(direction);
    //         tween.Kill(false);
    //         transform.DOScaleZ(1f, timeDash).OnStart(() =>
    //         {
    //             rigid.AddForce(direction * dashSpeed, ForceMode2D.Impulse);
    //         }).OnComplete(() =>
    //         {
    //             rigid.velocity = Vector2.zero;
    //             if (horizontal != 0)
    //             {
    //                 transform.localScale = new Vector2(horizontal, 1f);
    //             }
    //             tween = DOTween.To(() => rigid.gravityScale, x => rigid.gravityScale = x, 3.2f, 2f);
    //             onDash = false;

    //             if (onGround)
    //             {
    //                 if (horizontal == 0)
    //                 {
    //                     playerState = PlayerState.IDLE;
    //                 }
    //                 else
    //                 {
    //                     playerState = PlayerState.RUN;
    //                 }
    //             }
    //             else
    //             {
    //                 playerState = PlayerState.FALLING;
    //             }
    //         });
    //     }
    // }

    public void TransformToDemon()
    {
        if (playerForm != PlayerForm.DEMON)
        {
            playerForm = PlayerForm.DEMON;
            ghostController.delay = 0.1f;
            animatorController.ChangeToDemonAnimator();
        }
    }
}


