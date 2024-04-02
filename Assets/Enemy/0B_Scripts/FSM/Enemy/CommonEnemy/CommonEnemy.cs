using System;
using UnityEngine;
using FSM;

public enum CommonEnemyStateEnum {
    Chase, Attack, Dead
}

public class CommonEnemy : Enemy
{
    public EnemyStateMachine<CommonEnemyStateEnum> StateMachine { get; private set; }

    protected override void Awake() {
        base.Awake();

        StateMachine = new EnemyStateMachine<CommonEnemyStateEnum>();

        foreach(CommonEnemyStateEnum stateEnum in Enum.GetValues(typeof(CommonEnemyStateEnum))) {
            string typeName = stateEnum.ToString();
            Type t = Type.GetType($"CommonEnemy{typeName}State");

            try {
                var enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<CommonEnemyStateEnum>;
                StateMachine.AddState(stateEnum, enemyState);
            }
            catch {
                Debug.LogError($"[Enemy CommonEnemy] : Not Found State [{typeName}]");
            }
        }
    }

    private void Start() {
        StateMachine.Initialize(CommonEnemyStateEnum.Chase, this);
    }

    private void Update() {
        StateMachine.CurrentState.UpdateState();

        if(Input.GetKeyDown(KeyCode.P)) {
            isDead = true;
        }
    }

    public override void Attack() { }

    public override void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
