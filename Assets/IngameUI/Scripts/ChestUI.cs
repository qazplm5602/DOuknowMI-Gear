using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChestUI : MonoBehaviour
{
    [SerializeField] RectTransform _screen;
    [SerializeField] Transform _section;
    [SerializeField] ChestGearUI _gearPrefab;

    CanvasGroup _group;
    List<ChestGearUI> _gears;

    private void Awake() {
        _group = _screen.GetComponent<CanvasGroup>();
        _gears = new();
    }

    Sequence sequence;

    public void Show(GearSO[] gears) {
        foreach (var item in gears)
        {
            ChestGearUI gear = Instantiate(_gearPrefab, _section);
            _gears.Add(gear);

            gear._gear = item;
            gear._master = this;
            gear.Init();
        }

        int i = 0;
        sequence = DOTween.Sequence();
        foreach (var item in _gears)
        {
            sequence.Join(item._group.DOFade(0.3f, 0.5f).SetDelay(i * 0.02f));
            i++;
        }
    }

    public void Hide() {

    }

    void Clear() {

    }

    public void SelectCancel()
    {
        foreach (var item in _gears)
        {
            if (item.active) {
                item.active = false;
                item.SetFocus(false);
            }
        }
    }
}
