using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public bool showDamageText = true;
    public bool cameraShake = true;

    private bool showHealthBar = true;
    public Action<bool> ShowHealthBarEvent;

    public bool ShowHealthBar {
        get => showHealthBar;
        set {
            showHealthBar = value;
            ShowHealthBarEvent?.Invoke(showHealthBar);
        }
    }

    private void Awake() {
        showDamageText = PlayerPrefs.GetInt("generic.hitdamage", 1) == 1;
        cameraShake = PlayerPrefs.GetInt("generic.camerashake", 1) == 1;
    }
}
