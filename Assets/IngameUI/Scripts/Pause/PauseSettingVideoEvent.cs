using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSettingVideoEvent : MonoBehaviour
{
    PauseSettingMenu _menu;
    
    private void Awake() {
        _menu = transform.parent.GetComponent<PauseSettingMenu>();
    }

    readonly string VSYNC_EVENT = "video.vsync";
    readonly string SCREENMODE_EVENT = "video.screenmode";

    private void Start() {
        _menu.AddSetEvent(VSYNC_EVENT, SetvSync);
        _menu.AddGetEvent(VSYNC_EVENT, GetvSync);

        _menu.AddSetEvent(SCREENMODE_EVENT, SetScreenMode);
        _menu.AddGetEvent(SCREENMODE_EVENT, GetScreenMode);
    }

    private void OnDestroy() {
        _menu.RemoveSetEvent(VSYNC_EVENT, SetvSync);
        _menu.RemoveGetEvent(VSYNC_EVENT, GetvSync);

        _menu.RemoveSetEvent(SCREENMODE_EVENT, SetScreenMode);
        _menu.RemoveGetEvent(SCREENMODE_EVENT, GetScreenMode);
    }

    private void SetvSync(string boolean)
    {
        QualitySettings.vSyncCount = int.Parse(boolean);
        PlayerPrefs.SetInt(VSYNC_EVENT, QualitySettings.vSyncCount);
        PlayerPrefs.Save();
    }

    private string GetvSync() => QualitySettings.vSyncCount.ToString();

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
        
        switch (obj) {
            case "0":

                break;

            default:
                break;
        }

        // Screen.fullScreenMode = ;
        // PlayerPrefs.SetInt();
    }
}
