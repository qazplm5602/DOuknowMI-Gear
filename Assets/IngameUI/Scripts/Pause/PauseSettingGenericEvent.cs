    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSettingGenericEvent : MonoBehaviour
{
    PauseSettingMenu _menu;
    bool init = false;

    readonly string HITDAMAGE_EVENT = "generic.hitdamage";
    
    private void Awake() {
        _menu = transform.parent.GetComponent<PauseSettingMenu>();
    }

    private void OnEnable() {
        if (!init) return;
        transform.Find("Categorys").GetChild(0).GetComponent<Button>().onClick?.Invoke();
    }

    private void Start() {
        _menu.AddSetEvent(HITDAMAGE_EVENT, SetHitDamage);
        _menu.AddGetEvent(HITDAMAGE_EVENT, GetHitDamage);

        if (!init) {
            init = true;
            transform.Find("Categorys").GetChild(0).GetComponent<Button>().onClick?.Invoke();
        }
    }

    private void OnDestroy() {
        _menu.RemoveSetEvent(HITDAMAGE_EVENT, SetHitDamage);
        _menu.RemoveGetEvent(HITDAMAGE_EVENT, GetHitDamage);
    }

    void SetHitDamage(string value) {
        PlayerPrefs.SetInt(HITDAMAGE_EVENT, value == "True" ? 1 : 0);
        PlayerPrefs.Save();
    }

    string GetHitDamage() => PlayerPrefs.GetInt(HITDAMAGE_EVENT, 0) == 1 ? "true" : "false";
}
