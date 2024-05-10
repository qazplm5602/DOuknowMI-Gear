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
        if (Time.timeScale == 0) return;
        base.Enter();
        if (player.isUnderJumpping) {
            player.isUnderJumpping = false;
        }
        player.gameObject.layer = LayerMask.NameToLayer("PlayerDash");
        player.InputReader.InputDisable();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        dashDir = (mousePosition - player.transform.position).normalized;
        //player.MovementCompo.Flip(mousePosition);   
        Vector3 playerPos = player.transform.position;
        float angle = Mathf.Atan2(mousePosition.y - playerPos.y, mousePosition.x - playerPos.x) * Mathf.Rad2Deg;
        if (angle < 90) {
            player.MovementCompo.Flip(Vector2.zero, true, true);  
        }
        else {
            player.MovementCompo.Flip(Vector3.zero, true, false);
        }
        player.StartCoroutine(DashCor());
        player.StartCoroutine(GenerateAfterimageRoutine());
        player.isInvincibility = true;
        player.lastDashTime = Time.time;
        SoundManager.Instance.PlaySound(SoundType.PlayerDash);
    }

    public override void Exit()
    {
        player.isInvincibility = false;
        player.gameObject.layer = LayerMask.NameToLayer("Player");
        player.StartDelayCallback(0.1f, () => player.InputReader.InputEnable());
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

    private IEnumerator GenerateAfterimageRoutine() {
        float currentTime = 0;
        while (true) {
            if (endTriggerCalled) {
                yield break;
            }
            if (currentTime >= 0.05) {
                PoolManager.Instance.Pop(PoolingType.Afterimage);
                currentTime = 0;
                continue;
            }
            currentTime += Time.deltaTime;
            yield return null;
        }
    }
}
