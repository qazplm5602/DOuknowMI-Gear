using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : PlayerState
{
    public PlayerHurtState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.isInvincibility = true;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (endTriggerCalled) {
            stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        player.isInvincibility = false;
        base.Exit();
    }
}
