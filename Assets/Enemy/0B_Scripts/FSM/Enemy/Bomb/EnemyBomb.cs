using System;
using UnityEngine;
using FSM;

public class EnemyBomb : Enemy
{
    public EnemyStateMachine<CommonEnemyStateEnum> StateMachine { get; private set; }

    [Space]
    
    public GameObject boomPrefab;

    protected override void Awake() {
        base.Awake();

        StateMachine = new EnemyStateMachine<CommonEnemyStateEnum>();

        foreach(CommonEnemyStateEnum stateEnum in Enum.GetValues(typeof(CommonEnemyStateEnum))) {
            string typeName = stateEnum.ToString();
            Type t = Type.GetType($"Bomb{typeName}State");

            try {
                var enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<CommonEnemyStateEnum>;
                StateMachine.AddState(stateEnum, enemyState);
            }
            catch {
                Debug.LogError($"[Enemy Bomb] : Not Found State [{typeName}]");
            }
        }
    }

    private void Start() {
        StateMachine.Initialize(CommonEnemyStateEnum.Chase, this);
    }

    private void Update() {
        StateMachine.CurrentState.UpdateState();
    }

    public override void Attack() => StateMachine.CurrentState.AnimationAttackTrigger();

    public override void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
