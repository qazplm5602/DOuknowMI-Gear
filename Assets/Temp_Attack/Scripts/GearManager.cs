using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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


    public GameObject _player;

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

            if (item.LoadModule) {
                var script = scriptModule.LoadModule(GearScriptModule.Type.Skill, item.id, item.LoadModule);
                script._player = _player; // 플레이어 알려줌
            }

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
        List<int[]> ResultLink() {
            List<int[]> linkIndex = new();
        
            List<int> linkBox = null;
            int i = 0;
            foreach(var gear in gears) {
                if (gear.system.currentCogType == CogType.Link) {
                    if (linkBox == null)
                        linkBox = new();

                    linkBox.Add(i);
                } else {
                    if (linkBox != null) {
                        if (linkBox.Count > 1) {
                            linkIndex.Add(linkBox.ToArray());
                        }
                        linkBox = null;
                    }
                }

                ++i;
            }

            if (linkBox != null && linkBox.Count > 1)
                linkIndex.Add(linkBox.ToArray());

            foreach (var linkSection in linkIndex)
            {
                List<string> cogIds = new();
                foreach (var item in linkSection)
                    cogIds.Add(gears[item].data.id);

                // 정렬 하고 합쳐서 ID 만듬 ( 연계SO도 저렇게 찾을꺼 )
                string linkID = String.Join(",", cogIds.OrderBy(v => v).ToArray());
                print(linkID);
            }

            return linkIndex;
        }

        List<int[]> linkIndex = ResultLink();

        // 디버깅
        print("---------------- links");
        for (int i = 0; i < linkIndex.Count; i++)
        {
            print("["+i+"] " + String.Join(", ", linkIndex[i].ToArray()));
        }
    }
}
