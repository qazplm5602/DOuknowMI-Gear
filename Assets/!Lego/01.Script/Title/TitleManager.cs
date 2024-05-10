using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoSingleton<TitleManager>
{
    [SerializeField] private string _villageSceneName;

    public void ChangeSceneToVillage()
    {
        LoadManager.LoadScene(_villageSceneName);
    }
}
