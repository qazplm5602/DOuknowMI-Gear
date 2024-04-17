using System;
using UnityEngine;
using FSM;

public enum MGStateEnum { Battle, Spit, Punch, Hook, Dead }

public class EnemyMG : Enemy
{
    public EnemyStateMachine<MGStateEnum> StateMachine;
    

    [Header("Attack Range Settings")]
    public float rangeAttackRange;
    public float combatAttackRange;

    [Space]
    public Transform attackTransform;
    public Vector2 punchOffset;
    public Vector2 punchRange;
    public int punchDamage;
    public Vector2 hookOffset;
    public Vector2 hookRange;
    public int hookDamage;

    public readonly int battleModeHash = Animator.StringToHash("BattleMode");

    protected override void Awake() {
        base.Awake();

        StateMachine = new EnemyStateMachine<MGStateEnum>();

        foreach(MGStateEnum stateEnum in Enum.GetValues(typeof(MGStateEnum))) {
            string typeName = stateEnum.ToString();
            Type t = Type.GetType($"MG{typeName}State");

            try {
                var enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<MGStateEnum>;
                StateMachine.AddState(stateEnum, enemyState);
            }
            catch {
                Debug.LogError($"[Enemy MG] : Not Found State [{typeName}]");
            }
        }
    }

    private void Start() {
        StateMachine.Initialize(MGStateEnum.Battle, this);
    }

    private void Update() {
        StateMachine.CurrentState.UpdateState();
    }

    public override void Attack() => StateMachine.CurrentState.AnimationAttackTrigger();

    public override void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    protected override void OnDrawGizmos() {
        base.OnDrawGizmos();

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, combatAttackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangeAttackRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + punchOffset, punchRange);
        Gizmos.DrawWireCube((Vector2)transform.position + hookOffset, hookRange);
    }
}
