using UnityEngine;
using FSM;

public class GuniChaseState : EnemyState<CommonEnemyStateEnum>
{
    public GuniChaseState(Enemy enemy, EnemyStateMachine<CommonEnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private Transform _playerTrm;

    public override void Enter() {
        base.Enter();

        _playerTrm = PlayerManager.instance.playerTrm;
        _enemy.FlipController(_playerTrm.position.x - _enemy.transform.position.x);
    }

    public override void UpdateState() {
        if(_enemy.isDead) _stateMachine.ChangeState(CommonEnemyStateEnum.Dead);

        Vector2 direction = _playerTrm.position - _enemy.transform.position;
        if(_enemy.IsPlayerDetected(_enemy.attackOffset, _enemy.attackRange) && !_enemy.IsObstacleInLine(direction.magnitude, direction.normalized)) {
            _enemy.StopImmediately(false);
            if(_enemy.CanAttack()) {
                _stateMachine.ChangeState(CommonEnemyStateEnum.Attack);
            }
        }
        else if(Mathf.Abs(_playerTrm.position.x - _enemy.transform.position.x) > _enemy.nearDistance) Move();
        else {
            _enemy.FlipController(_playerTrm.position.x - _enemy.transform.position.x);
            _enemy.StopImmediately(false);
        }
        
        if(_enemy.IsOnPlatform()) {
            if(Mathf.Abs(direction.y) >= 2f && Mathf.Abs(direction.x) < 3f) {
                _enemy.downJumpTimer += Time.deltaTime;

                if(_enemy.downJumpTimer >= 2f) {
                    _enemy.DownJump();
                    _enemy.downJumpTimer = 0f;
                }
            }
            else {
                _enemy.downJumpTimer -= Time.deltaTime / 2f;
            }
        }
        else _enemy.downJumpTimer = 0f;
    }

    private void Move() {
        _enemy.SetVelocity(_enemy.Stat.moveSpeed.GetValue() * _enemy.FacingDirection, _rigidbody.velocity.y);
    }
}
