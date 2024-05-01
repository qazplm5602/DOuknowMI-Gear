using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMH : SkillController
{
    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }
}
