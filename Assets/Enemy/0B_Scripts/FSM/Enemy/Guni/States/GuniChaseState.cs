using UnityEngine;
using FSM;

public class GuniChaseState : EnemyState<GuniStateEnum>
{
    public GuniChaseState(Enemy enemy, EnemyStateMachine<GuniStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private Transform _playerTrm;

    public override void Enter() {
        base.Enter();

        _playerTrm = PlayerManager.instance.playerTrm;
        _enemy.FlipController(_playerTrm.position.x - _enemy.transform.position.x);
    }

    public override void UpdateState() {
        if(_enemy.isDead) _stateMachine.ChangeState(GuniStateEnum.Dead);

        Vector2 direction = _playerTrm.position - _enemy.transform.position;
        if(_enemy.IsPlayerDetected(_enemy.attackOffset, _enemy.attackRange) && !_enemy.IsObstacleInLine(direction.magnitude, direction.normalized)) {
            Debug.Log("Player Detected");
            _enemy.StopImmediately(false);
            if(_enemy.CanAttack()) {
                Debug.Log("Can Attack");
                _stateMachine.ChangeState(GuniStateEnum.Attack);
            }
        }
        else if(Mathf.Abs(_playerTrm.position.x - _enemy.transform.position.x) > _enemy.nearDistance) Move();
        else {
            _enemy.FlipController(_playerTrm.position.x - _enemy.transform.position.x);
            _enemy.StopImmediately(false);
        }
    }

    private void Move() {
        _enemy.SetVelocity(_enemy.moveSpeed * _enemy.FacingDirection, _rigidbody.velocity.y);
    }
}
