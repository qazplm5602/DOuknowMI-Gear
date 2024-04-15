using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCoal : SkillController
{
    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }
}
