using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCanDashState : PlayerGroundState
{
    public PlayerCanDashState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter() {
        base.Enter();
        player.InputReader.DashEvent += HadleDashEvent;
        player.InputReader.AttackEvent += HadleAttackEvent;
    }
    
    public override void Exit() {
        player.InputReader.DashEvent -= HadleDashEvent;
        player.InputReader.AttackEvent -= HadleAttackEvent;
        base.Exit();
    }

    private void HadleDashEvent() {
        stateMachine.ChangeState(PlayerStateEnum.Dash);
    }

    private void HadleAttackEvent() {
        bool coolPass = player.lastAttackTime + player.atkCool <= Time.time; // test;
        if (coolPass && !player.isAttack) {
            stateMachine.ChangeState(PlayerStateEnum.Attack);
        }
    }
}
