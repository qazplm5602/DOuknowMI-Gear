using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallImpact : PoolableMono
{
    public override void ResetItem()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        PoolManager.Instance.Push(this);
    }
}
