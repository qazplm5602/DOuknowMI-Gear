using UnityEngine;
using FSM;

public class BombChaseState : EnemyState<CommonEnemyStateEnum>
{
    public BombChaseState(Enemy enemy, EnemyStateMachine<CommonEnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private Transform _playerTrm;
    private float _jumpTimer = 0f;

    public override void Enter() {
        base.Enter();

        _playerTrm = PlayerManager.instance.playerTrm;
        _enemy.downJumpTimer = 0f;
    }

    public override void UpdateState() {
        Vector2 direction = _playerTrm.position - _enemy.transform.position;
        if(direction.magnitude <= _enemy.nearDistance) {
            if(_enemy.CanAttack()) {
                _stateMachine.ChangeState(CommonEnemyStateEnum.Attack);
            }
        }
        else if(Mathf.Abs(direction.x) >= 0.1f) {
            _enemy.SetVelocity(Mathf.Sign(direction.x) * _enemy.Stat.moveSpeed.GetValue(), _rigidbody.velocity.y);
        }
        else {
            _enemy.StopImmediately(false);
        }
        
        
        if(_enemy.IsOnPlatform()) {
            if(direction.y <= -1f && Mathf.Abs(direction.x) < 3f) {
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
        else _enemy.downJumpTimer -= Time.deltaTime / 2f;

        if(_enemy.IsUnderPlatform()) {
            if(direction.y >= 1f && Mathf.Abs(direction.x) < 3f) {
                _jumpTimer += Time.deltaTime;

                if(_jumpTimer >= 2f) {
                    float jump = Physics2D.Raycast(_enemy.transform.position, Vector2.up, 4f, _enemy.whatIsPlatform).distance;
                    _enemy.SetVelocity(_enemy.RigidbodyCompo.velocity.x, jump + _enemy.jumpPower);
                    _jumpTimer = 0f;
                }
            }
            else {
                _jumpTimer -= Time.deltaTime / 2f;
            }
        }
        else _jumpTimer -= Time.deltaTime / 2f;
    }
}
