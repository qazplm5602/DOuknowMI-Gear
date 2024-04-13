using UnityEngine;

public class TestLinkT2 : GearCogEvent
{
    PlayerWeapon _weaponType = PlayerWeapon.BoltNutWheelPistonChain;

    public override void Use()
    {
        print("1 2 3 체인");
        Vector3 playerPos = _player.transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        float angle = Mathf.Atan2(mousePos.y - playerPos.y, mousePos.x - playerPos.x) * Mathf.Rad2Deg;
        Quaternion look = Quaternion.AngleAxis(angle, Vector3.forward);
        Instantiate(PlayerWeapons.instance.WeaponDictionary[_weaponType], playerPos, look);
    }
}
