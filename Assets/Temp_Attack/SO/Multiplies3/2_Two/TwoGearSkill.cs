using System.Collections;
using UnityEngine;

public class TwoGearSkill : GearCogEvent
{
    PlayerWeapon _weaponType = PlayerWeapon.Wheel;


    public override void Use()
    {
        Transform spawnPos = _player.transform.GetChild(0);
        Quaternion look = GetTargetDirection();

        Instantiate(PlayerWeapons.instance.WeaponDictionary[_weaponType], spawnPos.position, look);
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
