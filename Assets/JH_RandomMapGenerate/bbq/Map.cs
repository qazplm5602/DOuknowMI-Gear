using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : CockSingleton<Map>
{
    public enum ROOMTYPE { Start, Normal }

    public GameObject[] smallroom;

    public GameObject StageLoad(ROOMTYPE type)
    {
        //�������� �̾Ƽ� �ϳ��� �Ѱ��ش�.
        int count = 0;
        if (type == ROOMTYPE.Normal)
        {
            count = smallroom.Length;
        }

        int rnd = Random.Range(0, count);

        return smallroom[rnd];

    }
}
