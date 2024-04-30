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
        float distance = Mathf.Abs(direction.x);
        if(_enemy.IsPlayerDetected(_enemy.attackOffset, _enemy.attackRange) && !_enemy.IsObstacleInLine(direction.magnitude, direction.normalized)) {
            _enemy.StopImmediately(false);
            if(_enemy.CanAttack()) {
                _stateMachine.ChangeState(CommonEnemyStateEnum.Attack);
            }
        }
        else if((Mathf.Abs(direction.y) >= 2f && distance > 0.1f) || distance > _enemy.nearDistance) {
            _enemy.FlipController(_playerTrm.position.x - _enemy.transform.position.x);
            Move();
        }
        else {
            _enemy.FlipController(direction.x);
            _enemy.StopImmediately(false);
        }
        
        if(_enemy.IsOnPlatform()) {
            if(Mathf.Abs(direction.y) >= 2f && Mathf.Abs(direction.x) < 3f) {
                _enemy.downJumpTimer += Time.deltaTime;
                Debug.Log(_enemy.downJumpTimer);

                if(_enemy.downJumpTimer >= 2f) {
                    _enemy.DownJump();
                    _enemy.downJumpTimer = 0f;
                }
            }
            else {
                _enemy.downJumpTimer -= Time.deltaTime / 2f;
            }
        }
        else _enemy.downJumpTimer -= Time.deltaTime / 2f;
    }

    private void Move() {
        _enemy.SetVelocity(_enemy.Stat.moveSpeed.GetValue() * _enemy.FacingDirection, _rigidbody.velocity.y);
    }
}
