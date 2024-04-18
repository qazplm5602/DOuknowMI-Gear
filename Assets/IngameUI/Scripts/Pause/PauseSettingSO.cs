using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct PauseSettingConfig {
    public enum Type {
        Check,
        Slider,
        Option
    }

    public string title;
    public string eventName;
    public Type type;
    [TextArea] public string description;
    
    [Header("Slider")]
    public int sliderMin;
    public int sliderMax;

    [Header("Option")]
    // public Dropdown.OptionDataList options;
    public string[] options;
}

[CreateAssetMenu(menuName = "SO/UI/PauseSetting")]
public class PauseSettingSO : ScriptableObject
{
    public PauseSettingConfig[] datas;
}
