using UnityEngine;
using FSM;

public class DemonSlimeAttackState : DemonSlimeAtkState
{
    public DemonSlimeAttackState(Enemy enemy, EnemyStateMachine<DemonSlimeStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    public override void AnimationAttackTrigger() {
        _enemy.DamageCasterCompo.Damage(_enemy.attackDamage, (Vector2)_enemy.transform.position + _enemy.attackOffset * _enemy.FacingDirection, _enemy.attackRange);
    }
}
