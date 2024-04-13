using UnityEngine;

public class OneGearSkill : GearCogEvent
{
    PlayerWeapon _weaponType = PlayerWeapon.BoltAndNut;

    public override void Use()
    {
        Transform spawnPos = _player.transform.GetChild(0);
        Vector3 playerPos = _player.transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        float angle = Mathf.Atan2(mousePos.y - playerPos.y, mousePos.x - playerPos.x) * Mathf.Rad2Deg;
        Quaternion look = Quaternion.AngleAxis(angle, Vector3.forward);
        Instantiate(PlayerWeapons.instance.WeaponDictionary[_weaponType], spawnPos.position, look);
    }
}
