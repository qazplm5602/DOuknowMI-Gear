using UnityEngine;
using FSM;

public class LJ_KEarthquakeState : EnemyState<LJ_KStateEnum>
{
    public LJ_KEarthquakeState(Enemy enemy, EnemyStateMachine<LJ_KStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    public override void Enter() {
        base.Enter();

        _enemy.StopImmediately(false);
    }

    public override void UpdateState() {
        if(_triggerCalled) {
            _stateMachine.ChangeState(LJ_KStateEnum.Battle);
        }
    }

    public override void Exit() {
        _enemy.lastAttackTime = Time.time;

        base.Exit();
    }
}
