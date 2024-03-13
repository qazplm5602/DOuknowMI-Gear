using System.Collections;
using System.Collections.Generic;
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
            cog.transform.RotateAround(transform.position, new Vector3(0,0,1), oneAngle * i);

            // 색상 콕
            cog_image.color = GetCogTypeColor(gearSO.CogList[i]);
        }
    }

    public void Run() {
        currentCog += reverse ? -1 : 1;
        if (currentCog < 0) {
            currentCog = gearSO.CogList.Length - 1;
        } else if (gearSO.CogList.Length < currentCog) {
            currentCog = 0;
        }

        
    }

    // 콕 색상
    Color GetCogTypeColor(CogType type) {
        switch (type)
        {
            case CogType.Skill:
                return Color.red;
            case CogType.Link:
                return Color.blue;
            default:
                return Color.white;
        }
    }
}
