using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected int animBoolHash;
    protected bool endTriggerCalled;

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName) {
        this.player = player;
        this.stateMachine = stateMachine;
        animBoolHash = Animator.StringToHash(animBoolName);
    }

    public virtual void Enter() {
        if (DialogueManager.instance.isEnd == false) {
            stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
        player.AnimatorCompo.SetBool(animBoolHash, true);
        endTriggerCalled = false;
    }

    public virtual void UpdateState() {
        if (DialogueManager.instance.isEnd == false) {
            stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    public virtual void Exit() {
        player.AnimatorCompo.SetBool(animBoolHash, false);;
    }

    public virtual void AnimationFinishTrigger() {
        endTriggerCalled = true;
    }
}
