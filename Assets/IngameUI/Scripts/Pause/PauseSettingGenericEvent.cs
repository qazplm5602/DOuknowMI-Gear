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

        _menu.AddSetEvent("generic.doming", OnChangeDoming);
        _menu.AddGetEvent("generic.doming", GetDoming);

        _menu.AddSetEvent("generic.domiweb", OnChangeDomiweb);
        _menu.AddGetEvent("generic.domiweb", GetDomiweb);
    }

    // 밍글링
    void OnChangeMingling(string value) {
        print($"OnChangeMingling {value}");
    }

    string GetMingling() {
        return "true";
    }

    void OnChangeDoming(string value) {
        print($"OnChangeDoming {value}");
    }

    string GetDoming() {
        return "38";
    }

    void OnChangeDomiweb(string value) {
        print($"OnChangeDoming {value}");
    }

    string GetDomiweb() {
        return "1";
    }
}
