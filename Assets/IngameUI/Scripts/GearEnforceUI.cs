using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System;
using DG.Tweening;

public class GearEnforceUI : MonoBehaviour
{
    [SerializeField] GameObject _mainBox;
    [SerializeField] GearEnforceUI_SkillBox _nowSkill;
    [SerializeField] GearEnforceUI_SkillBox _enforceSkill;
    
    [SerializeField] GameObject _payBoxNasa;
    [SerializeField] GameObject _payBoxSkill;
    
    [SerializeField] int[] _paymentCoin;

    [Header("Anim")]
    [SerializeField] RectTransform _animScreen;
    [SerializeField] GearEnforceUI_SkillBox AnimBoxBefore;
    [SerializeField] GearEnforceUI_SkillBox AnimBoxAfter;

    // 이건 기어 교체창하고 연동하기 전까지는 임시로 사용하는 변수 (그니까 바로 키면 이 기어로 켜짐)
    [SerializeField, System.Obsolete] GearSO tempGear;

    /////////////// cache
    TextMeshProUGUI subtitle;
    Image paySkillIco;
    TextMeshProUGUI payNasaT;

    CanvasGroup AnimScreen_group;
    CanvasGroup AnimBoxBefore_group;
    CanvasGroup AnimBoxAfter_group;
    TextMeshProUGUI AnimText;

    GearGroupDTO currentGear;
    Action callback;
    
    private void Awake() {
        subtitle = _mainBox.transform.Find("question").GetComponent<TextMeshProUGUI>();

        paySkillIco = _payBoxSkill.transform.Find("Img").GetComponent<Image>();
        payNasaT = _payBoxNasa.transform.Find("Text").GetComponent<TextMeshProUGUI>();

        AnimBoxBefore_group = AnimBoxBefore.GetComponent<CanvasGroup>();
        AnimBoxAfter_group = AnimBoxAfter.GetComponent<CanvasGroup>();
        AnimText = _animScreen.Find("title").GetComponent<TextMeshProUGUI>();
        AnimScreen_group = _animScreen.GetComponent<CanvasGroup>();
    
        // 테스트 코드
        Show(new GearGroupDTO() { data = tempGear, stat = new() { level = 1 } });
    }

    public void Show(GearGroupDTO gear) {
        currentGear = gear;
        subtitle.text = $"{gear.data.Name}의 기어를 강화 하시겠습니까?";

        // 기어 박스 설정
        _nowSkill.SetSkill(gear);

        GearStat currentStat = gear.stat;
        currentStat.level ++;
        _enforceSkill.SetSkill(new GearGroupDTO() { 
            data = gear.data,
            stat = currentStat
        });
        
        // 소모 아이템 설정
        _payBoxSkill.SetActive(gear.stat.level == 4);
        if (gear.stat.level == 4) {
            paySkillIco.sprite = gear.data.Icon;
        }

        payNasaT.text = $"<color=red>0</color> / {_paymentCoin[gear.stat.level]}";

        EnorceAnimReset();
        _mainBox.SetActive(true);
    }

    public void Hide() {
        currentGear = null;
        callback = null;
        _mainBox.SetActive(false);
    }

    public void EnorceAnim() {
        EnorceAnimReset();
        _animScreen.gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence();
    
        RectTransform AnimBoxBefore_trm = AnimBoxBefore.transform as RectTransform;
        AnimBoxBefore.transform.localScale = new Vector3(.8f, .8f, 1);
        
        sequence.Append(AnimScreen_group.DOFade(1, 0.3f));

        sequence.Append(AnimBoxBefore.transform.DOScale(1, 0.5f));
        sequence.Join(AnimBoxBefore_group.DOFade(1, 0.5f));

        sequence.Append(AnimBoxBefore_trm.DOShakeAnchorPos(4f, 10, vibrato: 100, fadeOut: false).OnComplete(() => {
            AnimBoxBefore.gameObject.SetActive(false);

            // 위치는 같게
            (AnimBoxAfter.transform as RectTransform).anchoredPosition = AnimBoxBefore_trm.anchoredPosition;
            AnimBoxAfter.gameObject.SetActive(true);
        }));

        sequence.Append(AnimBoxAfter.transform.DOScale(2, 1).SetEase(Ease.OutCirc));
        sequence.Join(AnimBoxAfter.transform.DOLocalRotate(new Vector3(20, -20, 0), 1).SetEase(Ease.OutCirc));
        sequence.Append(AnimBoxAfter.transform.DOScale(1, 0.3f).SetEase(Ease.InOutQuad));
        sequence.Join(AnimBoxAfter.transform.DOLocalRotate(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad));
    
        sequence.Join(AnimText.DOFade(1, 0.3f).SetEase(Ease.InOutQuad));
    }

    void EnorceAnimReset() {
        _animScreen.gameObject.SetActive(false);
        AnimScreen_group.alpha = 0;

        AnimBoxBefore.gameObject.SetActive(true);
        AnimBoxAfter.gameObject.SetActive(false);

        (AnimBoxBefore.transform as RectTransform).anchoredPosition = Vector2.zero;
    }
}
