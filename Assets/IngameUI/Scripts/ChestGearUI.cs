using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChestGearUI : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject cogPrefab;
    [SerializeField] TextMeshProUGUI _name;
    
    public CanvasGroup _group;
    public GearSO _gear;
    public ChestUI _master;
    public bool active;

    public void Init() {
        _group = GetComponent<CanvasGroup>();
        _group.alpha = 0;

        Color color = _name.color;
        color.a = 0;
        _name.color = color;
        _name.text = _gear.Name;

        for (int i = 0; i < _gear.CogList.Length; ++i) {
            var cog = Instantiate(cogPrefab, transform);
            var cog_image = cog.GetComponent<Image>();

            cog.transform.RotateAround(transform.position, new Vector3(0,0,1), 360f / _gear.CogList.Length * i);
            cog_image.color = GearCircle.GetCogTypeColor(_gear.CogList[i]);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (active) return;

        _master.SelectCancel();
        active = true;
        SetFocus(true);
    }

    public void SetFocus(bool value) {
        _group.DOFade(value ? 1f : 0.3f, 0.3f);
        _name.DOFade(value ? 1f : 0, 0.3f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _master.Hover(_gear);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _master.UnHover();
    }
}
