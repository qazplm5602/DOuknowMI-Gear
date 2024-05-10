using System;
using UnityEngine;
using UnityEngine.UI;
using FSM;

public enum LJ_KStateEnum {
    Battle, Chop, DoubleAttack, TripleAttack, SprayStone, Earthquake, Dead
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

    [Space]

    public GameObject stoneColumnPrefab;

    [Space]

    public BossStage bossStage;

    protected override void Awake() {
        base.Awake();
        healthBar.gameObject.SetActive(false);
        healthBar = transform.Find("Canvas/HealthBar").GetComponent<EnemyHealthBar>();
        healthBar.Init();
        HealthCompo.healthFilled = healthBar.transform.Find("Image").GetComponent<Image>();

        StateMachine = new EnemyStateMachine<LJ_KStateEnum>();
        
        HealthCompo.OnDead += () => StateMachine.ChangeState(LJ_KStateEnum.Dead);

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

    protected override void Update() {
        base.Update();
        
        StateMachine.CurrentState.UpdateState();
    }

    public override void Attack() {
        DamageCasterCompo.Damage(currentAttackDamage, 
            new Vector2(currentAttackOffset.x * FacingDirection, currentAttackOffset.y), currentAttackRange);
    }

    public override void SetDead()
    {
        base.SetDead();
        bossStage.Clear();
    }

    public override void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    protected override void OnDrawGizmos() {
        base.OnDrawGizmos();
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, combatAttackDistance);
        Gizmos.DrawWireSphere(transform.position, rangeAttackDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube((Vector2)transform.position + chopOffset * FacingDirection, chopRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((Vector2)transform.position + doubleAttack1Offset * FacingDirection, doubleAttack1Range);
        Gizmos.DrawWireCube((Vector2)transform.position + doubleAttack2Offset * FacingDirection, doubleAttack2Range);
    }
}
