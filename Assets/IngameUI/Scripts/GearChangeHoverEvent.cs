using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GearChangeHoverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public event System.Action<bool> OnHoverEvent;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHoverEvent?.Invoke(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnHoverEvent?.Invoke(false);
    }
}
