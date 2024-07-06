using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHangingState : PlayerState
{
    public PlayerHangingState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.inputManager.JumpEvent += OnJump;

        player.SetZeroVelocity();
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
    }

    public override void Update()
    {
        base.Update();

        if(xInput != 0 && xInput != player.facingDir)
            stateMachine.ChangeState(player.airState);

        if (!player.isLedgeDetected)
            stateMachine.ChangeState(player.airState);

        if(yInput == -1)
        {
            stateMachine.ChangeState(player.airState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.inputManager.JumpEvent -= OnJump;

        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        player.SetVelcocity(xInput,-1f);
    }

    public void OnJump()
    {
        stateMachine.ChangeState(player.jumpState);
    }
}
