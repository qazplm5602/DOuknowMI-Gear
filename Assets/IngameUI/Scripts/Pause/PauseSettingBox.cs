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

        switch (data.type)
        {
            case PauseSettingConfig.Type.Option:
                _dropdown.gameObject.SetActive(true);
                _dropdown.ClearOptions();
                _dropdown.AddOptions(data.options.ToList());
                break;
            
            case PauseSettingConfig.Type.Check:
                _toggle.gameObject.SetActive(true);
                break;

            case PauseSettingConfig.Type.Slider:
                _slider.gameObject.SetActive(true);
                _slider.minValue = data.sliderMin;
                _slider.maxValue = data.sliderMax;
                break;

            default:
                break;
        }
    }
}
