using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System;

public class GearEnforceUI : MonoBehaviour
{
    [SerializeField] GameObject _mainBox;
    [SerializeField] GearEnforceUI_SkillBox _nowSkill;
    [SerializeField] GearEnforceUI_SkillBox _enforceSkill;
    
    [SerializeField] GameObject _payBoxNasa;
    [SerializeField] GameObject _payBoxSkill;
    
    [SerializeField] int[] _paymentCoin;

    // 이건 기어 교체창하고 연동하기 전까지는 임시로 사용하는 변수 (그니까 바로 키면 이 기어로 켜짐)
    [SerializeField, System.Obsolete] GearSO tempGear;

    /////////////// cache
    TextMeshProUGUI subtitle;
    Image paySkillIco;
    TextMeshProUGUI payNasaT;

    GearGroupDTO currentGear;
    Action<bool> callback;
    
    private void Awake() {
        subtitle = _mainBox.transform.Find("question").GetComponent<TextMeshProUGUI>();

        paySkillIco = _payBoxSkill.transform.Find("Img").GetComponent<Image>();
        payNasaT = _payBoxNasa.transform.Find("Text").GetComponent<TextMeshProUGUI>();
    
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

        _mainBox.SetActive(true);
    }

    public void Hide() {
        currentGear = null;
        callback = null;
        _mainBox.SetActive(false);
    }
}
