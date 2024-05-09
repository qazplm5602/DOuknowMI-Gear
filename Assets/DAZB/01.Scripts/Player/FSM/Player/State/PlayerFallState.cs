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
        if (DialogueManager.Instance == null) {
            if (player.MovementCompo.CanUnderJump() && player.isUnderJumpping) {
                return;
            }
            if (player.MovementCompo.isGround) {
                PoolManager.Instance.Pop(PoolingType.PlayerLanding);
                player.gameObject.layer = LayerMask.NameToLayer("Player");
                SoundManager.Instance.PlaySound(SoundType.PlayerLanding);
                stateMachine.ChangeState(PlayerStateEnum.Idle);
                /* player.isUnderJumpping = false; */
            }
        }
        else {
            if (player.MovementCompo.CanUnderJump() && player.isUnderJumpping) {
                return;
            }
            if (player.MovementCompo.isGround || DialogueManager.Instance.isEnd == false) {
                PoolManager.Instance.Pop(PoolingType.PlayerLanding);
                player.gameObject.layer = LayerMask.NameToLayer("Player");
                SoundManager.Instance.PlaySound(SoundType.PlayerLanding);
                stateMachine.ChangeState(PlayerStateEnum.Idle);
                /* player.isUnderJumpping = false; */
            }
        }
    }

    private void HandleMovementEvent() {
        player.MovementCompo.SetMovement(player.InputReader._xMovement * player.moveSpeed, true);
    }
} 
