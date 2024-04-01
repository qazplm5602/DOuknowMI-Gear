using FSM;

public class CommonEnemyDeadState : EnemyState<CommonEnemyStateEnum>
{
    public CommonEnemyDeadState(Enemy enemy, EnemyStateMachine<CommonEnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    public override void Enter() {
        base.Enter();

        _enemy.StopImmediately(false);

        // 부품 생성
    }
}
