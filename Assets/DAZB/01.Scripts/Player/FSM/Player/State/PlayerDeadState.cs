using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    private Transform playerDeadTrm;
    public override void Enter()
    {
        base.Enter();
        player.MovementCompo.StopImmediately();
        playerDeadTrm = player.transform;
    }

    public override void UpdateState()
    {
        player.transform.position = playerDeadTrm.position;
    }
}
