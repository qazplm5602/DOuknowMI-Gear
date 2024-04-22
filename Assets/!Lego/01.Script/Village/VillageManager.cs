using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VillageManager : MonoSingleton<VillageManager>
{
    [SerializeField] private string _stageSceneName;

    public void ChangeSceneToStage()
    {
        SceneManager.LoadScene(_stageSceneName);
    }
}
