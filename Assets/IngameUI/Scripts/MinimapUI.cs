using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapUI : MonoBehaviour
{
    [SerializeField] GameObject _roomBox;
    [SerializeField] Transform _mapSection;
    [SerializeField] float gapSize = 2f;

    MapSpawner _mapSpawn;
    HashSet<int> _alreadyMap;

    Dictionary<int, GameObject> stages;

    private void Awake() {
        _alreadyMap = new();
        stages = new();
        _mapSpawn = FindObjectOfType<MapSpawner>();
    }

    private void Start() {
        CreateRoom(_mapSpawn.current.StartRoom, null, Door.DoorType.DoorMax);

        // 중간으로 이동
        Vector2 sumPos = Vector2.zero;
        foreach (var item in stages)
        {
            // sumPos += (Vector2)item.Value.transform.localPosition;
            sumPos += (item.Value.transform as RectTransform).anchoredPosition;
        }

        sumPos /= stages.Count;
        (_mapSection as RectTransform).anchoredPosition = -sumPos;

        // 선 연결
        _alreadyMap.Clear();
        CreateLine(_mapSpawn.current.StartRoom);

        // print($"map created: {_alreadyMap.Count}");
        // foreach (var item in _alreadyMap)
        // {
        //     print(item);
        // }
    }

    void CreateRoom(BaseStage stage, RectTransform beforeState, Door.DoorType dir) {
        if (_alreadyMap.Contains(stage.StageNum)) return;

        Vector2 createPos = beforeState == null ? Vector2.zero : beforeState.transform.localPosition;

        if (beforeState != null) {
            Vector2 beforeRoomSize = (beforeState.transform as RectTransform).rect.size;
            switch (dir)
            {
                case Door.DoorType.Up:
                    createPos += (Vector2.up * beforeRoomSize.y) + (Vector2.up * gapSize);
                    break;
                case Door.DoorType.Down:
                    createPos += (-Vector2.up * beforeRoomSize.y) + (-Vector2.up * gapSize);
                    break;
                case Door.DoorType.Right:
                    createPos += (Vector2.right * beforeRoomSize.x) + (Vector2.right * gapSize);
                    break;
                case Door.DoorType.Left:
                    createPos += (-Vector2.right * beforeRoomSize.x) + (-Vector2.right * gapSize);
                    break;
                case Door.DoorType.DoorMax:
                    break;
            }
        }


        // 룸 만듬
        RectTransform boxTrm = Instantiate(_roomBox, _mapSection).transform as RectTransform;
        boxTrm.localPosition = createPos;
        boxTrm.gameObject.name = "room-"+stage.StageNum;
        
        stages[stage.StageNum] = boxTrm.gameObject; // 혹시 모르니 저장

        _alreadyMap.Add(stage.StageNum);
        
        if (stage.StageLinkedData.UpMap) {
            CreateRoom(stage.StageLinkedData.UpMap, boxTrm, Door.DoorType.Up);
        }
        if (stage.StageLinkedData.DownMap) {
            CreateRoom(stage.StageLinkedData.DownMap, boxTrm, Door.DoorType.Down);
        }
        if (stage.StageLinkedData.LeftMap) {
            CreateRoom(stage.StageLinkedData.LeftMap, boxTrm, Door.DoorType.Left);
        }
         if (stage.StageLinkedData.RightMap) {
            CreateRoom(stage.StageLinkedData.RightMap, boxTrm, Door.DoorType.Right);
        }
    }

    void CreateLine(BaseStage stageSys) {
        // RectTransform trm = stage.transform as RectTransform;
        RectTransform trm = stages[stageSys.StageNum].transform as RectTransform;
        // BaseStage stageSys = stage.GetComponent<BaseStage>();
        
        // if (_alreadyMap.Contains(stageSys.StageNum)) return;
        // print($"_alreadyMap add {stageSys.StageNum}");
        _alreadyMap.Add(stageSys.StageNum);

        // 세이브
        bool upCreate = stageSys.StageLinkedData.UpMap && !_alreadyMap.Contains(stageSys.StageLinkedData.UpMap.StageNum);
        bool downCreate = stageSys.StageLinkedData.DownMap && !_alreadyMap.Contains(stageSys.StageLinkedData.DownMap.StageNum);
        bool leftCreate = stageSys.StageLinkedData.LeftMap && !_alreadyMap.Contains(stageSys.StageLinkedData.LeftMap.StageNum);
        bool rightCreate = stageSys.StageLinkedData.RightMap && !_alreadyMap.Contains(stageSys.StageLinkedData.RightMap.StageNum);
        
        // print($"[{stageSys.StageNum}] up: {upCreate} ({stageSys.StageLinkedData?.UpMap} {!_alreadyMap.Contains(stageSys.StageLinkedData.UpMap?.StageNum ?? 0)}) down: {downCreate} ({stageSys.StageLinkedData?.DownMap} {!_alreadyMap.Contains(stageSys.StageLinkedData.DownMap?.StageNum ?? 0)}) left: {leftCreate} right: {rightCreate}");

        if (upCreate) {
            RectTransform targetTrm = stages[stageSys.StageLinkedData.UpMap.StageNum].transform as RectTransform;
            RectTransform line = CreateLineUI();
            
            Vector2 myEnd = (Vector2)trm.localPosition + new Vector2(0, trm.rect.yMax);
            Vector2 targetEnd = (Vector2)targetTrm.localPosition + new Vector2(0, targetTrm.rect.yMin);

            Vector2 center = (targetEnd + myEnd) / 2f;
            line.localPosition = center;
            line.sizeDelta = new Vector2(2, (targetEnd.y - myEnd.y) / 2f);
            // print($"{myEnd} / {targetEnd} / {line.rect.height / 2f}");

            CreateLine(stageSys.StageLinkedData.UpMap);
        }
        if (downCreate) {
            RectTransform targetTrm = stages[stageSys.StageLinkedData.DownMap.StageNum].transform as RectTransform;
            RectTransform line = CreateLineUI();
            
            Vector2 myEnd = (Vector2)trm.localPosition + new Vector2(0, trm.rect.yMin);
            Vector2 targetEnd = (Vector2)targetTrm.localPosition + new Vector2(0, targetTrm.rect.yMax);

            Vector2 center = (targetEnd + myEnd) / 2f;
            line.localPosition = center;
            line.sizeDelta = new Vector2(2, (myEnd.y - targetEnd.y) / 2f);
            // print($"{myEnd} / {targetEnd} / {line.rect.height / 2f}");

            CreateLine(stageSys.StageLinkedData.DownMap);
        }
        if (leftCreate) {
            RectTransform targetTrm = stages[stageSys.StageLinkedData.LeftMap.StageNum].transform as RectTransform;
            RectTransform line = CreateLineUI();
            
            Vector2 myEnd = (Vector2)trm.localPosition + new Vector2(trm.rect.xMin, 0);
            Vector2 targetEnd = (Vector2)targetTrm.localPosition + new Vector2(targetTrm.rect.xMax, 0);

            Vector2 center = (targetEnd + myEnd) / 2f;
            line.localPosition = center;
            line.sizeDelta = new Vector2((myEnd.x - targetEnd.x) / 2f, 2);
            // print($"{myEnd} / {targetEnd} / {line.rect.height / 2f}");

            CreateLine(stageSys.StageLinkedData.LeftMap);
        }
        if (rightCreate) {
            RectTransform targetTrm = stages[stageSys.StageLinkedData.RightMap.StageNum].transform as RectTransform;
            RectTransform line = CreateLineUI();
            
            Vector2 myEnd = (Vector2)trm.localPosition + new Vector2(trm.rect.xMax, 0);
            Vector2 targetEnd = (Vector2)targetTrm.localPosition + new Vector2(targetTrm.rect.xMin, 0);
            // myEnd.y -= line.rect.height / 2f;
            // targetEnd.y -= line.rect.height / 2f;

            Vector2 center = (targetEnd + myEnd) / 2f;
            line.localPosition = center;
            line.sizeDelta = new Vector2((targetEnd.x - myEnd.x) / 2f, 2);
            // print($"{myEnd} / {targetEnd} / {line.rect.height / 2f}");

            CreateLine(stageSys.StageLinkedData.RightMap);
        }
    }

    RectTransform CreateLineUI() {
        GameObject lineEntity = new GameObject("line");
        lineEntity.transform.SetParent(_mapSection);

        lineEntity.AddComponent<Image>();
        
        return lineEntity.transform as RectTransform;
    }
}