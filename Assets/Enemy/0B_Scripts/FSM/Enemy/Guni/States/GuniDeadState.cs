using FSM;

public class GuniDeadState : EnemyState<CommonEnemyStateEnum>
{
    public GuniDeadState(Enemy enemy, EnemyStateMachine<CommonEnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    public override void Enter() {
        base.Enter();

        _enemy.StopImmediately(false);
    }
}
