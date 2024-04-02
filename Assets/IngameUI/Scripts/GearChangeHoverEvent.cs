using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GearChangeHoverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public event System.Action<bool> OnHoverEvent;
    public event System.Action OnLeftMouseDownEvent;
    public event System.Action OnRightMouseDownEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            OnLeftMouseDownEvent?.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right)
            OnRightMouseDownEvent?.Invoke();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHoverEvent?.Invoke(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnHoverEvent?.Invoke(false);
    }
    
    public void ClearEventHandler() {
        OnHoverEvent = null;
        OnLeftMouseDownEvent = null;
        OnRightMouseDownEvent = null;
    }
}
