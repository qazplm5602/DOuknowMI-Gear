using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSettingGenericEvent : MonoBehaviour
{
    PauseSettingMenu _menu;
    bool init = false;

    readonly string HITDAMAGE_EVENT = "generic.hitdamage";
    readonly string CAMSHARE_EVENT = "generic.camerashake";
    readonly string ENEMYHEALTH_EVENT = "generic.enemyhelath";

    GameManager gameManager;
    
    private void Awake() {
        // _menu = transform.parent.GetComponent<PauseSettingMenu>();
        _menu = FindObjectOfType<PauseSettingMenu>();
        print(_menu);
        gameManager = GameManager.Instance;
    }

    private void OnEnable() {
        if (!init) return;
        transform.Find("Categorys").GetChild(0).GetComponent<Button>().onClick?.Invoke();
    }

    private void Start() {
        _menu.AddSetEvent(CAMSHARE_EVENT, SetCamShake);
        _menu.AddGetEvent(CAMSHARE_EVENT, GetCamShake);

        _menu.AddSetEvent(ENEMYHEALTH_EVENT, SetEnemyHealth);
        _menu.AddGetEvent(ENEMYHEALTH_EVENT, GetEnemyHealth);

        _menu.AddSetEvent(HITDAMAGE_EVENT, SetHitDamage);
        _menu.AddGetEvent(HITDAMAGE_EVENT, GetHitDamage);

        if (!init) {
            init = true;
            transform.parent.Find("Categorys").GetChild(0).GetComponent<Button>().onClick?.Invoke();
        }
    }

    private void OnDestroy() {
        _menu.RemoveSetEvent(CAMSHARE_EVENT, SetCamShake);
        _menu.RemoveGetEvent(CAMSHARE_EVENT, GetCamShake);

        _menu.RemoveSetEvent(ENEMYHEALTH_EVENT, SetEnemyHealth);
        _menu.RemoveGetEvent(ENEMYHEALTH_EVENT, GetEnemyHealth);

        _menu.RemoveSetEvent(HITDAMAGE_EVENT, SetHitDamage);
        _menu.RemoveGetEvent(HITDAMAGE_EVENT, GetHitDamage);
    }

    void SetHitDamage(string value) {
        PlayerPrefs.SetInt(HITDAMAGE_EVENT, value == "True" ? 1 : 0);
        PlayerPrefs.Save();

        if (gameManager)
            gameManager.showDamageText = value == "True" ? true : false;
    }

    string GetHitDamage() => PlayerPrefs.GetInt(HITDAMAGE_EVENT, 1) == 1 ? "true" : "false";

    void SetCamShake(string value) {
        PlayerPrefs.SetInt(CAMSHARE_EVENT, value == "True" ? 1 : 0);
        PlayerPrefs.Save();

        if (gameManager)
            gameManager.cameraShake = value == "True" ? true : false;
    }

    string GetCamShake() => PlayerPrefs.GetInt(CAMSHARE_EVENT, 1) == 1 ? "true" : "false";

    void SetEnemyHealth(string value) {
        PlayerPrefs.SetInt(ENEMYHEALTH_EVENT, value == "True" ? 1 : 0);
        PlayerPrefs.Save();
    }

    string GetEnemyHealth() => PlayerPrefs.GetInt(ENEMYHEALTH_EVENT, 1) == 1 ? "true" : "false";
}
