using UnityEngine;

public class GearLensSkill : GearCogEvent
{
    public static GameObject _lensPrefab = null;
    public override void Use()
    {

        print(_player);
        if(_lensPrefab != null)
        {
            print("이미있어서삭제");
            Destroy(_lensPrefab);
        }
        Vector3 playerPos = _player.transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        playerPos += Vector3.up * 2;
        GameObject go = Instantiate(_prefab, playerPos, Quaternion.identity,_player.transform);
        _lensPrefab = go;
    }
}
