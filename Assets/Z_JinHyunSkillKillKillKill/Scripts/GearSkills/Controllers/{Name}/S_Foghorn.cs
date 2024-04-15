using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFoghorn : SkillController
{
    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }
}
