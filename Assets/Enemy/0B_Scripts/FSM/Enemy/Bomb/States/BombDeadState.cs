using FSM;

public class BombDeadState : EnemyState<CommonEnemyStateEnum>
{
    public BombDeadState(Enemy enemy, EnemyStateMachine<CommonEnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    public override void Enter() {
        base.Enter();

        _enemy.StopImmediately(false);

    }
}
