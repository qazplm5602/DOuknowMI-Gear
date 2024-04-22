using System.Collections;
using UnityEngine;

public class Smoke : PoolableMono
{
    [SerializeField] private Transform pos;
    private Transform playerTrm;
    private bool endCall = false;
    private Animator anim;
    private readonly int smokeHash = Animator.StringToHash("Smoke");

    public override void ResetItem()
    {
/*         if (PlayerManager.instance.player.MovementCompo.Velocity.x == 0) {
            PoolManager.Instance.Push(this);
            return;
        } */
        anim = GetComponent<Animator>();
        //anim.speed = 2;
        playerTrm = PlayerManager.instance.playerTrm;
        StartCoroutine(SmokeRoutine());
        print("dddDdd");
    }

    public void AnimationEnd() {
        endCall = true;
    }

    private IEnumerator SmokeRoutine() {
        transform.position = playerTrm.position;
        transform.eulerAngles = playerTrm.eulerAngles;
        yield return new WaitUntil(() => endCall == true);
        endCall = false;
        PoolManager.Instance.Push(this);
        yield return null;
    }
}
