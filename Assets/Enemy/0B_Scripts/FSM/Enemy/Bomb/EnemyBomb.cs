using System;
using UnityEngine;
using FSM;

public class EnemyBomb : Enemy
{
    public EnemyStateMachine<CommonEnemyStateEnum> StateMachine { get; private set; }

    protected override void Awake() {
        base.Awake();

        StateMachine = new EnemyStateMachine<CommonEnemyStateEnum>();
        
        HealthCompo.OnDead += () => StateMachine.ChangeState(CommonEnemyStateEnum.Dead);

        foreach(CommonEnemyStateEnum stateEnum in Enum.GetValues(typeof(CommonEnemyStateEnum))) {
            string typeName = stateEnum.ToString();
            Type t = Type.GetType($"Bomb{typeName}State");

            try {
                var enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<CommonEnemyStateEnum>;
                StateMachine.AddState(stateEnum, enemyState);
            }
            catch {
                Debug.LogError($"[Enemy Bomb] : Not Found State [{typeName}]");
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
        EnemyBoom boom = PoolManager.Instance.Pop(PoolingType.Boom) as EnemyBoom;
        boom.transform.position = transform.position;
        boom.Init(3f, attackDamage);
        
        StateMachine.CurrentState.AnimationAttackTrigger();
    }

    public override void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
