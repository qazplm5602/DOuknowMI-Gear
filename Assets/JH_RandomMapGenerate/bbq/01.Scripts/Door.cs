using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorType { Up, Down, Right, Left, DoorMax };
    private Dictionary<DoorType, DoorType> OppoDir = new Dictionary<DoorType, DoorType>()
    {
        {DoorType.Up, DoorType.Down },
        {DoorType.Right, DoorType.Left },
        {DoorType.Down, DoorType.Up },
        {DoorType.Left, DoorType.Right },
    };

    public DoorType Type;
    
    private BaseStage stageData;

    private GameObject nextRoom;

    public void Awake()
    {
        
    }

    private void OnEnable()
    {
        GameObject stage = gameObject;
        int tries = 0, maxTries = 25;
        while (!stage.CompareTag("Stage"))
        {
            stage = stage.transform.parent.gameObject;
            tries++;
        }
        if (tries > maxTries)
            return;

        stageData = stage.GetComponent<BaseStage>();

        switch (Type)
        {
            case DoorType.Up:
                nextRoom = stageData.StageLinkedData.UpMap;
                break;
            case DoorType.Down:
                nextRoom = stageData.StageLinkedData.DownMap;
                break;
            case DoorType.Right:
                nextRoom = stageData.StageLinkedData.RightMap;
                break;
            case DoorType.Left:
                nextRoom = stageData.StageLinkedData.LeftMap;
                break;
        }


    }

    public void Teleport()
    {
        stageData.Exit();
        nextRoom.GetComponent<BaseStage>().Enter();
        //텔레포트
        //plr.transform.position = nextRoom.GetComponent<BaseStage>().door[(int)OppoDir[Type]].transform.position;
    }
}
