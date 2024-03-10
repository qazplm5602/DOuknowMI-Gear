using System;
using UnityEngine;
using FSM;

public enum DemonSlimeStateEnum {
    Idle, Move, Battle, Attack, Dead
}

public class EnemyDemonSlime : Enemy
{
    public EnemyStateMachine<DemonSlimeStateEnum> StateMachine { get; private set; }

    [SerializeField] protected float _smashRange;
    [SerializeField] protected float _breathRange;
    [SerializeField] protected float _spellRange;

    protected override void Awake() {
        base.Awake();

        StateMachine = new EnemyStateMachine<DemonSlimeStateEnum>();

        foreach(DemonSlimeStateEnum stateEnum in Enum.GetValues(typeof(DemonSlimeStateEnum))) {
            string typeName = stateEnum.ToString();
            Type t = Type.GetType($"DemonSlime{typeName}State");

            try {
                var enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<DemonSlimeStateEnum>;
                StateMachine.AddState(stateEnum, enemyState);
            }
            catch {
                Debug.LogError($"[Enemy DemonSlime] : Not Found State [{typeName}]");
            }
        }
    }

    private void Start() {
        StateMachine.Initialize(DemonSlimeStateEnum.Idle, this);
    }

    private void Update() {
        StateMachine.CurrentState.UpdateState();
    }

    public override void Attack() {
        
    }

    public override void ReturnDefaultSpeed() {
        
    }

    public override void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
