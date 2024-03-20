using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseStage : MonoBehaviour
{
    public Door[] door;
    public Map.ROOMTYPE type;

    
    public LinkedStage StageLinkedData;

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
}