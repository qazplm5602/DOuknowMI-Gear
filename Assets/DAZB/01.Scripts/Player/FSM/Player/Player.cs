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
}

public class Player : Agent
{
    [Header("Setting value")]
    public float moveSpeed;
    public float dashPower;
    public float jumpPower;
    public PlayerStateMachine StateMachine {get; private set;}
    [SerializeField] private InputReader inputReader;
    public InputReader InputReader => inputReader;
    public bool isDash;

    protected override void Awake() {
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
    }

    protected void Update() {
        StateMachine.CurrentState.UpdateState();
    }
}
