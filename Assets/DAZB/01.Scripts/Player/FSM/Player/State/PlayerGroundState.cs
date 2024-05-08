using Unity.VisualScripting;
using UnityEngine.InputSystem;

public abstract class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter() {
        base.Enter();
        player.InputReader.JumpEvent += HandleJumpEvent;
    }
    
    public override void Exit() {
        player.InputReader.JumpEvent -= HandleJumpEvent;
        base.Exit();
    }
    
    public override void UpdateState() {
        base.UpdateState();
        if (DialogueManager.Instance == null) {
            if (player.MovementCompo.isGround == false) {
                //SoundManager.Instance.PlaySound(SoundType.PlayerLending);
                stateMachine.ChangeState(PlayerStateEnum.Fall);
            }
        }
        else {
            if (player.MovementCompo.isGround == false && DialogueManager.Instance.isEnd == true) {
                //SoundManager.Instance.PlaySound(SoundType.PlayerLending);
                stateMachine.ChangeState(PlayerStateEnum.Fall);
            }
        }
    }

    private void HandleAttackEvent() {
        
    }

    private void HandleJumpEvent()
    {
        if (DialogueManager.Instance == null) {
            if (player.MovementCompo.isGround && !player.isDead && !player.ishurt) {
                stateMachine.ChangeState(PlayerStateEnum.Jump);
            }
        }
        else {
            if (player.MovementCompo.isGround && DialogueManager.Instance.isEnd && !player.isDead && !player.ishurt) {
                stateMachine.ChangeState(PlayerStateEnum.Jump);
            }
        }
    }
}
