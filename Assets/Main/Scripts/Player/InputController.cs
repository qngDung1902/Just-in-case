using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Lean.Gui;

public class InputController : SingletonMonoBehaviour<InputController>
{
    public float dashSpeed, timeDash;
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
    public LeanJoystick joystick;
    public GameObject slashEffect;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public PlayerForm playerForm;

    // [HideInInspector]
    public float horizontal;

    private CollisionController playerCollision;
    private Rigidbody2D rigid;
    private Vector2 tmpDir;
    private float localGravity;
    private bool onDash;
    public bool onGround => CollisionController.Instance.onGround;
    private Tween tween;

    public StateMachine movementStateMachine;
    public StandingState standing;
    public JumpingState jumping;


    public override void Awake()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        localGravity = rigid.gravityScale;

        playerForm = PlayerForm.BASIC;

    }

    private void Start()
    {
        movementStateMachine = new StateMachine();

        standing = new StandingState(this, movementStateMachine);
        jumping = new JumpingState(this, movementStateMachine);

        movementStateMachine.Initialize(standing);
    }

    private void Update()
    {
        // UpdateMovement(horizontal);
        movementStateMachine.CurrentState.HandleInput();

        movementStateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate() 
    {
        movementStateMachine.CurrentState.PhysicUpdate();
    }

    public void UpdateMovement(float speed)
    {
        horizontal = joystick.ScaledValue.x;
        rigid.velocity = new Vector2(speed * horizontal, rigid.velocity.y);
    }

    public void UpdateDirection()
    {
        if (horizontal == 0) return;

        transform.localScale = new Vector2(horizontal = horizontal > 0 ? 1 : -1, 1f);
    }

    public void Jump()
    {
        standing.jump = true;
    }

    public void ApplyJump(float jumpForce)
    {
        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.velocity += Vector2.up * jumpForce;
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

    // public void State(PlayerState state)
    // {
    //     playerState = state;
    // }

    public void TransformToDemon()
    {
        if (playerForm != PlayerForm.DEMON)
        {
            playerForm = PlayerForm.DEMON;
            ghostController.delay = 0.1f;
            animatorController.ChangeToDemonAnimator();
            GameUIManager.Instance.Active();
        }
    }
}


