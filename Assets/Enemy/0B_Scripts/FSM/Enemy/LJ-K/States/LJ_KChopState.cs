using UnityEngine;
using FSM;

public class LJ_KChopState : EnemyState<LJ_KStateEnum>
{
    public LJ_KChopState(Enemy enemy, EnemyStateMachine<LJ_KStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private EnemyLJ_K _enemyLJ_K;

    public override void Enter() {
        base.Enter();

        _enemyLJ_K = _enemy as EnemyLJ_K;

        _enemyLJ_K.currentAttackRange = _enemyLJ_K.chopRange;
        _enemyLJ_K.currentAttackOffset = _enemyLJ_K.chopOffset;

        _enemy.DamageCasterCompo.Damage(_enemyLJ_K.chopDamage, (Vector2)_enemy.transform.position + _enemyLJ_K.chopOffset, _enemyLJ_K.chopRange);
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
