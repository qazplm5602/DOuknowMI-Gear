using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCar : SkillController
{
    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }
}