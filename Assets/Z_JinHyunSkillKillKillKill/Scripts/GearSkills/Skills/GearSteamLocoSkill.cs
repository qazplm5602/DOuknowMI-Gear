using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSteamLocoSkill : GearCogEvent
{
    private PlayerSkill _skillType = PlayerSkill.SteamLoco;

    public override void Use()
    {
        Vector3 playerPos = _player.transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion look = AngleManager.GetTargetDirection(playerPos, mousePos);

        GameObject prefab = PlayerSkillManager.Instance.playerSkill[_skillType];

        Instantiate(prefab, playerPos, look);

    }
}