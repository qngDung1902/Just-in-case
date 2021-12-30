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
    public GameObject slashEffect;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public PlayerForm playerForm;

    // [HideInInspector]
    public int horizontal;


    private CollisionController playerCollision;
    private Rigidbody2D rigid;

    private Vector2 tmpDir;

    private float localGravity;

    private bool onDash;
    public bool onGround => CollisionController.Instance.onGround;

    private Tween tween;
    public override void Awake()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        localGravity = rigid.gravityScale;

        state = PlayerState.IDLE;
        playerForm = PlayerForm.BASIC;

    }

    private void Update()
    {
        UpdateMovement(horizontal);
    }

    private void UpdateMovement(int direction)
    {
        if (playerState != PlayerState.DASH)
        {
            rigid.velocity = new Vector2(speed * direction, rigid.velocity.y);
        }
    }

    public void Move(int direction)
    {
        if (playerState != PlayerState.DASH)
        {
            playerState = PlayerState.RUN;
            transform.localScale = new Vector2(direction, 1f);
            horizontal = direction;
            rigid.gravityScale = localGravity;
        }
    }

    public void Stop()
    {
        if (playerState != PlayerState.DASH)
        {
            playerState = PlayerState.IDLE;
        }
        rigid.velocity = Vector2.zero;
        rigid.gravityScale = localGravity;
        horizontal = 0;
    }

    public void Jump()
    {
        if (onGround)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
            rigid.velocity += Vector2.up * jumpForce;
            playerState = PlayerState.FALLING;
        }
    }

    public void Flick(Vector2 direction)
    {
        tmpDir = direction;
        rigid.gravityScale = 0;
        rigid.velocity = Vector2.zero;
        rigid.velocity += direction * dashSpeed;
        playerState = PlayerState.DASH;

        UpdateByDirection(direction.x);
        float angleRad = Mathf.Atan2(direction.y, direction.x);
        float angleDeg = (180 / Mathf.PI) * angleRad;
        if (transform.localScale.x >= 0)
        {
            slashEffect.transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);
            slashEffect.SetActive(true);
        }
        else
        {
            slashEffect.transform.rotation = Quaternion.Euler(0f, 0f, angleDeg + 180f);
            slashEffect.SetActive(true);
        }
    }

    public void UpdateByDirection(float hozirontalDirection)
    {
        if (hozirontalDirection == 0) return;

        transform.localScale = new Vector2(hozirontalDirection = hozirontalDirection > 0 ? 1 : -1, 1f);
    }

    public void CompleteDash()
    {
        rigid.velocity = Vector2.zero;
        rigid.gravityScale = localGravity;
        UpdateOnCompletedDash();
    }

    public void UpdateOnCompletedDash()
    {
        if (!onGround)
        {
            animatorController.Play("player_demon_backtofall");
            UpdateByDirection(horizontal);
            playerState = PlayerState.FALLING;
        }
        else if (horizontal != 0)
        {
            animatorController.Play("player_demon_baktorun");
            UpdateByDirection(horizontal);
        }
        else if (horizontal == 0)
        {
            animatorController.Play("player_demon_baktoidle");
        }
    }

    public void HitEffect(Vector2 hitPosition)
    {
        Time.timeScale = 0;
        StartCoroutine(IEnumHitEffect(hitPosition));
    }

    private IEnumerator IEnumHitEffect(Vector2 hitPosition)
    {
        yield return new WaitForSecondsRealtime(0.01f);
        EffectManager.Instance.SpawnHitEffect(hitPosition, tmpDir);
        Camera.main.DOShakeRotation(0.2f, 1, 30, 25, true).SetUpdate(true).OnComplete(() => Time.timeScale = 1);
    }

    public void State(PlayerState state)
    {
        playerState = state;
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
            Stop();
            playerForm = PlayerForm.DEMON;
            ghostController.delay = 0.1f;
            animatorController.ChangeToDemonAnimator();
            GameUIManager.Instance.Active();
        }
    }
}


