using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        CreateRoom(_mapSpawn.current.StartRoom, Vector2.zero);

        print($"map created: {_alreadyMap.Count}");
        foreach (var item in _alreadyMap)
        {
            print(item);
        }
    }

    void CreateRoom(BaseStage stage, Vector2 nowPos) {
        print(nowPos);
        if (_alreadyMap.Contains(stage.StageNum)) return;

        // 룸 만듬
        RectTransform boxTrm = Instantiate(_roomBox, _mapSection).transform as RectTransform;
        boxTrm.localPosition = nowPos;
        boxTrm.gameObject.name = "room-"+stage.StageNum;
        
        stages[stage.StageNum] = boxTrm.gameObject; // 혹시 모르니 저장

        // boxTrm.rect.

        _alreadyMap.Add(stage.StageNum);
        
        Vector2 size = boxTrm.rect.size;

        if (stage.StageLinkedData.UpMap) {
            CreateRoom(stage.StageLinkedData.UpMap, nowPos + (Vector2.up * size.y) + (Vector2.up * gapSize));
        }
        if (stage.StageLinkedData.DownMap) {
            CreateRoom(stage.StageLinkedData.DownMap, nowPos + (-Vector2.up * size.y) + (-Vector2.up * gapSize));
        }
        if (stage.StageLinkedData.LeftMap) {
            CreateRoom(stage.StageLinkedData.LeftMap, nowPos + (-Vector2.right * size.x) + (-Vector2.right * gapSize));
        }
         if (stage.StageLinkedData.RightMap) {
            CreateRoom(stage.StageLinkedData.RightMap, nowPos + (Vector2.right * size.x) + (Vector2.right * gapSize));
        }
    }
}
