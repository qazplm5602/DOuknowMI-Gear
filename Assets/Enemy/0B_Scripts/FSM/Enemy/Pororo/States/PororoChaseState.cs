using UnityEngine;
using FSM;

public class PororoChaseState : EnemyState<CommonEnemyStateEnum>
{
    public PororoChaseState(Enemy enemy, EnemyStateMachine<CommonEnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private Transform _playerTrm;

    public override void Enter() {
        base.Enter();

        _playerTrm = PlayerManager.instance.playerTrm;
        _enemy.FlipController(_playerTrm.position.x - _enemy.transform.position.x);
    }

    public override void UpdateState() {
        if(_enemy.isDead) _stateMachine.ChangeState(CommonEnemyStateEnum.Dead);

        if(_enemy.IsPlayerDetected(_enemy.attackOffset, _enemy.attackRange)) {
            if(_enemy.CanAttack()) {
                _stateMachine.ChangeState(CommonEnemyStateEnum.Attack);
                return;
            }
        }
        if(Vector2.Distance(_playerTrm.position, _enemy.transform.position) > _enemy.nearDistance) Move();
        else {
            _enemy.FlipController(_playerTrm.position.x - _enemy.transform.position.x);
            _enemy.StopImmediately(true);
        }
    }

    private void Move() {
        Vector2 direction = _playerTrm.position - _enemy.transform.position;
        direction.Normalize();
        direction *= _enemy.moveSpeed;
        _enemy.SetVelocity(direction.x, direction.y);
    }
}
