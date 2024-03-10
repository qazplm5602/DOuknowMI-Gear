using UnityEngine;
using FSM;

public class DemonSlimeIdleState : DemonSlimeGroundState
{
    public DemonSlimeIdleState(Enemy enemy, EnemyStateMachine<DemonSlimeStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private Coroutine _delayCoroutine = null;

    public override void Enter() {
        base.Enter();

        if(_delayCoroutine != null) _enemy.StopCoroutine(_delayCoroutine);
        _enemy.StopImmediately(false);

        _delayCoroutine = _enemy.StartDelayCallback(_enemy.idleTime, () => _stateMachine.ChangeState(DemonSlimeStateEnum.Move));
    }

    public override void Exit() {
        if(_delayCoroutine != null) _enemy.StopCoroutine(_delayCoroutine);

        base.Exit();
    }
}
