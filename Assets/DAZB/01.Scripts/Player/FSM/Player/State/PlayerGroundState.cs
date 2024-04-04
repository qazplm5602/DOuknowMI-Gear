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
        if (player.MovementCompo.isGround == false) {
            stateMachine.ChangeState(PlayerStateEnum.Fall);
        }
    }

    private void HandleJumpEvent()
    {
        if (player.MovementCompo.isGround) {
            stateMachine.ChangeState(PlayerStateEnum.Jump);
        }
    }
}
