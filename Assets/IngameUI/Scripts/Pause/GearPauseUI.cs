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
        resumeBtn.onClick.AddListener(Hide);
        settingBtn.onClick.AddListener(() => ScreenChange("setting"));
        settingExitBtn.onClick.AddListener(() => ScreenChange("home"));
    }

    private void Update() {
        // TEST
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Show();
        }
        // TEST END
    }

    public void Show() {
        _pauseScreen.SetActive(true);
    }

    public void Hide() {
        _pauseScreen.SetActive(false);
        GearPauseTooltipClear();
    }

    void ScreenChange(string type) {
        if (!type.Equals("home")) { // 홈이 숨겨짐
            GearPauseTooltipClear();
        }
        _Home.SetActive(type.Equals("home"));

        _Setting.SetActive(type.Equals("setting"));
    }

    void GearPauseTooltipClear() {
        foreach (var item in _Home.transform.GetComponentsInChildren<GearPauseButtonTooltip>())
            item.ForceHide();
    }
}
