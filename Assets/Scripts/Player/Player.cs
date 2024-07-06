using UnityEngine;

public class Player : Entity
{
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerBlockState blockState { get; private set; }
    public PlayerHangingState hangingState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerCrouchState crouchState { get; private set; }
    public PlayerCrouchDashState crouchDashState { get; private set; }

    public PlayerPrimaryAttackState primaryAttackState { get; private set; }

    public PlayerStateMachine stateMachine { get; private set; }


    [SerializeField] private LayerMask ledgeLayer;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private float ledgeCheckDistance;

    [Header("Animator info")]
    public RuntimeAnimatorController defaultAnimator;
    public AnimatorOverrideController fireSwordAnimator;

    [Header("Move info")]
    public float moveSpeed;

    [Header("Jump info")]
    public float jumpForce;

    [Header("Attack Info")]
    public Vector2[] attackForce;

    [Header("DashInfo")]
    public float dashSpeed;
    public float dashDuration;
    public float crouchDashSpeed;
    public float crouchDashDuration;
    public float crouchDashCooldown = 0.12f;

    [Header("FireSword")]
    public float fireSwordCooldown;
    [HideInInspector] public float fireSwordTimer;


    [Header("Colliders")]
    public Collider2D defaultCollider;
    public Collider2D crouchCollider;

    //private SkillManager skillManager;
    public bool isBusy;

    protected override void Start()
    {
        base.Start();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Air");
        airState = new PlayerAirState(this, stateMachine, "Air");
        blockState = new PlayerBlockState(this, stateMachine, "Block");
        hangingState = new PlayerHangingState(this, stateMachine, "Hang");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        crouchState = new PlayerCrouchState(this, stateMachine, "Crouch");
        crouchDashState = new PlayerCrouchDashState(this, stateMachine, "CrouchDash");

        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "PrimaryAttack");

        stateMachine.Initialize(idleState);
        //skillManager = SkillManager.instance;
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();

        if (!isBusy)
            CheckAbilities();
    }

    private void CheckAbilities()
    {
        //if (Input.GetKeyDown(KeyCode.LeftShift) && skillManager.dash.CanUseSkill())
        //{
        //    stateMachine.ChangeState(dashState);
        //}
    }

    public void SetFireSwordController() => animator.runtimeAnimatorController = fireSwordAnimator;
    public void SetDefaultController()
    {
        PlayerState currentState = stateMachine.currentState;
        animator.runtimeAnimatorController = defaultAnimator;
        stateMachine.ChangeState(currentState);
    }

    public bool isLedgeDetected => Physics2D.Raycast(ledgeCheck.position, Vector3.right * facingDir, ledgeCheckDistance, ledgeLayer);
    public void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(ledgeCheck.position, new Vector3((ledgeCheck.position.x + (ledgeCheckDistance * facingDir)), ledgeCheck.position.y));
    }
}
