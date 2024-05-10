using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToDungeon : InteractiveObject
{
    public override void Interaction()
    {
        VillageManager.Instance.ChangeSceneToStage();
    }
}
