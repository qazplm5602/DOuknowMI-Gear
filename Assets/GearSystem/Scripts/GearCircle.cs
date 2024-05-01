using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GearCircle : MonoBehaviour
{
    RectTransform _transform;
    [SerializeField] GameObject gearCogPrefab;
    public GearSO gearSO;
    float oneAngle;
    public bool reverse = false;
    int currentCog = 0;
    // 현재 콕 어디로 바라봄?
    public CogType currentCogType => gearSO.CogList[currentCog];

    Sequence sequence;

    // GameObject cog;
    
    private void Awake() {
        _transform = GetComponent<RectTransform>();
    }

    public void Init() {
        currentCog = 0;
        oneAngle = 360f / gearSO.CogList.Length;

        for (int i = 0; i < gearSO.CogList.Length; ++i) {
            var cog = Instantiate(gearCogPrefab, transform);
            var cog_image = cog.GetComponent<Image>();

            // 회전 ㄱㄱ
            cog.transform.RotateAround(transform.position, new Vector3(0,0,1), oneAngle * i * (reverse ? 1 : -1));

            // 색상 콕
            cog_image.color = GetCogTypeColor(gearSO.CogList[i]);
        }
    }

    public void Run(Action cb = null) {
        currentCog += 1;
        if (currentCog < 0) {
            currentCog = gearSO.CogList.Length - 1;
        } else if (gearSO.CogList.Length <= currentCog) {
            currentCog = 0;
        }

        sequence = DOTween.Sequence();
        sequence.Append(_transform.DOLocalRotate(new Vector3(0,0, currentCog * oneAngle * (reverse ? -1 : 1)), .3f));
        sequence.OnComplete(() => cb?.Invoke());
    }

    // 콕 색상
    static public Color GetCogTypeColor(CogType type) {
        switch (type)
        {
            case CogType.Skill:
                return new Color32(245, 105, 84, 255);
            case CogType.Link:
                return new Color32(0, 159, 255, 255);
            default:
                return new Color32(236, 161, 47, 255);
        }
    }
    static public Color GetLevelBorderColor(int level) {
        switch (level)
        {
            case 1:
                return Color.gray;
            case 2:
                return Color.yellow;
            case 3:
                return Color.green;
            case 4:
                return Color.red;
            case 5:
                return new Color32(95, 0, 255, 255);
        

            default:
                return Color.black;
        }
    }

    private void OnDestroy() {
        sequence?.Kill();
    }
}
