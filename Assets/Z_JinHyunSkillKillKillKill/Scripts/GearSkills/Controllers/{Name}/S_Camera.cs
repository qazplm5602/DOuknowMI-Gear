using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCamera : SkillController
{
    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }
}
