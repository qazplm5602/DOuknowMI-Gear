using UnityEngine;

public class TestLinkT2 : GearCogEvent
{
    PlayerWeapon _weaponType = PlayerWeapon.BoltNutWheelPistonChain;

    public override void Use()
    {
        print("1 2 3 체인");
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
}
