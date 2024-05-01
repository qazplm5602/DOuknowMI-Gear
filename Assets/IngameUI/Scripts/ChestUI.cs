using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChestUI : MonoBehaviour
{
    [SerializeField] RectTransform _screen;
    [SerializeField] Transform _section;
    [SerializeField] ChestGearUI _gearPrefab;
    [SerializeField] ChestTooltipUI _tooltip;
    [SerializeField] Button _selectBtn;

    CanvasGroup _group;
    List<ChestGearUI> _gears;
    
    private void Awake() {
        _group = _screen.GetComponent<CanvasGroup>();
        _gears = new();
        _selectBtn.onClick.AddListener(FinalSelect);
    }

    Sequence sequence;

    public void Show(GearSO[] gears) {
        Clear();
        _screen.gameObject.SetActive(true);

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
        sequence.Append(_group.DOFade(1, 0.3f));
        foreach (var item in _gears)
        {
            sequence.Join(item._group.DOFade(0.3f, 0.5f).SetDelay(i * 0.02f));
            i++;
        }
    }

    public void Hide() {
        Clear();
    }

    void Clear() {
        if (sequence != null) {
            sequence.Kill();
            sequence = null;
        }

        foreach (var item in _gears)
            Destroy(item.gameObject);

        _gears.Clear();
        _tooltip.Hide();
        _group.alpha = 0;
        _screen.gameObject.SetActive(false);
    }

    public void Hover(GearSO data) {
        _tooltip.Show(data);
    }

    public void UnHover() {
        _tooltip.Hide();
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

    void FinalSelect() {
        ChestGearUI gearUI = null;
        foreach (var item in _gears)
        {
            if (item.active) {
                gearUI = item;
                break;
            }
        }

        if (gearUI == null) return; // 선택 안함

        if (sequence != null)
            sequence.Kill();

        if (GearManager.Instance.GetSlotGearSO().Length < 4) {
            GearManager.Instance.GearAdd(gearUI._gear);
        } else {
            IngameUIControl.Instance.gearChangeUI.GiveInventory(gearUI._gear);
        }

        sequence = DOTween.Sequence();

        sequence.Join(gearUI.transform.DOScale(2, 0.3f).SetEase(Ease.OutQuad));
        sequence.Join(_group.DOFade(0, 0.3f).SetEase(Ease.OutQuad));
        sequence.AppendCallback(() => Hide());
    }
}
