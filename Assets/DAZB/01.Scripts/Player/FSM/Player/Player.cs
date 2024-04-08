using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStateEnum {
    Idle,
    Run,
    Dash,
    Fall,
    Jump,
    Hurt,
    Attack,
    Dead
}

public class Player : Agent
{
    [Header("Setting value")]
    public float speed;
    public float dashPower;
    public float jumpPower;
    public float ATK;
    public float def;
    public PlayerStateMachine StateMachine {get; private set;}
    [HideInInspector] public float lastAttackTime;
    [SerializeField] private InputReader inputReader;
    public InputReader InputReader => inputReader;
    public bool isDash;
    private PlayerStat stats;

    protected override void Awake() {
        stats = GetComponent<PlayerStat>();
        stats.OnUpdateStat += UpdateState;
        base.Awake();
        StateMachine = new PlayerStateMachine();
        foreach (PlayerStateEnum stateEnum in Enum.GetValues(typeof(PlayerStateEnum))) {
            string typeName = stateEnum.ToString();
            try {
                Type t = Type.GetType($"Player{typeName}State");
                PlayerState playerState = Activator.CreateInstance(t, this, StateMachine, typeName) as PlayerState;
                StateMachine.AddState(stateEnum, playerState);
            }
            catch (Exception ex) {
                Debug.LogError($"{typeName} is loading error check Message : {ex.Message}");
            }
        }
    }

    protected void Start() {
        StateMachine.Initialize(PlayerStateEnum.Idle, this);
        speed = stats.defaultSpeed;
        def = stats.defaultDefense;
    }

    public override void HandleDeadEvent() {
        isDead = true;
        StateMachine.ChangeState(PlayerStateEnum.Dead);
    }

    protected void Update() {
        if (DialogueManager.instance.isEnd == false || isDead) return;
        StateMachine.CurrentState.UpdateState();
    }

    private void UpdateState() {
        
    }
}
