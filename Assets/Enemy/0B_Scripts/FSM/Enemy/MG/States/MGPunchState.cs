using UnityEngine;
using FSM;

public class MGPunchState : EnemyState<MGStateEnum>
{
    public MGPunchState(Enemy enemy, EnemyStateMachine<MGStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private EnemyMG _enemyMG;

    public override void Enter() {
        base.Enter();

        _enemyMG = _enemy as EnemyMG;
        _enemy.StopImmediately(false);
    }

    public override void UpdateState() {
        if(_triggerCalled) {
            _stateMachine.ChangeState(MGStateEnum.Hook);
        }
    }

    public override void AnimationAttackTrigger() {
        _enemy.DamageCasterCompo.Damage(_enemyMG.punchDamage, (Vector2)_enemy.transform.position + _enemyMG.punchOffset * _enemy.FacingDirection, _enemyMG.punchRange);
    }
}
