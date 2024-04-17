using System.Collections.Generic;
using UnityEngine;

public enum ROOMTYPE { Start, Normal, Boss ,Pray }
public enum ROOMSIZE { NODATA = -1, Small, Medium, Large }
public class Map : CockSingleton<Map>
{

    public BaseStage CurrentStage;

    public BaseStage StartRoom;
    public BaseStage[] BossRoom; 
    public BaseStage PrayRoom; 
    public BaseStage[] SmallRoom;
    public BaseStage[] MediumRoom;
    public BaseStage[] LargeRoom;

    private Dictionary<ROOMSIZE, BaseStage[]> Rooms = new Dictionary<ROOMSIZE, BaseStage[]>();

    public void Init()
    { 
        Rooms[ROOMSIZE.Small] = SmallRoom;
        Rooms[ROOMSIZE.Medium] = MediumRoom;
        Rooms[ROOMSIZE.Large] = LargeRoom;
    }

    public BaseStage StageLoad(ROOMTYPE type, ROOMSIZE size = ROOMSIZE.Large)
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
