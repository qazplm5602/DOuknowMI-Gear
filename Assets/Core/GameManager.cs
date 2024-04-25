using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public bool showDamageText = true;
    private bool showHealthBar = true;
    public bool cameraShaking = true;
    public Action<bool> ShowHealthBarEvent;

    public bool ShowHealthBar {
        get => showHealthBar;
        set {
            showHealthBar = value;
            ShowHealthBarEvent?.Invoke(showHealthBar);
        }
    }
}
