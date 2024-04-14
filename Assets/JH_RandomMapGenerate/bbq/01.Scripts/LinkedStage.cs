using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LinkedStage
{
    public BaseStage RightMap = null;
    public BaseStage LeftMap = null;
    public BaseStage UpMap = null;
    public BaseStage DownMap = null;


    public int Num;
    public int indexX;
    public int indexY;

}