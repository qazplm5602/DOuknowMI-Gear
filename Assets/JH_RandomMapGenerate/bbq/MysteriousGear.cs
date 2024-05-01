using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteriousGear : InteractiveObject
{
    [SerializeField] private EliteMobStage stage;
    public override void Interaction()
    {
        stage.StartEliteStage();
        print("AMBATAKAM");
    }
}
