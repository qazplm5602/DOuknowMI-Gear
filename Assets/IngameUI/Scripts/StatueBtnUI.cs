using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatueBtnUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI desc;
    [SerializeField] Image border;

    public void Init(StatueBtnDTO data) {
        title.text = data.title;
        desc.text = data.desc;
        border.color = data.color;
    }

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
