using UnityEngine;

public class GearLensSkill : GearCogEvent
{
    public override void Use()
    {
        Vector3 playerPos = _player.transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        playerPos += Vector3.up * 2;
        Instantiate(_prefab, playerPos, Quaternion.identity,_player.transform);
    }
}
