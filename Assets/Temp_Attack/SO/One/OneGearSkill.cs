using UnityEngine;

public class OneGearSkill : GearCogEvent
{
    [SerializeField] private GameObject _boltPrefab;
    public override void Use()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mousePos - transform.position;
        Quaternion look = Quaternion.LookRotation(dir);
        Instantiate(_boltPrefab, transform.position, look);
    }
}
