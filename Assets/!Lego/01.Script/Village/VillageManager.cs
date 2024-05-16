using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VillageManager : MonoSingleton<VillageManager>
{
    [SerializeField] private string _stageSceneName;
    [SerializeField] private PolygonCollider2D _VillageCollider;
    [SerializeField] CinemachineConfiner2D confiner2d;

    private void Start()
    {
        confiner2d.m_BoundingShape2D = _VillageCollider;
    }
    public void ChangeSceneToStage()
    {
        LoadManager.LoadScene(_stageSceneName);
    }
}
