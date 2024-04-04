using UnityEngine;
using FSM;

public class DemonSlimeIdleState : DemonSlimeGroundState
{
    public DemonSlimeIdleState(Enemy enemy, EnemyStateMachine<DemonSlimeStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private Transform _playerTrm;
    private EnemyDemonSlime _demonSlime;

    public override void Enter() {
        base.Enter();

        _enemy.StopImmediately(false);

        _playerTrm = GameObject.Find("Player").transform;
        _demonSlime = (EnemyDemonSlime)_enemy;
    }

    public override void UpdateState() {
        base.UpdateState();

        if(Mathf.Abs(_playerTrm.position.x - _enemy.transform.position.x) > _enemy.nearDistance) {
            _stateMachine.ChangeState(DemonSlimeStateEnum.Battle);
        }

        CheckAttack();
    }

    public override void Exit() {
        base.Exit();
    }

    private void CheckAttack() {
        if(_enemy.CanAttack()) {
            Vector3 direction = _playerTrm.position - _enemy.transform.position;
            if(!_enemy.IsObstacleInLine(direction.magnitude, direction.normalized)) {
                if(_enemy.IsPlayerDetected(_demonSlime.smashOffset, _demonSlime.smashRange)) {
                    _stateMachine.ChangeState(DemonSlimeStateEnum.Smash);
                }
                else if(_enemy.IsPlayerDetected(_demonSlime.attackOffset, _demonSlime.attackRange)) {                                                                                      
                    _stateMachine.ChangeState(DemonSlimeStateEnum.Attack);
                }
                else if(_enemy.IsPlayerDetected(_demonSlime.breathOffset, _demonSlime.breathRange)) {
                    _stateMachine.ChangeState(DemonSlimeStateEnum.Breath);
                }
            }
            else {
                _stateMachine.ChangeState(DemonSlimeStateEnum.Spell);    
            }
        }
    }
}
