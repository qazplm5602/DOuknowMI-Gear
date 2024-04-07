using UnityEngine;
using FSM;

public class DemonSlimeAtkState : EnemyState<DemonSlimeStateEnum>
{
    public DemonSlimeAtkState(Enemy enemy, EnemyStateMachine<DemonSlimeStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    public override void UpdateState() {
        base.UpdateState();

        if(_triggerCalled) {
            _stateMachine.ChangeState(DemonSlimeStateEnum.Battle);
        }
    }

    public override void Exit() {
        _enemy.lastAttackTime = Time.time;

        base.Exit();
    }
}
