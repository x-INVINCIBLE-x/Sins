using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : PlayerState
{
    public PlayerBlockState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.inputManager.BlockEvent += CancelBlock;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();

        player.inputManager.BlockEvent -= CancelBlock;
    }

    public void CancelBlock()
    {
        stateMachine.ChangeState(player.idleState);
    }

}
