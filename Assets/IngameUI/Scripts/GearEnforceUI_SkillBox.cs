using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GearEnforceUI_SkillBox : MonoBehaviour
{
    [SerializeField] Image _icon;
    [SerializeField] Image _border;
    [SerializeField] TextMeshProUGUI _level;

    public void SetSkill(GearGroupDTO gear) {
        _icon.sprite = gear.data.Icon;
        _level.text = $"+{gear.stat.level}";
        
        _border.enabled = gear.stat.level != 0;
        if (_border.enabled)
            _border.color = GearCircle.GetLevelBorderColor(gear.stat.level);
    }
}
