using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct GearInfo {
    public GearSO data;
    public GameObject entity;
    public GearCircle system;
}

public class GearManager : MonoBehaviour
{
    [SerializeField] Transform section;
    [SerializeField] GearScriptModule scriptModule;
    [SerializeField] GearSO[] spawnGears;
    [SerializeField] Vector2[] spawnGearCoords;

    [SerializeField] GameObject gearCircle;

    GearInfo[] gears;

    private void Start() {
        Init();
    }

    void Init() {
        gears = new GearInfo[spawnGears.Length];

        int i = 0;
        bool isReverse = false;
        foreach (var item in spawnGears)
        {
            var gear = Instantiate(gearCircle, section);
            var gear_transform = gear.GetComponent<RectTransform>();
            gear_transform.anchoredPosition = spawnGearCoords[i];
            
            var gearD = gears[i] = new GearInfo() { 
                data = item,
                entity = gear,
                system = gear.GetComponent<GearCircle>()
            };

            gearD.system.gearSO = gearD.data;
            gearD.system.reverse = isReverse;
            gearD.system.Init();

            isReverse = !isReverse;
            ++i;
        }
    }

    int rollFinishRoll = 0;
    Action roolFinishCb;
    void RollFinish() {
        if (roolFinishCb == null) return;
        
        ++rollFinishRoll;
        if (gears.Length != rollFinishRoll) return;

        roolFinishCb.Invoke();
        roolFinishCb = null;
    }

    public void StartRoll(Action cb) {
        rollFinishRoll = 0;
        roolFinishCb = cb;

        foreach (var gear in gears)
            gear.system.Run(RollFinish);
    }

    public void GetGearResult() {

    }
}
