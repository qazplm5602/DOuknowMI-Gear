using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GearChangeUI : MonoBehaviour
{
    [SerializeField] Transform _content;
    
    //////////////// 설명 관련
    [Header("Description")]
    [SerializeField] Image desc_image;
    [SerializeField] TextMeshProUGUI desc_title;
    [SerializeField] TextMeshProUGUI desc_subText;
    [SerializeField] TextMeshProUGUI desc_script;

    /////////////// 기어 관련
    [Header("Gear")]
    [SerializeField] GameObject _gearPrefab;
    [SerializeField] GameObject _cogPrefab;
    [SerializeField] Vector2 _gearSize; // 기어 사이즈 강제 변환

    [SerializeField] GearSO[] _gearDatas; // 테스트만 하고 삭제 예정 (나중에 gearManager에서 가져올 예정)

    List<GearSO> gearDatas;

    private void Awake() {
        gearDatas = new();

        foreach (var item in _gearDatas)
            GearAdd(item);
    }

    void GearAdd(GearSO gearInfo) {
        gearDatas.Add(gearInfo);

        GameObject gearObj = Instantiate(_gearPrefab, _content);
        RectTransform gearTrm = gearObj.GetComponent<RectTransform>();

        Vector2 ratioSize = _gearSize / gearTrm.sizeDelta;
        gearTrm.sizeDelta = _gearSize;
        
        gearObj.GetComponent<GearChangeHoverEvent>().OnHoverEvent += (isHover) => {
            if (isHover) {
                ShowDescription(gearInfo);
            } else {
                HideDescription();
            }
        };

        // 콕 소환
        int i = 0;
        foreach (var item in gearInfo.CogList)
        {
            GameObject cogObj = Instantiate(_cogPrefab, gearTrm);
            RectTransform cogTrm = cogObj.GetComponent<RectTransform>();

            cogTrm.sizeDelta = new Vector2(cogTrm.sizeDelta.x * ratioSize.x, cogTrm.sizeDelta.y * ratioSize.y);
            cogTrm.anchoredPosition = new Vector2(cogTrm.anchoredPosition.x * ratioSize.x, cogTrm.anchoredPosition.y * ratioSize.y);

            cogObj.transform.RotateAround(gearTrm.position, new Vector3(0, 0, 1), (360f / gearInfo.CogList.Length) * -i);
            
            // 색상 지정
            if (item != CogType.None)
                cogObj.GetComponent<Image>().color = GearCircle.GetCogTypeColor(item);

            i++;
        }
    }

    void GearRemove(int idx) {

    }

    // 설명띄움
    void ShowDescription(GearSO gearInfo) {
        desc_title.text = gearInfo.Name;
        desc_image.sprite = gearInfo.Icon;
        desc_subText.text = $"톱니개수: {gearInfo.CogList.Length}개{(gearInfo.ActiveDamage ? $"\n기본 데미지: {gearInfo.DefaultDamage}" : "")}{(gearInfo.ActiveRange ? $"\n기본 범위: {gearInfo.DefaultRange}" : "")}";
        desc_script.text = gearInfo.Desc;
    }

    void HideDescription() {
        desc_title.text = "";
        desc_image.sprite = null;
        desc_subText.text = "";
        desc_script.text = "설명을 보려면 기어, 슬롯을 마우스에 올려두세요.";
    }
}
