using UnityEngine;
using FSM;

public class BT_KAttackState : EnemyState<CommonEnemyStateEnum>
{
    public BT_KAttackState(Enemy enemy, EnemyStateMachine<CommonEnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private EnemyBT_K _enemyBT_K;
    private Vector2 direction;

    public override void Enter() {
        base.Enter();

        _enemyBT_K = _enemy as EnemyBT_K;
        direction = (PlayerManager.instance.playerTrm.position - _enemyBT_K.attackTransform.position).normalized;
    }

    public override void UpdateState() {
        
        if(_enemy.isDead) _stateMachine.ChangeState(CommonEnemyStateEnum.Dead);
        
        if(_triggerCalled) {
            _stateMachine.ChangeState(CommonEnemyStateEnum.Chase);
        }
    }

    public override void AnimationAttackTrigger() {
        GameObject bulletObject = PoolManager.Instance.Pop(PoolingType.Bullet).gameObject;
        bulletObject.transform.position = _enemyBT_K.attackTransform.position;
        bulletObject.GetComponent<EnemyProjectile>().Init(3, 5.5f, direction);
        Debug.Log("탕!!");
    }

    public override void Exit() {
        _enemy.lastAttackTime = Time.time;

        base.Exit();
    }
}