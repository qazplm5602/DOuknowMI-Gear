using UnityEngine;
using FSM;

public class LJ_KBattleState : EnemyState<LJ_KStateEnum>
{
    public LJ_KBattleState(Enemy enemy, EnemyStateMachine<LJ_KStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private EnemyLJ_K _enemyLJ_K;
    private Transform _playerTrm;

    public override void Enter() {
        base.Enter();

        _playerTrm = PlayerManager.instance.playerTrm;
        _enemyLJ_K = _enemy as EnemyLJ_K;
        _enemy.AnimatorCompo.SetFloat(_enemyLJ_K.battleModeHash, 0);
        _enemy.FlipController(_playerTrm.position.x - _enemy.transform.position.x);
    }

    public override void UpdateState() {
        float distance = Mathf.Abs(_playerTrm.position.x - _enemy.transform.position.x);
        if(distance <= _enemyLJ_K.combatAttackDistance) {
            if(_enemy.CanAttack()) {
                _stateMachine.ChangeState(LJ_KStateEnum.DoubleAttack);
                return;
            }
        }
        else if(distance > _enemyLJ_K.rangeAttackDistance) {
            if(_enemy.CanAttack()) {
                return;
            }
        }

        if(distance <= _enemy.nearDistance) {
            _enemy.AnimatorCompo.SetFloat(_enemyLJ_K.battleModeHash, 0);
            _enemy.StopImmediately(false);
        }
        else {
            _enemy.AnimatorCompo.SetFloat(_enemyLJ_K.battleModeHash, 1);
            _enemy.FlipController(_playerTrm.position.x - _enemy.transform.position.x);
            _enemy.SetVelocity(_enemy.moveSpeed * _enemy.FacingDirection, _rigidbody.velocity.y);
        }
    }
}
