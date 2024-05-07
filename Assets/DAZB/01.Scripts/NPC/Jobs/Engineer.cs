using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engineer : Npc
{
    public override void Interaction()
    {
        IngameUIControl.Instance.gearChangeUI.Open();
    }
}
