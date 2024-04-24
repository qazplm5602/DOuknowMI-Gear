using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillClock : SkillController
{
    private float _slowPercent = 0.1f;

    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }

    protected override IEnumerator MoveRoutine(Transform startTrm)
    {
        print("moving");

        ModifyEnemyStat(StatType.AttackSpeed, _slowPercent, true);
        ModifyEnemyStat(StatType.MoveSpeed, _slowPercent, true);
        yield return StartCoroutine(base.MoveRoutine(startTrm));
        ModifyEnemyStat(StatType.AttackSpeed, _slowPercent, false);
        ModifyEnemyStat(StatType.MoveSpeed, _slowPercent, false);
    }
}
