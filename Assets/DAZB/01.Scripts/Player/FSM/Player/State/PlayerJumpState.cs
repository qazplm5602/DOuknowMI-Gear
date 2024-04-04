using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerCanDashState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.RigidCompo.AddForce(Vector2.up * player.jumpPower, ForceMode2D.Impulse);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        HandleMovementEvent();
        if (player.RigidCompo.velocity.y < 0f) {
            stateMachine.ChangeState(PlayerStateEnum.Fall);
        }
        else if (player.MovementCompo.isGround) {
            stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    private void HandleMovementEvent() {
        player.MovementCompo.SetMovement(player.InputReader._xMovement * player.moveSpeed);
    }
}
