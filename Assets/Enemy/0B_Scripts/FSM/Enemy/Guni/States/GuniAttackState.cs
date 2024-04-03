using UnityEngine;
using FSM;

public class GuniAttackState : EnemyState<GuniStateEnum>
{
    public GuniAttackState(Enemy enemy, EnemyStateMachine<GuniStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private EnemyGuni _enemyGuni;
    private Vector2 direction;

    public override void Enter() {
        base.Enter();
        Debug.Log("Enter Attack State");

        _enemyGuni = _enemy as EnemyGuni;
        direction = (PlayerManager.instance.playerTrm.position - _enemyGuni.attackTransform.position).normalized;
    }

    public override void UpdateState() {
        
        if(_enemy.isDead) _stateMachine.ChangeState(GuniStateEnum.Dead);
        
        if(_triggerCalled) {
            _stateMachine.ChangeState(GuniStateEnum.Chase);
        }
    }

    public override void AnimationAttackTrigger() {
        GameObject bulletObject = PoolManager.Instance.Pop(PoolingType.Bullet).gameObject;
        bulletObject.transform.position = _enemyGuni.attackTransform.position;
        bulletObject.GetComponent<EnemyProjectile>().Init(direction);
        Debug.Log("탕!!");
    }

    public override void Exit() {
        _enemy.lastAttackTime = Time.time;

        base.Exit();
    }
}
