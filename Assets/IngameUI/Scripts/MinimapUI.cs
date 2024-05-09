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
        CreateRoom(_mapSpawn.current.StartRoom, null, Door.DoorType.DoorMax);

        print($"map created: {_alreadyMap.Count}");
        foreach (var item in _alreadyMap)
        {
            print(item);
        }
    }

    void CreateRoom(BaseStage stage, RectTransform beforeState, Door.DoorType dir) {
        if (_alreadyMap.Contains(stage.StageNum)) return;

        Vector2 createPos = beforeState == null ? Vector2.zero : beforeState.transform.localPosition;

        if (beforeState != null) {
            print(beforeState);
            print(beforeState.transform);
            print(beforeState.transform as RectTransform);
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

        // boxTrm.rect.

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
}
