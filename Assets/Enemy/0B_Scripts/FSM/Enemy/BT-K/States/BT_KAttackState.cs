using UnityEngine;
using FSM;
using Unity.VisualScripting;

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
        
        if(_enemyBT_K.attackCount == _enemyBT_K.bulletCount) {
            _enemyBT_K.attackCount = 0;
            _stateMachine.ChangeState(CommonEnemyStateEnum.Chase);
        }
    }

    public override void AnimationAttackTrigger() {
        Vector2 currentDirection = (PlayerManager.instance.playerTrm.position - _enemyBT_K.attackTransform.position).normalized;
        float angle = Mathf.Acos(Vector2.Dot(direction, currentDirection)) * Mathf.Rad2Deg;
        float t = 20f / angle;
        t = Mathf.Clamp01(t);
        direction = Vector2.Lerp(direction, currentDirection, t);

        GameObject bulletObject = PoolManager.Instance.Pop(PoolingType.Bullet).gameObject;
        bulletObject.transform.position = _enemyBT_K.attackTransform.position;
        bulletObject.GetComponent<EnemyProjectile>().Init(3, 5.5f, direction, (int)_enemy.Stat.attack.GetValue());
    }

    public override void Exit() {
        _enemy.lastAttackTime = Time.time;

        base.Exit();
    }
}
