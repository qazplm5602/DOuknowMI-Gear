using System.Collections;
using UnityEngine;

public class Smoke : PoolableMono
{
    private Transform playerTrm;
    private bool endCall = false;
    private Animator anim;
    private readonly int smokeHash = Animator.StringToHash("Smoke");

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    public override void ResetItem()
    {
/*         if (PlayerManager.instance.player.MovementCompo.Velocity.x == 0) {
            PoolManager.Instance.Push(this);
            return;
        } */
        anim.speed = 2;
        playerTrm = PlayerManager.instance.playerTrm;
        StartCoroutine(SmokeRoutine());
    }

    public void AnimationEnd() {
        endCall = true;
    }

    private IEnumerator SmokeRoutine() {
        transform.position = playerTrm.position + new Vector3(0, -0.7f, 0);
        transform.eulerAngles = playerTrm.eulerAngles;
        yield return new WaitUntil(() => endCall == true);
        endCall = false;
        PoolManager.Instance.Push(this);
        yield return null;
    }
}
