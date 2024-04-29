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
        else if(_enemy.IsOnPlatform()) {
            if(Mathf.Abs(direction.y) >= 2f && Mathf.Abs(direction.x) < 3f) {
                _enemy.downJumpTimer += Time.deltaTime;
                _enemy.DownJump();
                _enemy.downJumpTimer = 0f;
            }
            else {
                _enemy.downJumpTimer -= Time.deltaTime / 2f;
            }
        }
        else {
            _enemy.SetVelocity(Mathf.Sign(direction.x) * _enemy.Stat.moveSpeed.GetValue(), _rigidbody.velocity.y);
        }
    }
}
