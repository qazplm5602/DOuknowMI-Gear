using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearPauseUI : MonoBehaviour
{
    [SerializeField] GameObject _pauseScreen;
    [SerializeField] GameObject _Home;
    [SerializeField] GameObject _Setting;

    [SerializeField] Button resumeBtn;
    [SerializeField] Button settingBtn;
    [SerializeField] Button settingExitBtn;

    private void Awake() {
        resumeBtn.onClick.AddListener(() => _pauseScreen.SetActive(false));
        settingBtn.onClick.AddListener(() => ScreenChange("setting"));
        settingExitBtn.onClick.AddListener(() => ScreenChange("home"));
    }

    private void Update() {
        // TEST
        if (Input.GetKeyDown(KeyCode.Escape)) {
            _pauseScreen.SetActive(true);
        }
        // TEST END
    }

    void ScreenChange(string type) {
        _Home.SetActive(type.Equals("home"));
        _Setting.SetActive(type.Equals("setting"));
    }
}
