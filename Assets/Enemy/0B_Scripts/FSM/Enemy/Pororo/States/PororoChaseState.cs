using UnityEngine;
using FSM;

public class PororoChaseState : EnemyState<CommonEnemyStateEnum>
{
    public PororoChaseState(Enemy enemy, EnemyStateMachine<CommonEnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private EnemyPororo _enemyPororo;
    private Transform _playerTrm;

    public override void Enter() {
        base.Enter();

        _enemyPororo = _enemy as EnemyPororo;

        _playerTrm = PlayerManager.instance.playerTrm;
        _enemy.FlipController(_playerTrm.position.x - _enemy.transform.position.x);
    }

    public override void UpdateState() {
        if(_enemy.isDead) _stateMachine.ChangeState(CommonEnemyStateEnum.Dead);

        Vector2 distance = _playerTrm.position - _enemy.transform.position;
        if(distance.magnitude < _enemyPororo.attackDistance && !_enemy.IsObstacleInLine(_enemyPororo.attackDistance, distance.normalized)) {
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
