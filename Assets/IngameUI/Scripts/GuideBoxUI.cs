using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GuideBoxUI : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    static Action OnTriggerDisable;
    [SerializeField] Image background;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI sub;
    
    GuideUI _core;

    bool isActive = false;

    GuideMenu _data;

    public void Init(GuideMenu data, GuideUI master) {
        OnTriggerDisable += UnActive;

        title.text = data.title;
        sub.text = data.sub;
        _data = data;

        _core = master;
    }

    private void OnDestroy() {
        OnTriggerDisable -= UnActive;
    }

    public void SetActive(bool active) {
        isActive = active;
        
        title.color = active ? Color.black : Color.white;
        sub.color = active ? new Color32(72, 72, 72, 255) : new Color32(149, 149, 149, 255);
        background.color = active ? new Color32(215, 215, 215, 185) : new Color(0,0,0,0);
    }

    void UnActive() {
        SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnTriggerDisable?.Invoke();
        SetActive(true);
        _core.ShowContent(_data.content);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
