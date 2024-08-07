using UnityEngine;

public class GearClockSkill : GearCogEvent
{

    public override void Use()
    {
        Vector3 playerPos = _player.transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Quaternion look = AngleManager.Instance.GetTargetDirection(playerPos, mousePos);

        Instantiate(_prefab, playerPos, look);
    }
}