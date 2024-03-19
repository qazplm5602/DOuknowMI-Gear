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

struct GearLinkDTO {
    public string id; // 기어 연계 ID ex) One,Two 
    public GearLinkSO data; // 연계 SO
    public GearCogEvent script; // 연게 SO 인스턴스 모듈
    public int[] gearIdx; // 연계된 기어 인덱스
}

struct GearSkillDTO {
    public string id;
    public GearSO data;
    public GearCogEvent script;
    public int gearIdx;
}

public class GearManager : MonoBehaviour
{
    [SerializeField] Transform section;
    [SerializeField] GearScriptModule scriptModule;
    [SerializeField] GearSO[] spawnGears;
    [SerializeField] Vector2[] spawnGearCoords;
    
    [SerializeField] GearLinkSO[] loadLinkData;
    Dictionary<string, GearLinkSO> linkDataSO; // id -> 기어연계SO 가져오는 용도

    [SerializeField] GameObject gearCircle;

    GearInfo[] gears;


    public GameObject _player;

    private void Start() {
        Init();
    }

    void Init() {
        /////////// 기어 소환
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
    
        /////////// 연계 SO 로드
        linkDataSO = new();

        foreach (var item in loadLinkData)
        {
            if (item.LoadModule == null) continue;

            string id = item.GetId();
            linkDataSO[id] = item;
            scriptModule.LoadModule(GearScriptModule.Type.Link, id, item.LoadModule);
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
        HashSet<int> ignoreIdx = new();
        GearLinkDTO[] ResultLink() {
            print("ResultLink / ignore: "+ignoreIdx.Count);
            
            List<GearLinkDTO> result = new();
            List<int[]> linkIndex = new();
        
            List<int> linkBox = null;
            int i = 0;
            foreach(var gear in gears) {
                if (gear.system.currentCogType == CogType.Link && !ignoreIdx.Contains(i) /* 포함 안되어있어야 됨 */) {
                    if (linkBox == null)
                        linkBox = new();

                    linkBox.Add(i);
                } else if (linkBox != null) {
                    if (linkBox.Count > 1)
                        linkIndex.Add(linkBox.ToArray());
                        
                    linkBox = null;
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
                
                GearCogEvent script;
                if (!linkDataSO.TryGetValue(linkID, out var linkSO) || (script = scriptModule.GetLinkScript(linkID)) == null) {
                    ignoreIdx.Add(linkSection[linkSection.Length - 1]); // 마지막꺼 무사 해
                    return ResultLink(); // 이거 아직 못찾으면 하나 무시해서 다시 찾을꺼임 (재귀함수 밍밍)
                }

                result.Add(new() {
                    id = linkID,
                    data = linkSO,
                    script  = script,
                    gearIdx = linkSection
                });
            }

            return result.ToArray();
        }
        GearSkillDTO[] ResultSkill() {
            List<GearSkillDTO> result = new();
            
            int i = 0;
            foreach (var gear in gears)
            {
                if (gear.system.currentCogType == CogType.Skill)
                    result.Add(new() {
                        id = gear.data.id,
                        data = gear.data,
                        script = scriptModule.GetSkillScript(gear.data.id),
                        gearIdx = i
                    });
                
                i++;
            }

            return result.ToArray();
        }

        GearLinkDTO[] linkIndex = ResultLink();
        if (linkIndex.Length == 0) { // 연계된 기어가 하나도 없으면
            GearSkillDTO[] skillResult = ResultSkill();

            print("---------------- skills");
            for (int i = 0; i < skillResult.Length; i++)
            {
                print($"[{skillResult[i].gearIdx}] {skillResult[i].id}");
            }
        }

        // 디버깅
        print("---------------- links");
        for (int i = 0; i < linkIndex.Length; i++)
        {
            print("["+i+"] " + String.Join(", ", linkIndex[i].id));
        }
    }
}
