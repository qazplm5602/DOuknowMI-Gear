using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.isAttack = true;
    }

    public override void UpdateState() {
        if (endTriggerCalled) {
            stateMachine.ChangeState(PlayerStateEnum.Idle);
            player.isAttack = false;
        }
    }
}
