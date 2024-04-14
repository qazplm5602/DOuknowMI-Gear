using System.Collections;
using UnityEngine;
using FSM;

public class BombAttackState : EnemyState<CommonEnemyStateEnum>
{
    public BombAttackState(Enemy enemy, EnemyStateMachine<CommonEnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    public override void Enter() {
        base.Enter();

        _enemy.moveSpeed = 1;

        _enemy.StartCoroutine(Blink());
        _enemy.StartDelayCallback(2, () => _stateMachine.ChangeState(CommonEnemyStateEnum.Dead));
    }

    public override void UpdateState() {
        _enemy.SetVelocity(_enemy.FacingDirection * _enemy.moveSpeed, _rigidbody.velocity.y);
    }

    private IEnumerator Blink() {
        _enemy.StartCoroutine(ChangeColorRoutine(Color.red, 0.5f));
        yield return new WaitForSeconds(0.5f);
        
        _enemy.StartCoroutine(ChangeColorRoutine(Color.white, 0.5f));
        yield return new WaitForSeconds(0.5f);
        
        _enemy.StartCoroutine(ChangeColorRoutine(Color.red, 0.35f));
        yield return new WaitForSeconds(0.35f);
        
        _enemy.StartCoroutine(ChangeColorRoutine(Color.white, 0.35f));
        yield return new WaitForSeconds(0.35f);
        
        _enemy.StartCoroutine(ChangeColorRoutine(Color.red, 0.3f));
    }

    private IEnumerator ChangeColorRoutine(Color color, float time) {
        Color beginColor = _enemy.SpriteRendererCompo.color;
        
        float timer = 0f;
        while(timer < time) {
            timer += Time.deltaTime;

            _enemy.SpriteRendererCompo.color = Color.Lerp(beginColor, color, timer / time);

            yield return null;
        }
    }
}
