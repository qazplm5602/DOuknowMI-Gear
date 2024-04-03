using System;
using UnityEngine;
using FSM;
using Unity.PlasticSCM.Editor.WebApi;

public enum GuniStateEnum {
    Chase,
    Attack,
    Dead
}

public class EnemyGuni : Enemy
{
    public EnemyStateMachine<GuniStateEnum> StateMachine { get; private set; }

    public Transform attackTransform;

    protected override void Awake() {
        base.Awake();

        StateMachine = new EnemyStateMachine<GuniStateEnum>();

        foreach(GuniStateEnum stateEnum in Enum.GetValues(typeof(GuniStateEnum))) {
            string typeName = stateEnum.ToString();
            Type t = Type.GetType($"Guni{typeName}State");

            try {
                var enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<GuniStateEnum>;
                StateMachine.AddState(stateEnum, enemyState);
            }
            catch {
                Debug.LogError($"[Enemy Guni] : Not Found State [{typeName}]");
            }
        }
    }

    private void Start() {
        StateMachine.Initialize(GuniStateEnum.Chase, this);
    }

    private void Update() {
        StateMachine.CurrentState.UpdateState();
    }

    public override void Attack() {
        StateMachine.CurrentState.AnimationAttackTrigger();
    }

    public override void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
