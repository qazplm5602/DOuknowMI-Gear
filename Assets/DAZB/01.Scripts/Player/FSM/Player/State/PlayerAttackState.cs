using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    private void Update() {
        if (endTriggerCalled) {
            stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }
}
