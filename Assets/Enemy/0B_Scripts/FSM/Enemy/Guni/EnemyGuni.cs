using System;
using UnityEngine;
using FSM;

public class EnemyGuni : Enemy
{
    public EnemyStateMachine<CommonEnemyStateEnum> StateMachine { get; private set; }

    public Transform attackTransform;

    protected override void Awake() {
        base.Awake();

        StateMachine = new EnemyStateMachine<CommonEnemyStateEnum>();
        
        HealthCompo.OnDead += () => StateMachine.ChangeState(CommonEnemyStateEnum.Dead);

        foreach(CommonEnemyStateEnum stateEnum in Enum.GetValues(typeof(CommonEnemyStateEnum))) {
            string typeName = stateEnum.ToString();
            Type t = Type.GetType($"Guni{typeName}State");

            try {
                var enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<CommonEnemyStateEnum>;
                StateMachine.AddState(stateEnum, enemyState);
            }
            catch {
                Debug.LogError($"[Enemy Guni] : Not Found State [{typeName}]");
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

        StateMachine.CurrentState.AnimationAttackTrigger();
    }

    public override void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
