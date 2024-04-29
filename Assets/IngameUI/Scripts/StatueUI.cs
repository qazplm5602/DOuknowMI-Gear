using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StatueUI : MonoBehaviour
{
    [SerializeField] CanvasGroup _mainGroup;
    [SerializeField] RectTransform _button1;
    [SerializeField] RectTransform _button2;

    bool init = false;

    CanvasGroup group1;
    CanvasGroup group2;

    Sequence sequence;

    private void Awake() {
        if (init) return;
        init = true;
        
        group1 = _button1.GetComponent<CanvasGroup>();
        group2 = _button2.GetComponent<CanvasGroup>();

        _button1.GetComponent<Button>().onClick.AddListener(() => ClickButton(true));
        _button2.GetComponent<Button>().onClick.AddListener(() => ClickButton(false));
    }

    public void Show() {
        Awake();
        
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
        gameObject.SetActive(false);

        _mainGroup.alpha = group1.alpha = group2.alpha = 0;
        _mainGroup.blocksRaycasts = group1.blocksRaycasts = group2.blocksRaycasts = false;

        _button1.anchoredPosition = new Vector2(100, 0);
        _button2.anchoredPosition = new Vector2(-100, 0);
    }

    void ClickButton(bool left) {
        _mainGroup.blocksRaycasts = group1.blocksRaycasts = group2.blocksRaycasts = false;

        if (left) {
            
        } else {
            
        }
    }
}
