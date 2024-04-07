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
    public Vector2 smashRange;
    public Vector2 smashOffset;
    public Vector2 breathRange;
    public Vector2 breathOffset;

    protected override void Awake() {
        base.Awake();
        HealthCompo.OnDead += HandleDeadEvent;

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

    private void OnDestroy() {
        HealthCompo.OnDead -= HandleDeadEvent;
    }

    private void Update() {
        StateMachine.CurrentState.UpdateState();
    }

    public override void Attack() {
        
    }

    public override void ReturnDefaultSpeed() {

    }

    public override void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void HandleDeadEvent() {
        StateMachine.ChangeState(DemonSlimeStateEnum.Dead);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_checkTransform.position + (Vector3)smashOffset * FacingDirection, smashRange);
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(_checkTransform.position + (Vector3)attackOffset * FacingDirection, attackRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_checkTransform.position + (Vector3)breathOffset * FacingDirection, breathRange);
    }
}
