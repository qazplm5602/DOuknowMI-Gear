using FSM;
using UnityEngine;

public class WheelChainController : WeaponCollision
{
    [SerializeField] private Transform _visualTrm;
    [SerializeField] private GameObject _WheelChainFXPrefab;
    [SerializeField] private float _speed = 8f;

    private float _gearDamageMultiply = 10f;

    private float _rotationSpeed = 1500f;
    private int sign = 1;
    private void Start()
    {
        if (Mathf.Abs(transform.rotation.z) >= 90)
        {
            sign = -1;
            _rotationSpeed *= sign;
        }
    }
    private void FixedUpdate()
    {
        transform.position += _speed * Time.fixedDeltaTime * transform.right;
        _visualTrm.Rotate(0, 0, _rotationSpeed * Time.fixedDeltaTime * sign);
    }

    protected override void GiveDamageToEnemy(Enemy enemy)
    {
        //Instantiate(_WheelChainFXPrefab, transform.position, Quaternion.identity);
        enemy.HealthCompo.ApplyDamage(Mathf.CeilToInt(_gearDamageMultiply /* * _player.Damage어딨는데*/));
    }
}
