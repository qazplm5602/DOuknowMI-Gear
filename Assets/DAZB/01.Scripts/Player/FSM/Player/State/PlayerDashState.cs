using System.Collections;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    private Vector3 dashDir;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        dashDir = (mousePosition - player.transform.position).normalized;
        player.MovementCompo.Flip(mousePosition);
        player.StartCoroutine(DashCor());
        player.isInvincibility = true;
    }

    public override void Exit()
    {
        player.isInvincibility = false;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (endTriggerCalled) {
            stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    private IEnumerator DashCor() {
        if (player.isDash) yield break;
        player.isDash = true;
        player.RigidCompo.AddForce(dashDir * player.dashPower, ForceMode2D.Impulse);
        player.RigidCompo.gravityScale = 0;
        yield return new WaitForSeconds(0.2f);
        player.RigidCompo.velocity = Vector2.zero;
        player.RigidCompo.gravityScale = 3;
        player.isDash = false;
    }
}
