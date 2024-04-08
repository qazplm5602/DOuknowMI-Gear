using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GayManater : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private MapSpawner mapSpawner;

    public BaseStage CurrentRoom;

    private BaseStage StartRoom;

    private void Start()
    {
        mapSpawner = FindObjectOfType<MapSpawner>();
    }

    private void Update()
    {
        if (CurrentRoom != null && Input.anyKey)
        {
            BaseStage canMoveRoom = IsCanMoveRoom(new Vector2Int((int)Input.GetAxisRaw("Horizontal")
             , (int)Input.GetAxisRaw("Vertical")));
            if ((bool)canMoveRoom)
            {
                print("ming u moved room");
                MoveRoom(canMoveRoom);
            }
        }
    }

    [ContextMenu("자위중")]
    public void Ming()
    {
        var startRoom = mapSpawner.current.StartRoom;
        MoveRoom(startRoom);
    }

    public void MoveRoom(BaseStage targetRoom)
    {
        if (CurrentRoom != null)
            CurrentRoom.Exit();
        CurrentRoom = targetRoom;
        CurrentRoom.Enter();
        vcam.transform.position = targetRoom.transform.position + Vector3.back * 10;
    }

    public BaseStage IsCanMoveRoom(Vector2Int whereToMove)
    {
        if (CurrentRoom.RoomActive && CurrentRoom.Cleared)
        {
            if (CurrentRoom.StageLinkedData.LeftMap && whereToMove == new Vector2Int(-1, 0)) return CurrentRoom.StageLinkedData.LeftMap;
            if (CurrentRoom.StageLinkedData.RightMap && whereToMove == new Vector2Int(1, 0)) return CurrentRoom.StageLinkedData.RightMap;
            if (CurrentRoom.StageLinkedData.DownMap && whereToMove == new Vector2Int(0, -1)) return CurrentRoom.StageLinkedData.DownMap;
            if (CurrentRoom.StageLinkedData.UpMap && whereToMove == new Vector2Int(0, 1)) return CurrentRoom.StageLinkedData.UpMap;
        }
        return null;
    }
}
