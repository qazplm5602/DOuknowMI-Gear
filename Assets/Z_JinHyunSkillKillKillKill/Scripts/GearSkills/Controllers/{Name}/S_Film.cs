using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFilm  : SkillController
{
    private void Start()
    {
        StartCoroutine(MoveRoutine(transform));
    }
}
