using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerCanDashState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.MovementCompo.StopImmediately();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (player.InputReader._xMovement.sqrMagnitude > 0.05f) {
            stateMachine.ChangeState(PlayerStateEnum.Run);
        }
    }
}
