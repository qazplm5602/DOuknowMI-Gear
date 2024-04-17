using UnityEngine;
using FSM;

public class PororoAttackState : EnemyState<CommonEnemyStateEnum>
{
    public PororoAttackState(Enemy enemy, EnemyStateMachine<CommonEnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private EnemyPororo _enemyPororo;
    
    public override void Enter() {
        base.Enter();

        _enemy.StopImmediately(true);
        _enemyPororo = _enemy as EnemyPororo;
    }

    public override void UpdateState() {
        if(_enemy.isDead) _stateMachine.ChangeState(CommonEnemyStateEnum.Dead);
        
        if(_triggerCalled) {
            _stateMachine.ChangeState(CommonEnemyStateEnum.Chase);
        }
    }

    public override void AnimationAttackTrigger() {
        GameObject bulletObject = PoolManager.Instance.Pop(PoolingType.Bullet).gameObject;
        bulletObject.transform.position = _enemyPororo.attackTransform.position;

        Vector2 direction = (PlayerManager.instance.playerTrm.position - _enemyPororo.attackTransform.position).normalized;
        bulletObject.GetComponent<EnemyProjectile>().Init(10, 8f, direction, _enemy.Stat.attack.GetValue());
        Debug.Log("탕!!");
    }

    public override void Exit() {
        _enemy.lastAttackTime = Time.time;

        base.Exit();
    }
}
