using System;
using UnityEngine;
using FSM;

public enum LJ_KStateEnum {
    Battle, Chop, Dead
}

public class EnemyLJ_K : Enemy
{
    public EnemyStateMachine<LJ_KStateEnum> StateMachine { get; private set; }

    public readonly int battleModeHash = Animator.StringToHash("BattleMode");

    [HideInInspector] public Vector2 currentAttackRange;
    [HideInInspector] public Vector2 currentAttackOffset;

    [Header("Pattern Settings")]
    public int chopDamage;
    public Vector2 chopRange;
    public Vector2 chopOffset; 

    protected override void Awake() {
        base.Awake();

        StateMachine = new EnemyStateMachine<LJ_KStateEnum>();

        foreach(LJ_KStateEnum stateEnum in Enum.GetValues(typeof(LJ_KStateEnum))) {
            string typeName = stateEnum.ToString();
            Type t = Type.GetType($"LJ_K{typeName}State");

            try {
                var enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<LJ_KStateEnum>;
                StateMachine.AddState(stateEnum, enemyState);
            }
            catch {
                Debug.LogError($"[Enemy LJ-K] : Not Found State [{typeName}]");
            }
        }
    }

    private void Start() {
        StateMachine.Initialize(LJ_KStateEnum.Battle, this);
    }

    private void Update() {
        StateMachine.CurrentState.UpdateState();
    }

    public override void Attack() {
        
    }

    public override void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
