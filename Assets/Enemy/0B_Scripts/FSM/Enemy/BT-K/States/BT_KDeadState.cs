using FSM;

public class BT_KDeadState : EnemyState<CommonEnemyStateEnum>
{
    public BT_KDeadState(Enemy enemy, EnemyStateMachine<CommonEnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    public override void Enter() {
        base.Enter();
    }
}
