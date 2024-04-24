using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionState : PlayerState
{
    public PlayerInteractionState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        DialogueManager.Instance.isEnd = false;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(DialogueManager.instance.isEnd) {
            stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        DialogueManager.Instance.isEnd = false;
    }
}
