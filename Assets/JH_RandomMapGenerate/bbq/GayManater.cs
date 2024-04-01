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
        //IsCanMoveRoom(new Vector2Int(Input.GetAxisRaw("Horizontal")
        //    , Input.GetAxisRaw("Vertical"));
    }

    [ContextMenu("ÀÚÀ§Áß")]
    public void Ming()
    {
        var startRoom = mapSpawner.current.StartRoom;
        MoveRoom(startRoom);
    }

    public void MoveRoom(BaseStage targetRoom)
    {
        CurrentRoom.Exit();
        CurrentRoom = targetRoom;
        CurrentRoom.Enter();
        vcam.transform.position = targetRoom.transform.position + Vector3.back * 10;
    }

    public bool IsCanMoveRoom(Vector2Int whereToMove)
    {
        if (CurrentRoom.RoomActive && CurrentRoom.Cleared)
        {
            if (CurrentRoom.StageLinkedData.LeftMap && whereToMove == new Vector2Int(-1, 0)) return true; 
            if (CurrentRoom.StageLinkedData.RightMap && whereToMove == new Vector2Int(1, 0)) return true; 
            if (CurrentRoom.StageLinkedData.DownMap && whereToMove == new Vector2Int(0, -1)) return true; 
            if (CurrentRoom.StageLinkedData.UpMap && whereToMove == new Vector2Int(0, 1)) return true; 
        }
        return false;
    }
}
