using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public float moveSpeed;
    public float dashPower;
    public float jumpPower;
    public float atk;
    public float atkCool;
    public float criticalChance;
    public PlayerStateMachine StateMachine {get; private set;}
    [SerializeField] private InputReader inputReader;
    public InputReader InputReader => inputReader;
    public bool isDash;
    private PlayerStat stats;
    public float lastAttackTime {get; private set;}
    public bool isAttack;

    protected override void Awake() {
        stats = GetComponent<PlayerStat>();
        stats.OnUpdateStat += UpdateState;
        moveSpeed = stats.defaultMoveSpeed;
        atk = stats.defaultAtk;
        atkCool = stats.defaultAttackCool;
        criticalChance = stats.defaultCriticalChance;
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

    private void OnDestroy() {
        stats.OnUpdateStat -= UpdateState;
    }

    protected void Start() {
        StateMachine.Initialize(PlayerStateEnum.Idle, this);
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
        moveSpeed = stats.currentMoveSpeed;
        atk = stats.currentAtk;
        atkCool = stats.currentAttckCool;
        criticalChance = stats.currentCriticalChance;
    }
}
