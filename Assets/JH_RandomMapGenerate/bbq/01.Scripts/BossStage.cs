using FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStage : BaseStage
{
    [SerializeField] private Enemy Boss;
    [SerializeField] private GameObject TIMELINE;

    public Transform RewardPosition;
    public InteractiveObject RewardChest;
    public override void Enter()
    {
        TIMELINE.SetActive(true);
        base.Enter();
        Cleared = false;
    }

    public void OnEnable()
    {
        OnClearChanged += SpawnReward;
    }

    public void OnDisable()
    {
        OnClearChanged -= SpawnReward;
    }

    public void SpawnReward(bool _)
    {
        if (!(Cleared == true && _)) return;
        if (RewardChest == null) return;
        GearChest dick = Instantiate(RewardChest, transform) as GearChest;
        dick.transform.position = RewardPosition.position;
    }

}
