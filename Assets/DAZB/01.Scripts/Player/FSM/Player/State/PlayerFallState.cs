using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerCanDashState
{
    public PlayerFallState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void UpdateState() {
        base.UpdateState();
        HandleMovementEvent();
        if (player.MovementCompo.isGround || DialogueManager.instance.isEnd == false) {
            stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    private void HandleMovementEvent() {
        player.MovementCompo.SetMovement(player.InputReader._xMovement * player.moveSpeed, true);
    }
} 
