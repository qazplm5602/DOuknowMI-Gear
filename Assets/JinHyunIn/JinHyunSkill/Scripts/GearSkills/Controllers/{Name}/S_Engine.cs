using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEngine : SkillController
{
    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }
}
