using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct StatueBtnDTO {
    public string title;
    public string desc;
    public Color color;
    public Action OnSelect;
}

public class StatueUI : MonoBehaviour
{
    [SerializeField] CanvasGroup _mainGroup;
    [SerializeField] RectTransform _button1;
    [SerializeField] RectTransform _button2;

    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI subTitle;
    [SerializeField] Image thumbnail;

    bool init = false;

    CanvasGroup group1;
    CanvasGroup group2;

    StatueBtnUI _script1, _script2;
    Action leftCb, rightCb;

    Sequence sequence;

    private void Awake() {
        if (init) return;
        init = true;
        
        group1 = _button1.GetComponent<CanvasGroup>();
        group2 = _button2.GetComponent<CanvasGroup>();

        _script1 = _button1.GetComponent<StatueBtnUI>();
        _script2 = _button2.GetComponent<StatueBtnUI>();

        _button1.GetComponent<Button>().onClick.AddListener(() => ClickButton(true));
        _button2.GetComponent<Button>().onClick.AddListener(() => ClickButton(false));
    }

    public void Show(string _title, string desc, Sprite image, StatueBtnDTO leftBtn, StatueBtnDTO rightBtn) {
        Awake();
        if (sequence != null) {
            sequence.Kill();
            sequence = null;
        }

        title.text = _title;
        subTitle.text = desc;
        thumbnail.sprite = image;

        _script1.Init(leftBtn);
        _script2.Init(rightBtn);

        leftCb = leftBtn.OnSelect;
        rightCb = rightBtn.OnSelect;

        gameObject.SetActive(true);
        _mainGroup.blocksRaycasts = group1.blocksRaycasts = group2.blocksRaycasts = true;

        sequence = DOTween.Sequence();

        sequence.Append(_mainGroup.DOFade(1, 0.3f));
        sequence.AppendInterval(1);

        sequence.Append(group1.DOFade(1, 0.3f).SetEase(Ease.OutQuad));
        sequence.Join(_button1.DOAnchorPosX(160, 0.3f).SetEase(Ease.OutQuad));
        
        sequence.Join(group2.DOFade(1, 0.3f).SetEase(Ease.OutQuad));
        sequence.Join(_button2.DOAnchorPosX(-160, 0.3f).SetEase(Ease.OutQuad));
    }

    public void Hide() {
        if (sequence != null) {
            sequence.Kill();
            sequence = null;
        }

        gameObject.SetActive(false);

        _mainGroup.alpha = group1.alpha = group2.alpha = 0;
        _mainGroup.blocksRaycasts = group1.blocksRaycasts = group2.blocksRaycasts = false;

        _button1.anchoredPosition = new Vector2(100, 0);
        _button2.anchoredPosition = new Vector2(-100, 0);
        _button1.localScale = _button2.localScale = new Vector3(1, 1, 1);

        _script1.enabled = true;
        _script2.enabled = true;
    }

    void ClickButton(bool left) {
        _mainGroup.blocksRaycasts = group1.blocksRaycasts = group2.blocksRaycasts = false;
        
        float halfWidth = (transform as RectTransform).rect.width / 2;

        sequence = DOTween.Sequence();

        if (left) {
            _script1.enabled = false;
            _button1.DOKill();
            sequence.Join(_button1.DOAnchorPosX(halfWidth, 0.3f).SetEase(Ease.OutQuad));
        } else {
            _script1.enabled = false;
            _button2.DOKill();
            sequence.Join(_button2.DOAnchorPosX(-halfWidth, 0.3f).SetEase(Ease.OutQuad));
        }

        sequence.Join(_mainGroup.DOFade(0, 0.3f).SetEase(Ease.OutQuad));
        sequence.AppendCallback(() => Hide());

        (left ? leftCb : rightCb).Invoke();
    }
}
