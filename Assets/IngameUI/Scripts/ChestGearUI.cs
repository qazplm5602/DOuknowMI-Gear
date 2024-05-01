using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestGearUI : MonoBehaviour
{
    public CanvasGroup _group;
    [SerializeField] GameObject cogPrefab;
    [SerializeField] TextMeshProUGUI _name;
    
    public GearSO _gear;

    public void Init() {
        _group = GetComponent<CanvasGroup>();
        _group.alpha = 0;

        Color color = _name.color;
        color.a = 0;
        _name.color = color;

        for (int i = 0; i < _gear.CogList.Length; ++i) {
            var cog = Instantiate(cogPrefab, transform);
            var cog_image = cog.GetComponent<Image>();

            cog.transform.RotateAround(transform.position, new Vector3(0,0,1), 360f / _gear.CogList.Length * i);
            cog_image.color = GearCircle.GetCogTypeColor(_gear.CogList[i]);
        }
    }
}
