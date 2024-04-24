using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillClock : SkillController
{
    private float _slowPercent = 0.7f;

    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }

    protected override IEnumerator MoveRoutine(Transform startTrm)
    {
        print("moving");
        ModifyEnemyStat(_slowPercent, StatType.AttackSpeed, _debuffTime);
        ModifyEnemyStat(_slowPercent, StatType.MoveSpeed, _debuffTime);
        yield return StartCoroutine(base.MoveRoutine(startTrm));
    }
}
