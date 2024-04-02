using UnityEngine;
using FSM;

public class CommonEnemyChaseState : EnemyState<CommonEnemyStateEnum>
{
    public CommonEnemyChaseState(Enemy enemy, EnemyStateMachine<CommonEnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private Transform _playerTrm;

    public override void Enter() {
        base.Enter();

        _playerTrm = PlayerManager.instance.playerTrm;
    }

    public override void UpdateState() {
        if(_enemy.isDead) _stateMachine.ChangeState(CommonEnemyStateEnum.Dead);

        if(Physics2D.OverlapBox((Vector2)_enemy.transform.position + _enemy.attackOffset * _enemy.FacingDirection, _enemy.attackRange, 0, _enemy.whatIsPlayer)) {
            if(_enemy.CanAttack()) {
                _stateMachine.ChangeState(CommonEnemyStateEnum.Attack);
            }
        }
        else if(Mathf.Abs(_playerTrm.position.x - _enemy.transform.position.x) > _enemy.nearDistance) Move();
    }

    private void Move() {
        _enemy.SetVelocity(_enemy.moveSpeed * _enemy.FacingDirection, _rigidbody.velocity.y);
        
        _enemy.FlipController(_playerTrm.position.x - _enemy.transform.position.x);
    }
}
