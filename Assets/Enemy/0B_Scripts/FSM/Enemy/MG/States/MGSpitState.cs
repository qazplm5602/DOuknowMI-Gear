using UnityEngine;
using FSM;

public class MGSpitState : EnemyState<MGStateEnum>
{
    public MGSpitState(Enemy enemy, EnemyStateMachine<MGStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    public override void Enter() {
        base.Enter();

        _enemy.StopImmediately(false);
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
        Vector2 direction = PlayerManager.instance.playerTrm.position - _enemy.transform.position;

        EnemyProjectile obj = PoolManager.Instance.Pop(PoolingType.MGBullet) as MGBullet;
        obj.transform.position = (_enemy as EnemyMG).attackTransform.position;
        obj.Init(15, 8, direction);
    }
}
