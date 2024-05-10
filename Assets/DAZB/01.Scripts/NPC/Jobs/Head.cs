using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : Npc
{
    [SerializeField] private GameObject saveUI;
    public override void Interaction()
    {
        // 세이브 창 켜야함
        SaveManager.Instance.saveUI.Open();
    }
}
