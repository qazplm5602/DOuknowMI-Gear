using System.Collections.Generic;
using UnityEngine;

public enum ROOMTYPE { Start, Normal, Boss, Statue, Elite }
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
    public BaseStage[] EliteRoom;

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

        switch (type)
        {
            case ROOMTYPE.Start:
                return StartRoom;
            case ROOMTYPE.Normal:
                return Rooms[size][Random.Range(0, Rooms[size].Length)];
            case ROOMTYPE.Boss:
                return BossRoom[Random.Range(0, BossRoom.Length)];
            case ROOMTYPE.Statue:
                return PrayRoom;
            case ROOMTYPE.Elite:
                return EliteRoom[Random.Range(0, EliteRoom.Length)];
        }
        throw new System.NotImplementedException();
    }
}
