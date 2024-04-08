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
        player.InputReader.AttackEvent += HandleAttackEvent;
    }
    
    public override void Exit() {
        player.InputReader.DashEvent -= HadleDashEvent;
        player.InputReader.AttackEvent -= HandleAttackEvent;
        base.Exit();
    }

    private void HandleAttackEvent() {
        bool cooldownPass = player.lastAttackTime + player.speed <= Time.time;
        if (cooldownPass) {
            //stateMachine.ChangeState(PlayerStateEnum.Attack);
        }
    }

    private void HadleDashEvent() {
        stateMachine.ChangeState(PlayerStateEnum.Dash);
    }
}
