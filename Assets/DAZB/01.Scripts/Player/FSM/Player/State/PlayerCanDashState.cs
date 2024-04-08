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
    }
    
    public override void Exit() {
        player.InputReader.DashEvent -= HadleDashEvent;
        base.Exit();
    }

    private void HadleDashEvent() {
        stateMachine.ChangeState(PlayerStateEnum.Dash);
    }
}
