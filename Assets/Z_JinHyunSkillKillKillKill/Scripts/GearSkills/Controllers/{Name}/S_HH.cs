using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHH : SkillController
{
    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }
}
