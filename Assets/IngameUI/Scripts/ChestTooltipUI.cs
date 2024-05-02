using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestTooltipUI : MonoBehaviour
{
    [SerializeField] CanvasGroup _group;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI subTitle;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] Image icon;
    

    public void Show(GearSO data) {
        title.text = data.Name;
        subTitle.text = $"{(data.ActiveDamage ? $"기본 데미지: {data.DefaultDamage}\n" : "")}{(data.ActiveRange ? $"기본 범위: {data.DefaultRange}" : "")}";
        description.text = data.Desc;
        icon.sprite = data.Icon;
        
        _group.DOKill();
        _group.DOFade(1, 0.1f);
        gameObject.SetActive(true);
    }

    public void Hide() {
        _group.DOKill();
        _group.DOFade(0, 0.1f).OnComplete(() => gameObject.SetActive(false));
    }
    
    private void Update() {
        transform.position = Input.mousePosition;
    }
}
