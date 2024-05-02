using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using 자지 = System.Boolean;

public abstract class BaseStage : MonoBehaviour
{
    public event Action<자지> OnClearChanged;

    [Header("UP DOWN RIGHT LEFT")]
    public Door[] door;
    public ROOMTYPE type;

    public int StageNum;

    [HideInInspector] public LinkedStage StageLinkedData;

    [HideInInspector] public GameObject Arena; //적의 스폰 Parent

    public int MaxWave = 3;
    [HideInInspector] public int CurrentWave = 0;

    private bool cleared = true;

    //public bool Cleared = false;
    public bool Cleared
    {
        get
        {
            return cleared;
        }
        set
        {
            cleared = value;
            OnClearChanged?.Invoke(cleared);
        }
    }
    public bool RoomActive = false;

    public virtual void LoadDoors()
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


    public virtual void Initialize()
    {
        if (ROOMTYPE.Normal == type)
        {
            MaxWave = Mathf.Max(2, MaxWave - 1);
        }
        else
        {
            MaxWave = 0;
        }

        CurrentWave = 1;

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

    public virtual void StartRoom()
    {

    }

    [ContextMenu("Clear Wave")]
    public virtual void NextWave()
    {
        ++CurrentWave;
        if (CurrentWave > MaxWave)
            Clear();
        // do something for wavesa
    }

    public virtual void Init()
    {
        LoadDoors();
        Initialize();
    }

    public virtual void Enter()
    {
        Map.Instance.CurrentStage = this;
        RoomActive = true;

        StartRoom();
        //spawn shits
    }
    public virtual void Exit()
    {
        Cleared = true;
        RoomActive = false;
    }


    [ContextMenu("ClearRoom")]
    public virtual void Clear()
    {
        RoomActive = true;
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