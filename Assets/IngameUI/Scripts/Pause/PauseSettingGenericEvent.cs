    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSettingGenericEvent : MonoBehaviour
{
    PauseSettingMenu _menu;
    
    private void Awake() {
        _menu = transform.parent.GetComponent<PauseSettingMenu>();
    }

    private void Start() {
        _menu.AddSetEvent("generic.mingling", OnChangeMingling);
        _menu.AddGetEvent("generic.mingling", GetMingling);
    }

    // 밍글링
    void OnChangeMingling(string value) {
        print($"OnChangeMingling {value}");
    }

    string GetMingling() {
        return "true";
    }
}
