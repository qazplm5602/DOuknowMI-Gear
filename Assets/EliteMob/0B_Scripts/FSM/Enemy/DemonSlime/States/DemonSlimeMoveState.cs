using UnityEngine;
using FSM;

public class DemonSlimeMoveState : DemonSlimeGroundState
{
    public DemonSlimeMoveState(Enemy enemy, EnemyStateMachine<DemonSlimeStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private Transform _playerTrm;

    public override void Enter() {
        base.Enter();

        _playerTrm = GameObject.Find("Player").transform;
    }

    public override void UpdateState() {
        _enemy.SetVelocity(_enemy.moveSpeed * _enemy.FacingDirection, _rigidbody.velocity.y);

        _enemy.FlipController(_playerTrm.position.x - _enemy.transform.position.x);
    }
}
