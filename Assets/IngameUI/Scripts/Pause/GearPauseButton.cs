using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GearPauseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject buttonName;
    GearPauseButtonTooltip _tooltip;
    
    private void Awake() {
        _tooltip = GetComponentInChildren<GearPauseButtonTooltip>();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter == buttonName)
            _tooltip.SetToggle(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter == buttonName)
            _tooltip.SetToggle(false);
    }
}
