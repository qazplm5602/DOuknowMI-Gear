using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShell : SkillController
{
    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }
}