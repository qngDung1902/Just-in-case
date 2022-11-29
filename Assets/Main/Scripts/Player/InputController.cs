using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Lean.Gui;

public class InputController : SingletonMonoBehaviour<InputController> {
    public float dashSpeed, timeDash;
    public AnimatorController animatorController;
    public LeanJoystick joystick;
    public GameObject slashEffect;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public PlayerForm playerForm;
    private CollisionController playerCollision;

    // [HideInInspector]
    public float horizontal;

    private Rigidbody2D rigid;
    private Vector2 t_direction;
    private float localGravity;
    public bool onGround => playerCollision.onGround;
    private Tween tween;

    public StateMachine movementStateMachine;
    public GroundedState groundState;
    public StandingState idleState;
    public JumpingState jumpState;
    public RunningState runState;


    public override void Awake() {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        localGravity = rigid.gravityScale;
        playerForm = PlayerForm.BASIC;

    }

    private void Start() {
        movementStateMachine = new StateMachine();

        groundState = new GroundedState(this, movementStateMachine);
        idleState = new StandingState(this, movementStateMachine);
        jumpState = new JumpingState(this, movementStateMachine);
        runState = new RunningState(this, movementStateMachine);

        movementStateMachine.Initialize(idleState);
    }

    private void Update() {
        movementStateMachine.CurrentState.HandleInput();
        movementStateMachine.CurrentState.LogicUpdate();

        if (Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }
    }

    private void FixedUpdate() {
        movementStateMachine.CurrentState.PhysicUpdate();
    }

    public void UpdateMovement(float speed) {
        horizontal = joystick.ScaledValue.x;
        rigid.velocity = new Vector2(speed * horizontal, rigid.velocity.y);
    }

    public void UpdateDirection() {
        if (horizontal == 0) return;

        transform.localScale = new Vector2(horizontal = horizontal > 0 ? 1 : -1, 1f);
    }

    public void Jump() {
        movementStateMachine.CurrentState.jump = true;
    }

    public void ApplyJump(float jumpForce) {
        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.velocity += Vector2.up * jumpForce;
    }

    public void Flick(Vector2 direction) {
        t_direction = direction;
        rigid.gravityScale = 0;
        rigid.velocity = Vector2.zero;
        rigid.velocity += direction * dashSpeed;
        // playerState = PlayerState.DASH;

        UpdateByDirection(direction.x);
        float angleRad = Mathf.Atan2(direction.y, direction.x);
        float angleDeg = (180 / Mathf.PI) * angleRad;
        if (transform.localScale.x >= 0) {
            slashEffect.transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);
            slashEffect.SetActive(true);
        } else {
            slashEffect.transform.rotation = Quaternion.Euler(0f, 0f, angleDeg + 180f);
            slashEffect.SetActive(true);
        }
    }

    public void UpdateByDirection(float hozirontalDirection) {
        if (hozirontalDirection == 0) return;

        transform.localScale = new Vector2(hozirontalDirection = hozirontalDirection > 0 ? 1 : -1, 1f);
    }

    public void CompleteDash() {
        rigid.velocity = Vector2.zero;
        rigid.gravityScale = localGravity;
        UpdateOnCompletedDash();
    }

    public void UpdateOnCompletedDash() {
        if (!onGround) {
            animatorController.Play("player_demon_backtofall");
            UpdateByDirection(horizontal);
            // playerState = PlayerState.FALLING;
        } else if (horizontal != 0) {
            animatorController.Play("player_demon_baktorun");
            UpdateByDirection(horizontal);
        } else if (horizontal == 0) {
            animatorController.Play("player_demon_baktoidle");
        }
    }

    public void HitEffect(Vector2 hitPosition) {
        Time.timeScale = 0;
        StartCoroutine(IEnumHitEffect(hitPosition));
    }

    private IEnumerator IEnumHitEffect(Vector2 hitPosition) {
        yield return new WaitForSecondsRealtime(0.01f);
        EffectManager.Instance.SpawnHitEffect(hitPosition, t_direction);
        Camera.main.DOShakeRotation(0.2f, 1, 30, 25, true).SetUpdate(true).OnComplete(() => Time.timeScale = 1);
    }

    public void TransformToDemon() {
        if (playerForm != PlayerForm.DEMON) {
            playerForm = PlayerForm.DEMON;
            animatorController.ChangeToDemonAnimator();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(Const.TAG_GROUND)) {
            movementStateMachine.CurrentState.ground = true;
        }
    }
}


