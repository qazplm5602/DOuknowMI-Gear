using UnityEngine;
using FSM;

public class MGDeadState : EnemyState<MGStateEnum>
{
    public MGDeadState(Enemy enemy, EnemyStateMachine<MGStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    public override void Enter() {
        base.Enter();

        _enemy.StopImmediately(false);
    }
}
