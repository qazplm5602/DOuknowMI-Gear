using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GearEnforceUI : MonoBehaviour
{
    [SerializeField] GameObject _mainBox;
    [SerializeField] GearEnforceUI_SkillBox _nowSkill;
    [SerializeField] GearEnforceUI_SkillBox _enforceSkill;
    
    [SerializeField] GameObject _payBoxNasa;
    [SerializeField] GameObject _payBoxSkill;
    
    [SerializeField] int[] _paymentCoin;

    /////////////// cache
    TextMeshProUGUI subtitle;
    Image paySkillIco;
    TextMeshProUGUI payNasaT;
    
    private void Awake() {
        subtitle = _mainBox.transform.Find("question").GetComponent<TextMeshProUGUI>();

        paySkillIco = _payBoxSkill.transform.Find("Img").GetComponent<Image>();
        payNasaT = _payBoxSkill.transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    public void Show(GearGroupDTO gear) {
        subtitle.text = $"{gear.data.Name}의 기어를 강화 하시겠습니까?";

        _nowSkill.SetSkill(gear);

        GearStat currentStat = gear.stat;
        currentStat.level ++;
        _enforceSkill.SetSkill(new GearGroupDTO() { 
            data = gear.data,
            stat = currentStat
        });


        _mainBox.SetActive(true);
    }

    public void Hide() {
        _mainBox.SetActive(false);
    }
}
