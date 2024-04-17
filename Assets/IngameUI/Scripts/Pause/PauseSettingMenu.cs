using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
struct PauseSettingSOPair {
    public string category;
    public PauseSettingSO so;
}

public class PauseSettingMenu : MonoBehaviour
{
    [Header("Section")]
    [SerializeField] Transform _categorys;
    [SerializeField] Transform _scrollContent;
    [SerializeField] TextMeshProUGUI _description;

    [Header("Prefab")]
    [SerializeField] GameObject _box;
    
    [Header("Config")]
    [SerializeField] PauseSettingSOPair[] menus;
}
