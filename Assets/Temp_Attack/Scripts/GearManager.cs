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

public struct GearCogResultDTO {
    public CogType type;
    public GearCogEvent script;
    public int[] gearIdx;
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

    List<GearInfo> gears;


    public GameObject _player;

    private void Start() {
        Init();
    }

    void Init() {
        /////////// 기어 소환
        gears = new();

        foreach (var item in spawnGears)
            GearAdd(item);
    
        /////////// 연계 SO 로드
        linkDataSO = new();

        // combine 길이가 큰것부터 ㄱㄱㄱ
        Array.Sort(loadLinkData, (a, b) => {
            if (a.Combine.Length < b.Combine.Length) return 1;
            else if (a.Combine.Length > b.Combine.Length) return -1;
            else return 0;
        });

        foreach (var item in loadLinkData)
        {
            if (item.LoadModule == null) continue;

            string id = item.GetId();
            linkDataSO[id] = item;
            var script = scriptModule.LoadModule(GearScriptModule.Type.Link, id, item.LoadModule);
            script._player = _player; // 플레이어 알려줌
        }
    }
        

    public void GearAdd(GearSO data) {
        var myIdx = gears.Count;
        var gear = Instantiate(gearCircle, section);
        var gear_transform = gear.GetComponent<RectTransform>();
        gear_transform.anchoredPosition = spawnGearCoords[myIdx];
        
        var gearD = new GearInfo() { 
            data = data,
            entity = gear,
            system = gear.GetComponent<GearCircle>()
        };
        gears.Add(gearD);

        if (data.LoadModule) {
            var script = scriptModule.LoadModule(GearScriptModule.Type.Skill, data.id, data.LoadModule);
            script._player = _player; // 플레이어 알려줌
        }

        gearD.system.gearSO = data;
        gearD.system.reverse = (myIdx % 2) != 0;
        gearD.system.Init();
    }
    public void GearRemove(int idx) {
        var gear = gears[idx];
        Destroy(gear.entity);
        
        gears.RemoveAt(idx);

        for (int i = idx; i < gears.Count; i++)
        {
            var gearD = gears[i];
            var gearTrm = gearD.entity.GetComponent<RectTransform>();
            gearTrm.anchoredPosition = spawnGearCoords[i];
            gearD.system.reverse = !gearD.system.reverse;
        }
    }

    int rollFinishRoll = 0;
    Action roolFinishCb;
    void RollFinish() {
        if (roolFinishCb == null) return;
        
        ++rollFinishRoll;
        if (gears.Count != rollFinishRoll) return;

        roolFinishCb.Invoke();
        roolFinishCb = null;
    }

    public void StartRoll(Action cb) {
        rollFinishRoll = 0;
        roolFinishCb = cb;

        foreach (var gear in gears)
            gear.system.Run(RollFinish);
    }

    public GearCogResultDTO[] GetGearResult() {
        GearLinkDTO[] ResultLink(out GearSkillDTO[] responseSkillGear) {
            List<GearLinkDTO> result = new();
            Dictionary<GearSO, int> cacheGear = new();

            // 기어 캐싱
            int i = 0;
            foreach(var gear in gears) {
                if (gear.system.currentCogType == CogType.Link)
                    cacheGear[gear.data] = i;

                ++i;
            }

            bool isFaild;
            bool firstGearFlag = false; // 첫번째 기어 연계 조합 됨?
            foreach (var item in loadLinkData)
            {
                isFaild = false;
                List<int> combineIdx = new(); // 연계된 기어 인덱스

                foreach (var gear in item.Combine) {
                    if (!cacheGear.TryGetValue(gear, out var idx)) { // 아니 연계된 기어가 없음
                        isFaild = true;
                        break;
                    } 

                    combineIdx.Add(idx);
                }
                if (isFaild) continue;

                // 일단 완료했으면 제외할꺼 추가
                foreach (var idx in combineIdx)
                    if (idx != 0) { // 0번째는 제외 안함
                        cacheGear.Remove(gears[idx].data);
                    } else firstGearFlag = true;
                        

                string id = item.GetId();
                result.Add(new GearLinkDTO() {
                    id = id,
                    data = item,
                    script = scriptModule.GetLinkScript(id),
                    gearIdx = combineIdx.ToArray()
                });
            }

            // 조합이 안된 기어 확인
            List<GearSkillDTO> skillResult = new();
            foreach (var item in cacheGear) {
                if (item.Value == 0 && firstGearFlag) continue; // 첫번째 기어는 이미 조합 됨
                
                var id = item.Key.id;
                skillResult.Add(new() {
                    id = id,
                    data = item.Key,
                    script = scriptModule.GetSkillScript(id),
                    gearIdx = item.Value
                });
            }

            responseSkillGear = skillResult.ToArray();
            return result.ToArray();
        }
        GearSkillDTO[] ResultSkill() {
            List<GearSkillDTO> result = new();
            
            int i = 0;
            foreach (var gear in gears)
            {
                if (gear.system.currentCogType == CogType.Skill || gear.system.currentCogType == CogType.Link /* 어차피 이 함수를 실행하는건 연계된 콕이 없으니 연계된 기어들은 다 스킬로 ㄱㄱ */)
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

        List<GearCogResultDTO> result = new();
        GearLinkDTO[] linkIndex = ResultLink(out GearSkillDTO[] linkFormatSkill /* Cog가 연계이지만 조합은 안된 기어들은 스킬로 감 */);
        if (linkIndex.Length == 0) { // 연계된 기어가 하나도 없으면
            GearSkillDTO[] skillResult = ResultSkill();
            
            foreach (var item in skillResult)
            {
                result.Add(new() {
                    type = CogType.Skill,
                    script = item.script,
                    gearIdx = new int[1] { item.gearIdx }
                });
            }

            // Cog가 연계인것들도 다 스킬로 넘겨야함 (연계가 조합된게 없어서.)
            return result.ToArray();
        }

        foreach (var item in linkIndex)
            result.Add(new() {
                type = CogType.Link,
                script = item.script,
                gearIdx = item.gearIdx
            });

        // 스킬도 같이 보냄
        foreach (var item in linkFormatSkill)
            result.Add(new() {
                type = CogType.Skill,
                script = item.script,
                gearIdx = new int[1] { item.gearIdx }
            });

        return result.ToArray();
    }
}
