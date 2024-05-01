using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSettingVideoEvent : MonoBehaviour
{
    PauseSettingMenu _menu;
    
    private void Awake() {
        // _menu = transform.parent.GetComponent<PauseSettingMenu>();
        _menu = FindObjectOfType<PauseSettingMenu>();
    }

    readonly string RESOLUTION_EVENT = "video.resolution";
    readonly string VSYNC_EVENT = "video.vsync";
    readonly string SCREENMODE_EVENT = "video.screenmode";

    private void Start() {
        _menu.AddSetEvent(RESOLUTION_EVENT, SetResoution);
        _menu.AddGetEvent(RESOLUTION_EVENT, GetResoution);
        
        _menu.AddSetEvent(VSYNC_EVENT, SetvSync);
        _menu.AddGetEvent(VSYNC_EVENT, GetvSync);

        _menu.AddSetEvent(SCREENMODE_EVENT, SetScreenMode);
        _menu.AddGetEvent(SCREENMODE_EVENT, GetScreenMode);
    }

    private void SetResoution(string obj)
    {
    }

    private string GetResoution()
    {
        return string.Empty;
    }

    private void OnDestroy() {
        _menu.RemoveSetEvent(VSYNC_EVENT, SetvSync);
        _menu.RemoveGetEvent(VSYNC_EVENT, GetvSync);

        _menu.RemoveSetEvent(SCREENMODE_EVENT, SetScreenMode);
        _menu.RemoveGetEvent(SCREENMODE_EVENT, GetScreenMode);
    }

    private void SetvSync(string boolean)
    {
        QualitySettings.vSyncCount = boolean == "True" ? 1 : 0;
        PlayerPrefs.SetInt(VSYNC_EVENT, QualitySettings.vSyncCount);
        PlayerPrefs.Save();
    }

    private string GetvSync() => QualitySettings.vSyncCount == 1 ? "true" : "false";

    private string GetScreenMode()
    {
        switch (Screen.fullScreenMode)
        {
            case FullScreenMode.FullScreenWindow:
                return "1";

            case FullScreenMode.Windowed:
                return "2";

            case FullScreenMode.ExclusiveFullScreen:
            default:
                return "0";
        }
    }

    private void SetScreenMode(string obj)
    {
        FullScreenMode mode;

        switch (obj) {
            case "0":
                mode = FullScreenMode.ExclusiveFullScreen;
                break;
            case "1":
                mode = FullScreenMode.FullScreenWindow;
                break;
            case "2":
                mode = FullScreenMode.Windowed;
                break;
            default:
                return;
        }

        Screen.fullScreenMode = mode;
        PlayerPrefs.SetInt(SCREENMODE_EVENT, int.Parse(obj));
        PlayerPrefs.Save();
    }
}
