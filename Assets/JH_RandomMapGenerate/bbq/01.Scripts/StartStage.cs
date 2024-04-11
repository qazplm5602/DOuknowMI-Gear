using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStage : BaseStage
{
    public Transform SpawnPoint;
    private void Awake()
    {
        base.Cleared = true;
    }
    public override void Enter()
    {
        base.Enter();
    }
}
    