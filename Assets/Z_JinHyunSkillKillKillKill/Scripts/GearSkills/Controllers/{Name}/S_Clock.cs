using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillClock : SkillController
{
    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }
}
