using UnityEngine;
using FSM;

public class DemonSlimeIdleState : EnemyState<DemonSlimeStateEnum>
{
    public DemonSlimeIdleState(Enemy enemy, EnemyStateMachine<DemonSlimeStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private object _player; //Player
    private int _moveDirection;
    private float _timer = 0f;

    private readonly int _xVelocityHash = Animator.StringToHash("x_velocity");

    public override void UpdateState() {
        base.UpdateState();

        // SetDirectionToPlayer();

        _enemy.AnimatorCompo.SetFloat(_xVelocityHash, Mathf.Abs(_rigidbody.velocity.x));

        _moveDirection = _enemy.FacingDirection;
        _enemy.SetVelocity(_enemy.moveSpeed * _moveDirection, _rigidbody.velocity.y);
    }
}
