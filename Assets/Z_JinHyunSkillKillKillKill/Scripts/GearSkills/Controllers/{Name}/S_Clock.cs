using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillClock : SkillController
{
    private float _slowPercent = 0.5f;

    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }

    protected override IEnumerator MoveRoutine(Transform startTrm)
    {
        
        yield return StartCoroutine(base.MoveRoutine(startTrm));
    }
}
