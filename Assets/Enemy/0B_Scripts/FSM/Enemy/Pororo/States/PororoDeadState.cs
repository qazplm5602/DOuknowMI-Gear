using UnityEngine;
using FSM;

public class PororoDeadState : EnemyState<CommonEnemyStateEnum>
{
    public PororoDeadState(Enemy enemy, EnemyStateMachine<CommonEnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    public override void Enter() {
        base.Enter();

        _enemy.StopImmediately(true);
    }
}
