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
        coroutine = player.StartCoroutine(GenarateSmokeRoutine());
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Vector2 velocity = movementDirection;
        player.MovementCompo.SetMovement(velocity * player.moveSpeed, true);
        HandleMovementEvent();

    }

    public override void Exit()
    {
        player.StopCoroutine(coroutine);
        base.Exit();
    }

    private void HandleMovementEvent() {
        if (player.InputReader._xMovement.sqrMagnitude < Mathf.Epsilon) {
            player.StateMachine.ChangeState(PlayerStateEnum.Idle);
        }
        else {
            movementDirection = player.InputReader._xMovement.normalized;
        }
    }

    private IEnumerator GenarateSmokeRoutine() {
        float currentTime = 0.0f;
        float delay = 0.2f;
        PoolManager.Instance.Pop(PoolingType.MoveStartSmoke);
        while (true) {
            currentTime += Time.deltaTime;
            if (currentTime >= delay) {
                currentTime = 0;
                PoolManager.Instance.Pop(PoolingType.MoveSmoke);
            }
            yield return null;
        }
    }
}
