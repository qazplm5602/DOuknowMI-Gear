using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainCameraSkill : GearCogEvent
{
    public override void Use()
    {
        Vector3 playerPos = _player.transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Quaternion look = AngleManager.GetTargetDirection(playerPos, mousePos);

        Instantiate(_prefab, playerPos - new Vector3(0, 0.5f, 0), Quaternion.identity);
    }
}
