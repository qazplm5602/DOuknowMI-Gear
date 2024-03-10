using UnityEngine;
using FSM;

public class DemonSlimeMoveState : DemonSlimeGroundState
{
    public DemonSlimeMoveState(Enemy enemy, EnemyStateMachine<DemonSlimeStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    public override void UpdateState() {
        _enemy.SetVelocity(_enemy.moveSpeed * _enemy.FacingDirection, _rigidbody.velocity.y);
    }
}
