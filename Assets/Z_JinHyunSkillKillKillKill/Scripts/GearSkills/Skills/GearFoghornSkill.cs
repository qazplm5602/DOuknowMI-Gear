using UnityEngine;

public class GearFoghornSkill : GearCogEvent
{
    private PlayerSkill _skillType = PlayerSkill.Foghorn;

    public override void Use()
    {
        Vector3 playerPos = _player.transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion look = AngleManager.GetTargetDirection(playerPos, mousePos);

        GameObject prefab = PlayerSkillManager.Instance.playerSkill[_skillType];

        Instantiate(prefab, playerPos, look);

    }
}