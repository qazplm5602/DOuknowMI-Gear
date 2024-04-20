using UnityEngine;
using FSM;

public class GuniAttackState : EnemyState<CommonEnemyStateEnum>
{
    public GuniAttackState(Enemy enemy, EnemyStateMachine<CommonEnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private EnemyGuni _enemyGuni;
    private Vector2 direction;

    public override void Enter() {
        base.Enter();

        _enemyGuni = _enemy as EnemyGuni;
        direction = (PlayerManager.instance.playerTrm.position - _enemyGuni.attackTransform.position).normalized;
    }

    public override void UpdateState() {
        
        if(_enemy.isDead) _stateMachine.ChangeState(CommonEnemyStateEnum.Dead);
        
        if(_triggerCalled) {
            _stateMachine.ChangeState(CommonEnemyStateEnum.Chase);
        }
    }

    public override void AnimationAttackTrigger() {
        EnemyProjectile bullet = PoolManager.Instance.Pop(PoolingType.Bullet) as EnemyProjectile;
        bullet.transform.position = _enemyGuni.attackTransform.position;
        bullet.Init(3, 6, direction, (int)_enemy.Stat.attack.GetValue());
        Debug.Log("탕!!");
    }

    public override void Exit() {
        _enemy.lastAttackTime = Time.time;

        base.Exit();
    }
}
