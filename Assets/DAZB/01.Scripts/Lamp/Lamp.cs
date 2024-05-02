using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : InteractiveObject
{
    [SerializeField] private GameObject LightObject;
    [SerializeField] private bool isTurn;
    public override void Interaction()
    {
        // 껏다 켯다 넣어야함
        isTurn = !isTurn;
        if (!isTurn) {
            interactionName = "켜기";
        }
        else {
            interactionName = "끄기";
        }
        LightObject.SetActive(isTurn);
    }
}
