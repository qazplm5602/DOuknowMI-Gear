using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillClock : SkillController
{
    private void Start()
    {
        SoundManager.Instance.PlaySound(SoundType.ClockTicSound);
        StartCoroutine(MoveRoutine(transform));
    }

    protected override IEnumerator MoveRoutine(Transform startTrm)
    {
        print("moving");
        foreach (StatType t in _willModify)
        {
            ModifyEnemyStat(_debuffValue, t, _debuffTime);
        }
        yield return StartCoroutine(base.MoveRoutine(startTrm));
    }
}
