using System;
using UnityEngine;
using FSM;

public enum LJ_KStateEnum {
    Battle, Chop, DoubleAttack, TripleAttack, SprayStone, Dead
}

public class EnemyLJ_K : Enemy
{
    public EnemyStateMachine<LJ_KStateEnum> StateMachine { get; private set; }

    public readonly int battleModeHash = Animator.StringToHash("BattleMode");
    public readonly int arriveHash = Animator.StringToHash("Arrive");

    [HideInInspector] public int currentAttackDamage;
    [HideInInspector] public Vector2 currentAttackRange;
    [HideInInspector] public Vector2 currentAttackOffset;

    [Header("Pattern Check Settings")]
    public float combatAttackDistance;
    public float rangeAttackDistance;

    [Header("Pattern Settings")]
    public int chopDamage;
    public Vector2 chopRange;
    public Vector2 chopOffset;
    public GameObject chopStonePrefab;
    public Transform stoneSpawnPosTrm;

    [Space]

    public int doubleAttackDamage;
    public Vector2 doubleAttack1Range;
    public Vector2 doubleAttack1Offset;
    public Vector2 doubleAttack2Range;
    public Vector2 doubleAttack2Offset;

    [Space]

    public int tripleAttackDamage;

    [Space]

    public GameObject sprayStonePrefab;

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
        DamageCasterCompo.Damage(currentAttackDamage, (Vector2)transform.position + currentAttackOffset, currentAttackRange);
    }

    public override void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube((Vector2)transform.position + chopOffset * FacingDirection, chopRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((Vector2)transform.position + doubleAttack1Offset * FacingDirection, doubleAttack1Range);
        Gizmos.DrawWireCube((Vector2)transform.position + doubleAttack2Offset * FacingDirection, doubleAttack2Range);
    }
}