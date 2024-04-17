using UnityEngine;
using FSM;
using Unity.VisualScripting;

public class LJ_KDeadState : EnemyState<LJ_KStateEnum>
{
    public LJ_KDeadState(Enemy enemy, EnemyStateMachine<LJ_KStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    public override void Enter() {
        base.Enter();
        
        _enemy.StopImmediately(true);
        _enemy.ColliderCompo.enabled = false;
        _enemy.RigidbodyCompo.gravityScale = 0;

        _enemy.isDead = true;
    }

    public override void UpdateState() {
        if(_triggerCalled) {
            _enemy.StartDelayCallback(0.5f, Dead);
        }
    }

    private void Dead() {
        _enemy.SetDead();
        GameObject.Destroy(_enemy.gameObject);
    }
}
