using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearChangeUI : MonoBehaviour
{
    [SerializeField] Transform _content;
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
}
