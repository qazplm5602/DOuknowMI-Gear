using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumpState : PlayerCanDashState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
        if (Keyboard.current.sKey.isPressed) {
            if (player.MovementCompo.CanUnderJump()) {
                player.isUnderJumpping = true;
                player.RigidCompo.AddForce(Vector2.up * player.jumpPower * 0.2f, ForceMode2D.Impulse);
                player.gameObject.layer = LayerMask.NameToLayer("PlayerUnderJumpping");
                player.StartDelayCallback(0.5f, () => player.isUnderJumpping = false);
                SoundManager.Instance.PlaySound(SoundType.PlayerJump);
            }
            else {
                stateMachine.ChangeState(PlayerStateEnum.Idle);
            }
        }
        else {
            player.RigidCompo.AddForce(Vector2.up * player.jumpPower, ForceMode2D.Impulse);
            SoundManager.Instance.PlaySound(SoundType.PlayerJump);
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();
        HandleMovementEvent();
        if (player.RigidCompo.velocity.y < 0f ) {
            stateMachine.ChangeState(PlayerStateEnum.Fall);
        }
        else if (player.MovementCompo.isGround) {
            stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void HandleMovementEvent() {
        player.MovementCompo.SetMovement(player.InputReader._xMovement * player.moveSpeed, true);
    }
}
