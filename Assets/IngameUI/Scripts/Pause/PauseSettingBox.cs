using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseSettingBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI _title;
    [SerializeField] TMP_Dropdown _dropdown;
    [SerializeField] Slider _slider;
    [SerializeField] Toggle _toggle;

    Action onMouseOver;
    Action onMouseLeave;
    
    // string eventName;

    public void Init(PauseSettingMenu menu, PauseSettingConfig data, Action mouseEnter, Action mouseLeave) {
        _title.text = data.title;
        onMouseOver = mouseEnter;
        onMouseLeave = mouseLeave;
        

        // 불러온 값
        string loadValue = menu.TriggerGetEvent(data.eventName);

        switch (data.type)
        {
            case PauseSettingConfig.Type.Option:
                _dropdown.gameObject.SetActive(true);
                _dropdown.ClearOptions();
                _dropdown.AddOptions(data.options.ToList());
                _dropdown.onValueChanged.AddListener((int i) => {
                    menu.TriggerSetEvent(data.eventName, i.ToString());
                });
                
                if (int.TryParse(loadValue, out int result)) {
                    _dropdown.value = result;
                }

                break;
            
            case PauseSettingConfig.Type.Check:
                _toggle.gameObject.SetActive(true);
                _toggle.onValueChanged.AddListener((bool newValue) => {
                    menu.TriggerSetEvent(data.eventName, newValue.ToString());
                });
                _toggle.isOn = loadValue.Equals("true");
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
                if (int.TryParse(loadValue, out int result2)) {
                    _slider.value = result2;
                    domiT.text = result2.ToString();
                }
                break;

            default:
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onMouseOver?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onMouseLeave?.Invoke();
    }
}
