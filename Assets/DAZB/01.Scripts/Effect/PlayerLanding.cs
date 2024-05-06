using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLanding : PoolableMono
{
    public override void ResetItem()
    {
        transform.position = PlayerManager.instance.player.transform.position + new Vector3(-0.2f, -0.7f);
    }

    private void AnimationEnd() {
        PoolManager.Instance.Push(this);
    }
}
