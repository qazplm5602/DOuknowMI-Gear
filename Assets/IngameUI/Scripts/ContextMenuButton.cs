using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ContextMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Image _image;

    private void Awake() {
        _image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.color = new Color32(240, 240, 240, 255);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.color = new Color32(255, 255, 255, 255);
    }
}
