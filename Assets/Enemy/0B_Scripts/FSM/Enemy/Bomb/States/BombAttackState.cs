using System.Collections;
using UnityEngine;
using FSM;

public class BombAttackState : EnemyState<CommonEnemyStateEnum>
{
    public BombAttackState(Enemy enemy, EnemyStateMachine<CommonEnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private Transform _playerTrm;

    public override void Enter() {
        base.Enter();

        _enemy.Stat.moveSpeed.AddModifier(-6f);

        _enemy.StartCoroutine(Blink());
        _enemy.StartDelayCallback(2, () => _stateMachine.ChangeState(CommonEnemyStateEnum.Dead));

        _playerTrm = PlayerManager.instance.playerTrm;

        _enemy.HealthCompo.enabled = false;
        
        PoolManager.Instance.Push(_enemy.helathBar);
    }

    public override void UpdateState() {
        Vector2 direction = _playerTrm.position - _enemy.transform.position;
        _enemy.SetVelocity(Mathf.Sign(direction.x) * _enemy.Stat.moveSpeed.GetValue(), _rigidbody.velocity.y);
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
