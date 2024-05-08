using UnityEngine;

public class ChainMHSkill : GearCogEvent
{
    private GameObject _shPrefab;
    private GameObject _mhPrefab;
    public override void Use()
    {
        _shPrefab = GearManager.Instance.ScriptModule.GetSkillScript("SHGear")._prefab;
        _mhPrefab = GearManager.Instance.ScriptModule.GetSkillScript("MHGear")._prefab;

        Vector3 playerPos = _player.transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Quaternion look = AngleManager.Instance.GetTargetDirection(playerPos, mousePos);

        GameObject parent = Instantiate(_prefab, playerPos, look);
        GameObject sh = Instantiate(_shPrefab, parent.transform);
        GameObject mh1 = Instantiate(_mhPrefab, parent.transform);
        GameObject mh2 = Instantiate(_mhPrefab, parent.transform);

        sh.transform.localPosition = Vector2.zero;
        mh1.transform.localPosition = new Vector2(0, 0.5f);
        mh2.transform.localPosition = new Vector2(0, -0.5f);
    }
}
