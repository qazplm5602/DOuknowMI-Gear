using UnityEngine;
using FSM;
using System.Collections;

public class LJ_KSprayStoneState : EnemyState<LJ_KStateEnum>
{
    public LJ_KSprayStoneState(Enemy enemy, EnemyStateMachine<LJ_KStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private float paddingX;

    private EnemyLJ_K _enemyLJ_K;

    public override void Enter() {
        base.Enter();

        _enemyLJ_K = _enemy as EnemyLJ_K;

        BoxCollider2D boxCollider = _enemy.ColliderCompo as BoxCollider2D;
        paddingX = boxCollider.size.x / 1.5f;

        _enemy.StartCoroutine(MoveRoutine());
    }

    public override void UpdateState() {
        if(_triggerCalled) {
            _stateMachine.ChangeState(LJ_KStateEnum.Battle);
        }
    }

    private IEnumerator MoveRoutine() {
        RaycastHit2D rightHit = Physics2D.Raycast(_enemy.transform.position, Vector2.right, Mathf.Infinity, _enemy.whatIsObstacle);
        RaycastHit2D leftHit = Physics2D.Raycast(_enemy.transform.position, -Vector2.right, Mathf.Infinity, _enemy.whatIsObstacle);

        float rightDistance = Vector2.Distance(_enemy.transform.position, rightHit.point);
        float leftDistance = Vector2.Distance(_enemy.transform.position, leftHit.point);

        int direction = (rightDistance < leftDistance) ? 1 : -1;

        float goalX = (rightDistance < leftDistance) ? rightHit.point.x: leftHit.point.x;

        _enemy.AnimatorCompo.speed = 3f;

        yield return new WaitUntil(() => {
            _enemy.SetVelocity(direction * 12f, _enemy.RigidbodyCompo.velocity.y);
            return Mathf.Abs(_enemy.transform.position.x - goalX) < paddingX;
        });

        _enemy.AnimatorCompo.speed = 1f;
        _enemy.AnimatorCompo.SetTrigger(_enemyLJ_K.arriveHash);

        yield return new WaitForSeconds(0.5f);

        _enemy.Flip();
    }
}
