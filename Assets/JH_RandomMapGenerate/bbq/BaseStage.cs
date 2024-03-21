using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseStage : MonoBehaviour
{
    public Door[] door;
    public ROOMTYPE type;

    public int StageNum;

    public LinkedStage StageLinkedData;

    public GameObject Arena; //적의 스폰 Parent

    public bool Cleared = false;
    public bool RoomActive = false;

    public void LoadDoors()
    {
        door = new Door[(int)Door.DoorType.DoorMax];
        for (Door.DoorType i = Door.DoorType.Up; i < Door.DoorType.DoorMax; i++)
        {
            door[(int)i] = null;
        }

        Door[] temp = GetComponentsInChildren<Door>();
        foreach (var i in temp)
        {
            door[(int)i.Type] = i;
            i.gameObject.SetActive(false);
        }
    }


    public void Initialize()
    {
        Arena = gameObject;

        if (StageLinkedData.RightMap != null)
        {
            if (door[(int)Door.DoorType.Right] != null)
            {
                door[(int)Door.DoorType.Right].gameObject.SetActive(true);
               // door[(int)Door.DoorType.Right].CreateDoor(this.gameObject);
            }
        }
        if (StageLinkedData.LeftMap != null)
        {
            if (door[(int)Door.DoorType.Left] != null)
            {
                door[(int)Door.DoorType.Left].gameObject.SetActive(true);
               // door[(int)Door.DoorType.Left].CreateDoor(this.gameObject);
            }
        }
        if (StageLinkedData.UpMap != null)
        {
            if (door[(int)Door.DoorType.Up] != null)
            {
                door[(int)Door.DoorType.Up].gameObject.SetActive(true);
                //door[(int)Door.DoorType.Up].CreateDoor(this.gameObject);
            }
        }
        if (StageLinkedData.DownMap != null)
        {
            if (door[(int)Door.DoorType.Down] != null)
            {
                door[(int)Door.DoorType.Down].gameObject.SetActive(true);
                //door[(int)Door.DoorType.Down].CreateDoor(this.gameObject);
            }
        }
    }



    public void Init()
    {
        //playerobj = GameObject.Find("Player");
        LoadDoors();
        Initialize();
    }

    public void Enter()
    {
        Map.Instance.CurrentStage = this;
        RoomActive = true;
        //spawn shits
    }
    public void Exit()
    {

    }
    public void OnClear()
    {
        RoomActive = false;
        Cleared = true;
    }
    
    ///////적이 스폰하는 아레나 안에 넣을것
    //private void OnTransformChildrenChanged()
    //{
    //    if (transform.childCount <= 0 && RoomActive)
    //    {
    //        //적이 모두 죽음;
    //    }
    //}
}