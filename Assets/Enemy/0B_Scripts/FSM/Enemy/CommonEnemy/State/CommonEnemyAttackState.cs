using UnityEngine;
using FSM;

public class CommonEnemyAttackState : EnemyState<CommonEnemyStateEnum>
{
    public CommonEnemyAttackState(Enemy enemy, EnemyStateMachine<CommonEnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    public override void UpdateState() {
        
        if(_enemy.isDead) _stateMachine.ChangeState(CommonEnemyStateEnum.Dead);
        
        if(_triggerCalled) {
            _stateMachine.ChangeState(CommonEnemyStateEnum.Chase);
        }
    }

    public override void AnimationAttackTrigger() {
        _enemy.DamageCasterCompo.Damage(_enemy.attackDamage, (Vector2)_enemy.transform.position + _enemy.attackOffset * _enemy.FacingDirection, _enemy.attackRange);
    }

    public override void Exit() {
        _enemy.lastAttackTime = Time.time;

        base.Exit();
    }
}
