using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSettingSoundEvent : MonoBehaviour
{
    PauseSettingMenu _menu;

    //////////// eventName
    readonly string MASTER_EVENT = "sound.master";
    readonly string EFFECT_EVENT = "sound.effect";
    readonly string BGM_EVENT = "sound.bgm";
    
    private void Awake() {
        _menu = transform.parent.GetComponent<PauseSettingMenu>();
    }

    private void Start() {
        _menu.AddSetEvent(MASTER_EVENT, SetMaster);
        _menu.AddGetEvent(MASTER_EVENT, GetMaster);
        
        _menu.AddSetEvent(EFFECT_EVENT, SetEffect);
        _menu.AddGetEvent(EFFECT_EVENT, GetEffect);

        _menu.AddSetEvent(BGM_EVENT, SetBGM);
        _menu.AddGetEvent(BGM_EVENT, GetBGM);
    }

    private void OnDestroy() {
        _menu.RemoveSetEvent(MASTER_EVENT, SetMaster);
        _menu.RemoveGetEvent(MASTER_EVENT, GetMaster);
        
        _menu.RemoveSetEvent(EFFECT_EVENT, SetEffect);
        _menu.RemoveGetEvent(EFFECT_EVENT, GetEffect);
        
        _menu.RemoveSetEvent(BGM_EVENT, SetBGM);
        _menu.RemoveGetEvent(BGM_EVENT, GetBGM);
    }

    void SetMaster(string value) {
        PlayerPrefs.SetInt(MASTER_EVENT, int.Parse(value));
        PlayerPrefs.Save();
    }

    string GetMaster() {
        return PlayerPrefs.GetInt(MASTER_EVENT, 100).ToString();
    }

    void SetEffect(string value) {
        PlayerPrefs.SetInt(EFFECT_EVENT, int.Parse(value));
        PlayerPrefs.Save();
    }

    string GetEffect() {
        return PlayerPrefs.GetInt(EFFECT_EVENT, 100).ToString();
    }

    void SetBGM(string value) {
        PlayerPrefs.SetInt(BGM_EVENT, int.Parse(value));
        PlayerPrefs.Save();
    }

    string GetBGM() {
        return PlayerPrefs.GetInt(BGM_EVENT, 100).ToString();
    }
}
