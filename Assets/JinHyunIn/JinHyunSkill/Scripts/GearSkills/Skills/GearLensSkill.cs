using UnityEngine;

public class GearLensSkill : GearCogEvent
{
    public static GameObject _lensPrefab = null;
    public override void Use()
    {
        if(_lensPrefab != null)
        {
            Destroy(_lensPrefab);
        }
        Vector3 playerPos = _player.transform.position;
        playerPos += Vector3.up * 2;
        GameObject go = Instantiate(_prefab, playerPos, Quaternion.identity,_player.transform);
        _lensPrefab = go;
    }
}
