using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSteamLoco : SkillController
{
    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }
}
