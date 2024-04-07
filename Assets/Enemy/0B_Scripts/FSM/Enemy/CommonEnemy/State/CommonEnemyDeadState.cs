using UnityEngine;
using FSM;

public class CommonEnemyDeadState : EnemyState<CommonEnemyStateEnum>
{
    public CommonEnemyDeadState(Enemy enemy, EnemyStateMachine<CommonEnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    public override void Enter() {
        base.Enter();

        _enemy.StopImmediately(true);

        int randomSmallPartAmount = Random.Range(_enemy.dropTable.smallPartAmount.x, _enemy.dropTable.smallPartAmount.y + 1);
        int randomBigPartAmount = Random.Range(_enemy.dropTable.bigPartAmount.x, _enemy.dropTable.bigPartAmount.y + 1);
        for(int i = 0; i < randomSmallPartAmount; ++i) {
            Transform trm = PoolManager.Instance.Pop(PoolingType.SmallPart).transform;
            trm.position = _enemy.transform.position;
        }
        for(int i = 0; i < randomBigPartAmount; ++i) {
            Transform trm = PoolManager.Instance.Pop(PoolingType.BigPart).transform;
            trm.position = _enemy.transform.position;
        }

        _enemy.gameObject.SetActive(false);
    }
}
