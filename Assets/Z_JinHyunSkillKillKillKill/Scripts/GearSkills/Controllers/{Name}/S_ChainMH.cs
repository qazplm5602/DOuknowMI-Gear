using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillChainMH : SkillController
{
    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }

    
}