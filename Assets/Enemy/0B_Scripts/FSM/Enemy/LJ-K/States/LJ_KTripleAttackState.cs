using UnityEngine;
using FSM;

public class LJ_KTripleAttackState : EnemyState<LJ_KStateEnum>
{
    public LJ_KTripleAttackState(Enemy enemy, EnemyStateMachine<LJ_KStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private EnemyLJ_K _enemyLJ_K;

    public override void Enter() {
        base.Enter();

        _enemyLJ_K = _enemy as EnemyLJ_K;

        _enemyLJ_K.currentAttackDamage = _enemyLJ_K.tripleAttackDamage;
        _enemyLJ_K.currentAttackRange = _enemyLJ_K.doubleAttack1Range;
        _enemyLJ_K.currentAttackOffset = _enemyLJ_K.doubleAttack1Offset;

        _enemy.StopImmediately(false);
    }

    public override void UpdateState() {
        if(_triggerCalled) {
            _stateMachine.ChangeState(LJ_KStateEnum.Chop);
        }
    }

    public override void Exit() {
        base.Exit();
    }
}
