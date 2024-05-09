using UnityEngine;
using FSM;

public class MGHookState : EnemyState<MGStateEnum>
{
    public MGHookState(Enemy enemy, EnemyStateMachine<MGStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private EnemyMG _enemyMG;

    public override void Enter() {
        base.Enter();

        _enemyMG = _enemy as EnemyMG;
    }

    public override void UpdateState() {
        if(_triggerCalled) {
            _stateMachine.ChangeState(MGStateEnum.Battle);
        }
    }

    public override void Exit() {
        _enemy.lastAttackTime = Time.time;

        base.Exit();
    }

    public override void AnimationAttackTrigger() {
        _enemy.DamageCasterCompo.Damage(_enemyMG.hookDamage, (Vector2)_enemy.transform.position + _enemyMG.hookOffset * _enemy.FacingDirection, _enemyMG.hookRange);
        SoundManager.Instance.PlaySound("Magic Spell_Simple Swoosh_6");
    }
}
