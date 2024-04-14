using System.Collections;
using UnityEngine;

public class TwoGearSkill : GearCogEvent
{
    PlayerWeapon _weaponType = PlayerWeapon.Wheel;


    public override void Use()
    {
        Quaternion look = GetTargetDirection();
        Vector3 playerPos = _player.transform.position;
        Instantiate(PlayerWeapons.instance.WeaponDictionary[_weaponType], playerPos, look);
    }

    private Quaternion GetTargetDirection()
    {
        Vector3 playerPos = _player.transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePos.y - playerPos.y, mousePos.x - playerPos.x) * Mathf.Rad2Deg;
        Quaternion lookRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        return lookRotation;
    }
    
    //private IEnumerator MoveSkillGameObejct(float maxDistance, )
}
