using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repairman : Npc
{
    public override void Interaction()
    {
        PlayerStat.Instance.Open();
    }
}
