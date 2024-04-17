using UnityEngine;
using FSM;

public class MGBattleState : EnemyState<MGStateEnum>
{
    public MGBattleState(Enemy enemy, EnemyStateMachine<MGStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private EnemyMG _enemyMG;

    private Transform _playerTrm;

    public override void Enter() {
        base.Enter();

        _enemyMG = _enemy as EnemyMG;

        _playerTrm = PlayerManager.instance.playerTrm;
        _enemy.AnimatorCompo.SetFloat(_enemyMG.battleModeHash, 0);
    }

    public override void UpdateState() {
        Vector2 direction = _playerTrm.position - _enemy.transform.position;
        float distance = Vector2.Distance(_enemy.transform.position, _playerTrm.position);

        _enemy.FlipController(direction.x);

       if(_enemy.CanAttack() && !_enemy.IsObstacleInLine(distance, direction.normalized) && distance < _enemyMG.combatAttackRange) {
            _stateMachine.ChangeState(MGStateEnum.Punch);
        }
        else if(_enemy.CanAttack() && !_enemy.IsObstacleInLine(distance, direction.normalized) && distance < _enemyMG.rangeAttackRange) {
            _stateMachine.ChangeState(MGStateEnum.Spit);
        }
        else if(distance <= _enemy.nearDistance) {
            _enemy.StopImmediately(false);
            _enemy.AnimatorCompo.SetFloat(_enemyMG.battleModeHash, 0);
        }
        else {
            _enemy.AnimatorCompo.SetFloat(_enemyMG.battleModeHash, 1);
            _enemy.SetVelocity(_enemy.FacingDirection * _enemy.moveSpeed, _rigidbody.velocity.y);
        }
    }
}
