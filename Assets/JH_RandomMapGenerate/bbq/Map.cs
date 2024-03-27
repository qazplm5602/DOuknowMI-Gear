using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Door;

public enum ROOMTYPE { Start, Normal, Boss ,Pray }
public enum ROOMSIZE { NODATA = -1, Small, Medium, Large }
public class Map : CockSingleton<Map>
{

    public BaseStage CurrentStage;

    public GameObject StartRoom;
    public GameObject[] BossRoom; 
    public GameObject PrayRoom; 
    public GameObject[] SmallRoom;
    public GameObject[] MediumRoom;
    public GameObject[] LargeRoom;

    private Dictionary<ROOMSIZE, GameObject[]> Rooms = new Dictionary<ROOMSIZE, GameObject[]>();

    private void Awake()
    {
        Rooms[ROOMSIZE.Small] = SmallRoom;
        Rooms[ROOMSIZE.Medium] = MediumRoom;
        Rooms[ROOMSIZE.Large] = LargeRoom;
    }

    public GameObject StageLoad(ROOMTYPE type, ROOMSIZE size = ROOMSIZE.Large)
    {
        //returns random room u want
        if (type == ROOMTYPE.Start)
            return StartRoom;
        if (type == ROOMTYPE.Normal)
        {
            return Rooms[size][Random.Range(0, Rooms[size].Length)];
        }
        if (type == ROOMTYPE.Boss)
        {
            return BossRoom[Random.Range(0, BossRoom.Length)];
        }
        if (type == ROOMTYPE.Pray)
            return PrayRoom;
        throw new System.NotImplementedException();
    }
}
