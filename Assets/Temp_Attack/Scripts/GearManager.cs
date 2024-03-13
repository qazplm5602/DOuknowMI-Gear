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
            gearD.system.Init();

            ++i;
        }
    }
}
