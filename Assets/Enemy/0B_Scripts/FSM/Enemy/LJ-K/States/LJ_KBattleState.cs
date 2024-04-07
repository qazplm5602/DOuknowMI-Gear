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
    }

    public override void UpdateState() {
        float distance = Mathf.Abs(_playerTrm.position.x - _enemy.transform.position.x);
        if(distance <= 6f) {
            _enemy.StopImmediately(false);
        }
        else if(distance <= 7.5f) {
            if(_enemy.CanAttack()) {
                _stateMachine.ChangeState(LJ_KStateEnum.Chop);
            }
        }
        else {
            _enemy.SetVelocity(_enemy.moveSpeed * _enemy.FacingDirection, _rigidbody.velocity.y);
        }
    }
}
