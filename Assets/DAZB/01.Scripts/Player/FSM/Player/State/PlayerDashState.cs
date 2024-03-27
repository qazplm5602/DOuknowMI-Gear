using System.Collections;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    private Vector3 dashDir;
    private float dashTime = 0.1f;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        dashDir = (mousePosition - player.transform.position).normalized;
        player.StartCoroutine(DashCor());
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
        yield return new WaitForSeconds(0.1f);
        player.RigidCompo.velocity = Vector2.zero;
        player.isDash = false;
    }
}
