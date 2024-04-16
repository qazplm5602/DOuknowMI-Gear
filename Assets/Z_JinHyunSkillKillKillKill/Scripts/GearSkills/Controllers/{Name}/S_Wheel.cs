using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWheel : SkillController
{
    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }
}
