using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Move info")]
    public float moveSpeed;
    public float jumpForce;


    [Header("Dash info")]
    [SerializeField]
    private float dashCooldown;
    private float dashUsageTimer;
    public float dashSpeed;
    public float dashDuration;
    public float dashDir {  get; private set; } 


    #region State
    public PlayerStateMachine stateMachine {  get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerDashState dashState { get; private set; }

    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }    


    #endregion

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "isIdle");
        moveState = new PlayerMoveState(this, stateMachine, "isMove");
        dashState = new PlayerDashState(this, stateMachine, "isDash");
        jumpState = new PlayerJumpState(this, stateMachine, "isJump");
        airState = new PlayerAirState(this, stateMachine, "isJump");


    }


    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();

        CheckForDashInput();

        
    }



    private void CheckForDashInput()
    {

        dashUsageTimer -= Time.deltaTime;
        
        if(Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0)
        {
            dashUsageTimer = dashCooldown;
            dashDir = Input.GetAxisRaw("Horizontal");

            if(dashDir == 0)
            {
                dashDir = facingDir;
            }

            stateMachine.ChangeState(dashState);

        }

    }



}
