using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState CurrentState {get; private set;}
    public Dictionary<PlayerStateEnum, PlayerState> stateDictionary;
    private Player player;

    public PlayerStateMachine() {
        stateDictionary = new Dictionary<PlayerStateEnum, PlayerState>();
    }

    public void Initialize(PlayerStateEnum startState, Player player) {
        this.player = player;
        CurrentState = stateDictionary[startState];
        CurrentState.Enter();
    }

    public void ChangeState(PlayerStateEnum newState) {
        if (player.CanStateChangeable == false) return;
        CurrentState.Exit();
        CurrentState = stateDictionary[newState];
        CurrentState.Enter();
    }

    public void AddState(PlayerStateEnum stateEnum, PlayerState playerState) {
        stateDictionary.Add(stateEnum, playerState);
    }
}
