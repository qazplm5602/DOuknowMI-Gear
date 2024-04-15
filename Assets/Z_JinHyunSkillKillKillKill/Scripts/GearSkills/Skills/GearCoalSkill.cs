using UnityEngine;

public class GearCoalSkill : GearCogEvent
{
    private PlayerSkill _skillType = PlayerSkill.Coal;

    public override void Use()
    {
        Vector3 playerPos = _player.transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion look = AngleManager.GetTargetDirection(playerPos, mousePos);

        GameObject prefab = PlayerSkillManager.Instance.playerSkill[_skillType];

        Instantiate(prefab, playerPos, look);

    }
}