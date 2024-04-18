using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseSettingBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _title;
    [SerializeField] TMP_Dropdown _dropdown;
    [SerializeField] Slider _slider;
    [SerializeField] Toggle _toggle;
    
    // string eventName;

    public void Init(PauseSettingMenu menu, PauseSettingConfig data) {
        _title.text = data.title;
        // eventName = data.eventName;

        // 불러온 값
        string loadValue = menu.TriggerGetEvent(data.eventName);
        print(loadValue);

        switch (data.type)
        {
            case PauseSettingConfig.Type.Option:
                _dropdown.gameObject.SetActive(true);
                _dropdown.ClearOptions();
                _dropdown.AddOptions(data.options.ToList());
                _dropdown.onValueChanged.AddListener((int i) => {
                    menu.TriggerSetEvent(data.eventName, i.ToString());
                });
                break;
            
            case PauseSettingConfig.Type.Check:
                _toggle.gameObject.SetActive(true);
                _toggle.onValueChanged.AddListener((bool newValue) => {
                    menu.TriggerSetEvent(data.eventName, newValue.ToString());
                });
                break;

            case PauseSettingConfig.Type.Slider:
                var domiT = _slider.GetComponentInChildren<TextMeshProUGUI>();

                _slider.gameObject.SetActive(true);
                _slider.wholeNumbers = !data.sliderFloat;
                _slider.minValue = data.sliderMin;
                _slider.maxValue = data.sliderMax;
                _slider.onValueChanged.AddListener((float newValue) => {
                    domiT.text = newValue.ToString();
                    menu.TriggerSetEvent(data.eventName, newValue.ToString());
                });
                break;

            default:
                break;
        }
    }
}
