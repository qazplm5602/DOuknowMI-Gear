using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatueBtnUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(new Vector3(1.15f, 1.15f, 1), 0.3f).SetEase(Ease.OutQuart);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(new Vector3(1f, 1f, 1), 0.3f).SetEase(Ease.OutQuart);
    }
}
