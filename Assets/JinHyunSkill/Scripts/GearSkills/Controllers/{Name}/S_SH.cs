using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSH : SkillController
{
    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }
}
