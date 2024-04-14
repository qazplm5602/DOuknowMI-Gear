using FSM;

public class LJ_KDeadState : EnemyState<LJ_KStateEnum>
{
    public LJ_KDeadState(Enemy enemy, EnemyStateMachine<LJ_KStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    public override void Enter() {
        base.Enter();
        
        _enemy.StopImmediately(false);
    }
}
