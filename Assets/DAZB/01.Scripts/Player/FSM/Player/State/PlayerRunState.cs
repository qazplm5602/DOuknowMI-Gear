using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerGroundState
{
    private Vector2 movementDirection;
    public PlayerRunState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }


    public override void UpdateState()
    {
        base.UpdateState();
        Vector2 velocity = movementDirection;
        player.MovementCompo.SetMovement(velocity * player.moveSpeed);
        HandleMovementEvent();

    }
    private void HandleMovementEvent() {
        if (player.InputReader._xMovement.sqrMagnitude < Mathf.Epsilon) {
            player.StateMachine.ChangeState(PlayerStateEnum.Idle);
        }
        else {
            movementDirection = player.InputReader._xMovement.normalized;
        }
    }
}
