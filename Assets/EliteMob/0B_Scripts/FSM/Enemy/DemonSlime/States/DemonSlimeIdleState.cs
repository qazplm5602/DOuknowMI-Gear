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
            Vector2 smashStartPosition = (Vector2)_enemy.transform.position + _demonSlime.smashOffset * _enemy.FacingDirection;
            Vector2 attackStartPosition = (Vector2)_enemy.transform.position + _demonSlime.attackOffset * _enemy.FacingDirection;
            Vector2 breathStartPosition = (Vector2)_enemy.transform.position + _demonSlime.breathOffset * _enemy.FacingDirection;

            if(Physics2D.OverlapBox(smashStartPosition, _demonSlime.smashRange, 0, _enemy.whatIsPlayer)) {
                _stateMachine.ChangeState(DemonSlimeStateEnum.Smash);
            }
            else if(Physics2D.OverlapBox(attackStartPosition, _enemy.attackRange, 0, _enemy.whatIsPlayer)) {                                                                                      
                _stateMachine.ChangeState(DemonSlimeStateEnum.Attack);
            }
            else if(Physics2D.OverlapBox(breathStartPosition, _demonSlime.breathRange, 0, _enemy.whatIsPlayer)) {
                _stateMachine.ChangeState(DemonSlimeStateEnum.Breath);
            }
            else {
                _stateMachine.ChangeState(DemonSlimeStateEnum.Spell);    
            }
        }
    }
}
