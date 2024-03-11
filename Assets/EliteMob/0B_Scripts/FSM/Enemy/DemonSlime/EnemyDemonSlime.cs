using System;
using UnityEngine;
using FSM;

public enum DemonSlimeStateEnum {
    Idle, Move, Battle, Attack, Smash, Breath, Spell, Dead
}

public class EnemyDemonSlime : Enemy
{
    public EnemyStateMachine<DemonSlimeStateEnum> StateMachine { get; private set; }

    [Header("Pattern Settings")]
    public float smashRange;
    public float breathRange;

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

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_checkTransform.position, new Vector3(smashRange * 2, 3));
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(_checkTransform.position, new Vector3(attackRange * 2, 3.5f));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_checkTransform.position, new Vector3(breathRange * 2, 4));
    }
}
