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
        player.MovementCompo.StopImmediately();
        SoundManager.Instance.PlaySound(SoundType.PlayerTakeDamage);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (endTriggerCalled) {
            stateMachine.ChangeState(PlayerStateEnum.Idle);
            Debug.Log("hurt 상태 나옴");
        }
    }

    public override void Exit()
    {
        player.StartDelayCallback(1.5f, () => player.isInvincibility = false);
        base.Exit();
    }
}
