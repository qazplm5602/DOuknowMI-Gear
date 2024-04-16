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
        if(Vector2.Distance(_enemy.transform.position, _playerTrm.position) <= _enemy.nearDistance) {
            if(_enemy.CanAttack()) {
                _stateMachine.ChangeState(CommonEnemyStateEnum.Attack);
            }
        }
        else {
            _enemy.SetVelocity(_enemy.FacingDirection * _enemy.moveSpeed, _rigidbody.velocity.y);
        }
    }
}
