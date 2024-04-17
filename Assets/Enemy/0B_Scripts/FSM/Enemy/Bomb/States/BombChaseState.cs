using UnityEngine;
using FSM;

public class BombChaseState : EnemyState<CommonEnemyStateEnum>
{
    public BombChaseState(Enemy enemy, EnemyStateMachine<CommonEnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private Transform _playerTrm;

    public override void Enter() {
        base.Enter();

        _playerTrm = PlayerManager.instance.playerTrm;
    }

    public override void UpdateState() {
        Vector2 direction = _playerTrm.position - _enemy.transform.position;
        if(direction.magnitude <= _enemy.nearDistance) {
            if(_enemy.CanAttack()) {
                _stateMachine.ChangeState(CommonEnemyStateEnum.Attack);
            }
        }
        else {
            _enemy.SetVelocity(Mathf.Sign(direction.x) * _enemy.moveSpeed, _rigidbody.velocity.y);
        }
    }
}
