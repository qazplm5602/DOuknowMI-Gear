using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        player.isAttack = true;
        var gearList = GearManager.Instance.GetGearResult();
        foreach (var iter in gearList) {
            iter.script.Use();
        }
        GearManager.Instance.StartRoll(null);
        stateMachine.ChangeState(PlayerStateEnum.Idle);
    }

/*     public override void UpdateState() {
        HandleMovementEvent();
        if (endTriggerCalled) {
            stateMachine.ChangeState(PlayerStateEnum.Idle);
            player.isAttack = false;
        }
    }

    private void HandleMovementEvent() {
        player.MovementCompo.SetMovement(player.InputReader._xMovement * player.moveSpeed, true);
    } */
}
