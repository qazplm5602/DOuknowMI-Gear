using FSM;
using UnityEngine;

public class WheelController : WeaponCollision
{
    [SerializeField] private Transform _visualTrm;
    [SerializeField] private GameObject _boltFXPrefab;
    [SerializeField] private float _speed = 8f;
    private float _gearDamageMultiply = 5f;

    private float _rotationSpeed = 1500f;
    private void FixedUpdate()
    {
        transform.position += _speed * Time.fixedDeltaTime * transform.right;
        _visualTrm.Rotate(0, 0, _rotationSpeed * Time.fixedDeltaTime);
    }

    protected override void GiveDamageToEnemy(Enemy enemy)
    {
        //Instantiate(_WheelFXPrefab, transform.position, Quaternion.identity);
        enemy.HealthCompo.ApplyDamage(Mathf.CeilToInt(_gearDamageMultiply /* * _player.Damage어딨는데*/));
    }
}
