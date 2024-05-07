using FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStage : BaseStage
{
    [SerializeField] private Enemy Boss;
    [SerializeField] private GameObject TIMELINE;
    public override void Enter()
    {
        TIMELINE.SetActive(true);
        base.Enter();
    }
}
