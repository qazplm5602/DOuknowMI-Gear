using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteriousGear : InteractiveObject
{
    [SerializeField] private EliteMobStage stage;
    [SerializeField] GearDatabase database;
    [SerializeField] private GameObject 자지;
    public override void Interaction()
    {
        Instantiate(자지,transform.position,Quaternion.identity);

        var gear = database.GetRandomGear();
        IngameUIControl.Instance.gearChangeUI.GiveInventory(gear);
        stage.StartEliteStage();
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject,2);
    }
}
