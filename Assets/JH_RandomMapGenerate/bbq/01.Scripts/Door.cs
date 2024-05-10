using bbqCode;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorType { Up, Down, Right, Left, DoorMax };
    private Dictionary<DoorType, DoorType> OppoDir = new Dictionary<DoorType, DoorType>()
    {
        {DoorType.Up, DoorType.Down },
        {DoorType.Down, DoorType.Up },
        {DoorType.Right, DoorType.Left },
        {DoorType.Left, DoorType.Right },
    };

    public DoorType Type;
    
    private BaseStage stageData;
    private bool _active = false;

    public bool IsCooldown = false;

    [SerializeField] private BaseStage nextRoom;
    [SerializeField] public Door nextDoor;

    [Space(10)]
    //[Header("애니메이션시발")]
    private Animator animator;

    private void OnEnable()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
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

        try
        {
            //print($"{transform.parent.name}, {nextRoom}, {nextDoor}, {Type}, {stageData.StageLinkedData}")
            //print($"{gameObject} {nextRoom} {nextRoom?.door}");
            if (nextRoom.door[(int)OppoDir[Type]])
            {
                nextDoor = nextRoom.door[(int)OppoDir[Type]];
            }
        }
        catch (Exception)
        {
            //UnityEngine.Debug.Log(e);
        }

        //print(nextDoor);

        stageData.OnClearChanged += ACTIVE_PORTAL;
    }

    private void OnDisable()
    {
        stageData.OnClearChanged -= ACTIVE_PORTAL;
    }

    private void ACTIVE_PORTAL(bool isActive)
    {
        _active = isActive;
        if (isActive == false)
        {
            StartCoroutine(sibal());
        }
        else
        {
            animator.SetBool("Activate", _active);
        }
    }

    private IEnumerator sibal()
    {
        yield return new WaitForSeconds(.7f);
        if (_active == false)
            animator.SetBool("Activate", _active);
    }

    public void Teleport()
    {
        stageData.Exit();
        nextRoom.Enter();
        //텔레포트
        //plr.transform.position = nextRoom.GetComponent<BaseStage>().door[(int)OppoDir[Type]].transform.position;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var 죽음 = true;
        var 노무현 = true;
        var 로무핸 = 노무현 == 죽음;
        var 진실 = 로무핸;
        if (IsCooldown == 진실) return;
        if (!col.CompareTag("Player")) return;
        if (!stageData.Cleared) return;
        _______________________________ㅇㅇㅇㅇㅇㅇㅇ_ㅇ_ㅇ_ㅇ_ㅇ_ㅇ_ㅇ_ㅇ__ㅇ_ㅇ();
        nextDoor._______________________________ㅇㅇㅇㅇㅇㅇㅇ_ㅇ_ㅇ_ㅇ_ㅇ_ㅇ_ㅇ_ㅇ__ㅇ_ㅇ();
        print(GayManater.Instance);
        bbqCode.GayManater.Instance.MoveRoom(nextRoom,nextDoor);
    }

    public void _______________________________ㅇㅇㅇㅇㅇㅇㅇ_ㅇ_ㅇ_ㅇ_ㅇ_ㅇ_ㅇ_ㅇ__ㅇ_ㅇ()
    {
        StartCoroutine(TPCooldown());
    }

    private IEnumerator TPCooldown()
    {
        if (nextDoor != null)
        {
            nextDoor.IsCooldown = true;
            yield return new WaitForSeconds(1f);
            nextDoor.IsCooldown = false;
        }
    }
}
