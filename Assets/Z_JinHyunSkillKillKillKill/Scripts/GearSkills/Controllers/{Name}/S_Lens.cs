using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLens : SkillController
{
    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }
}
