using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerCanDashState
{
    private Vector2 movementDirection;
    private Coroutine coroutine;
    public PlayerRunState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.StartDelayCallback(0.05f, () => PoolManager.Instance.Pop(PoolingType.MoveStartSmoke));
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Vector2 velocity = movementDirection;
        player.MovementCompo.SetMovement(velocity * player.moveSpeed, true);
        HandleMovementEvent();

    }

    private void HandleMovementEvent() {
        if (Time.timeScale == 0) return;
        if (player.InputReader._xMovement.sqrMagnitude < Mathf.Epsilon) {
            player.StateMachine.ChangeState(PlayerStateEnum.Idle);
        }
        else {
            movementDirection = player.InputReader._xMovement.normalized;
        }
    }
}
