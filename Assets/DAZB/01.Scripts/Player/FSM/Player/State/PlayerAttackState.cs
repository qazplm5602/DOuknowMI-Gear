using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        if (GearManager.Instance == null) {
            Debug.Log("Gear manager is null");
            stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
        player.isAttack = true;
        player.lastAttackTime = Time.time;
        var gearList = GearManager.Instance.GetGearResult();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));  
        Vector3 playerPos = player.transform.position;
        float angle = Mathf.Atan2(mousePosition.y - playerPos.y, mousePosition.x - playerPos.x) * Mathf.Rad2Deg;
        if (player.MovementCompo.Velocity.magnitude <= 0) {
            if (angle < 90) {
                player.MovementCompo.Flip(Vector2.zero, true, true);  
            }
            else {
                player.MovementCompo.Flip(Vector3.zero, true, false);
            }
        }
        foreach (var iter in gearList) {
            iter.script.Use();
        }
        GearManager.Instance.StartRoll(null);
        stateMachine.ChangeState(PlayerStateEnum.Idle);
    }

    public override void Exit() {
        player.isAttack = false;
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
