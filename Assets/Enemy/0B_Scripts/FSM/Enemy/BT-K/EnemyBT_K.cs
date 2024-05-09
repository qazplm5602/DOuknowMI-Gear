using System;
using UnityEngine;
using FSM;

public class EnemyBT_K : Enemy
{
    public EnemyStateMachine<CommonEnemyStateEnum> StateMachine { get; private set; }

    [Space]

    public Transform attackTransform;

    public int bulletCount;
    [HideInInspector] public int attackCount = 0;

    protected override void Awake() {
        base.Awake();

        StateMachine = new EnemyStateMachine<CommonEnemyStateEnum>();
        
        HealthCompo.OnDead += () => StateMachine.ChangeState(CommonEnemyStateEnum.Dead);

        foreach(CommonEnemyStateEnum stateEnum in Enum.GetValues(typeof(CommonEnemyStateEnum))) {
            string typeName = stateEnum.ToString();
            Type t = Type.GetType($"BT_K{typeName}State");

            try {
                var enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<CommonEnemyStateEnum>;
                StateMachine.AddState(stateEnum, enemyState);
            }
            catch {
                Debug.LogError($"[Enemy BT-K] : Not Found State [{typeName}]");
            }
        }
    }

    private void Start() {
        StateMachine.Initialize(CommonEnemyStateEnum.Chase, this);
    }

    protected override void Update() {
        base.Update();
        
        StateMachine.CurrentState.UpdateState();
    }

    public override void Attack() {
        GameObject obj = PoolManager.Instance.Pop(PoolingType.Muzzle).gameObject;
        obj.transform.position = attackTransform.position;

        Muzzle muz = obj.GetComponent<Muzzle>();
        muz.Init(FacingDirection);

        SoundManager.Instance.PlaySound("Hand Gun 3");

        ++attackCount;
        StateMachine.CurrentState.AnimationAttackTrigger();
    }

    public override void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
