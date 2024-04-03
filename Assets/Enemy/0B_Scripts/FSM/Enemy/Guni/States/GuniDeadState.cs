using UnityEngine;
using FSM;

public class GuniDeadState : EnemyState<GuniStateEnum>
{
    public GuniDeadState(Enemy enemy, EnemyStateMachine<GuniStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    public override void Enter() {
        base.Enter();

        _enemy.StopImmediately(false);
    }
}
