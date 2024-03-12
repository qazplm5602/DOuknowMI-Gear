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
            float distance = Mathf.Abs(_playerTrm.position.x - _enemy.transform.position.x);
            
            if(distance <= _demonSlime.smashRange) {
                _stateMachine.ChangeState(DemonSlimeStateEnum.Smash);
            }
            else if(distance <= _demonSlime.attackRange) {
                _stateMachine.ChangeState(DemonSlimeStateEnum.Attack);
            }
            else if(distance <= _demonSlime.breathRange) {
                _stateMachine.ChangeState(DemonSlimeStateEnum.Breath);
            }
            else {
                _stateMachine.ChangeState(DemonSlimeStateEnum.Spell);    
            }
        }
    }
}
