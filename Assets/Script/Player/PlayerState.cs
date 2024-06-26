using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    protected Rigidbody2D rb;

    private string animBoolName;
    protected float xInput;
    protected float yInput;

    protected float stateTimer;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        player = _player;
        stateMachine = _stateMachine;
        animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        player.animator.SetBool(animBoolName, true);
        rb = player.rb;

    }

    public virtual void Exit() 
    {
        player.animator.SetBool(animBoolName, false);  
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;   

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        player.animator.SetFloat("yVelocity", rb.velocity.y);

    }

}
