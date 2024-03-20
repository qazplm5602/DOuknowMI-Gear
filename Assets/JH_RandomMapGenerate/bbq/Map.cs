using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : CockSingleton<Map>
{
    public enum ROOMTYPE { Start, Normal }

    public GameObject[] smallroom;

    public GameObject StageLoad(ROOMTYPE type)
    {
        //랜덤으로 뽑아서 하나를 넘겨준다.
        int count = 0;
        if (type == ROOMTYPE.Normal)
        {
            count = smallroom.Length;
        }

        int rnd = Random.Range(0, count);

        return smallroom[rnd];

    }
}
