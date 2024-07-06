using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    private float moveSpeed;
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.inputManager.JumpEvent += OnJump;
        player.inputManager.AttackEvent += OnAttack;
        player.inputManager.BlockEvent += OnBlock;
    }

    public override void Update()
    {
        base.Update();

        if(!player.isGroundDetected)
        {
            stateMachine.ChangeState(player.airState);
        }

    }

    public override void Exit()
    {
        base.Exit();

        player.inputManager.JumpEvent -= OnJump;
        player.inputManager.AttackEvent -= OnAttack;
        player.inputManager.BlockEvent -= OnBlock;
    }

    public void OnJump()
    {
        stateMachine.ChangeState(player.jumpState);
    }

    public void OnAttack()
    {
        stateMachine.ChangeState(player.primaryAttackState);
    }

    public void OnBlock()
    {
        stateMachine.ChangeState(player.blockState);
    }

}
