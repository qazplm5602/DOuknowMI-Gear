using UnityEngine;
using FSM;

public class BombDeadState : EnemyState<CommonEnemyStateEnum>
{
    public BombDeadState(Enemy enemy, EnemyStateMachine<CommonEnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private EnemyBomb _enemyBomb;

    public override void Enter() {
        base.Enter();

        Debug.Log(1);

        _enemyBomb = _enemy as EnemyBomb;

        _enemy.StopImmediately(false);

        GameObject.Instantiate(_enemyBomb.boomPrefab, _enemy.transform.position, Quaternion.identity);
    }
}
