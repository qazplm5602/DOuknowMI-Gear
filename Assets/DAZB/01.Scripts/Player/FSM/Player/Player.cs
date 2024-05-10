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
    Dead,
    Interaction
}

public class Player : Agent
{
    [Header("Setting value")]
    public float moveSpeed;
    public float dashPower;
    public float jumpPower;
    public float dashCool;
    public float atk;
    public float atkCool;
    public int defense;
    public float criticalChance;
    public PlayerStateMachine StateMachine {get; private set;}
    [SerializeField] private InputReader inputReader;
    [SerializeField] private GearSO[] gearSO;
    public InputReader InputReader => inputReader;
    public bool isDash;
    public float lastAttackTime;
    public float lastDashTime;
    public bool isAttack;
    public bool isUnderJumpping = false;

    protected override void Awake() {
        for (int i = 0; i < gearSO.Length; ++i) {
            IngameUIControl.Instance.gearChangeUI.GiveInventory(gearSO[i]);
        }
        SetStat();
        base.Awake();
        PlayerStat.Instance.OnUpdateStat += SetStat;
        SetStat();
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
    }

    public override void HandleDeadEvent() {
        isDead = true;
        StateMachine.ChangeState(PlayerStateEnum.Dead);
    }

    protected void Update() {
        if (DialogueManager.Instance != null)
            if (DialogueManager.Instance.isEnd == false || isDead) return;
        StateMachine.CurrentState.UpdateState();
    }

    private void SetStat() {
        atk = stat.attack.GetValue();
        atkCool = stat.GetAttackSpeed();
        moveSpeed = stat.GetMoveSpeed();
        criticalChance = stat.GetCriticalChance();
        defense = (int)stat.defense.GetValue();
    }
}
