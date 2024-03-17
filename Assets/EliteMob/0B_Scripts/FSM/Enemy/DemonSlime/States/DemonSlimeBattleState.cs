using UnityEngine;
using FSM;

public class DemonSlimeBattleState : EnemyState<DemonSlimeStateEnum>
{
    public DemonSlimeBattleState(Enemy enemy, EnemyStateMachine<DemonSlimeStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    protected Transform _playerTrm;
    private EnemyDemonSlime _demonSlime;

    public override void Enter() {
        base.Enter();

        _playerTrm = GameObject.Find("Player").transform;
        _demonSlime = (EnemyDemonSlime)_enemy;
    }

    public override void UpdateState() {
        base.UpdateState();
        
        _enemy.SetVelocity(_enemy.moveSpeed * _enemy.FacingDirection, _rigidbody.velocity.y);

        _enemy.FlipController(_playerTrm.position.x - _enemy.transform.position.x);

        CheckAttack();
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
