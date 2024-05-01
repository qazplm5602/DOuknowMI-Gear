using FSM;
using UnityEngine;

public class BoltController : WeaponCollision
{
    [SerializeField] private float _speed = 8f;
    [SerializeField] private GameObject _boltFXPrefab;
    private float _gearDamageMultiply = 2f;
    private void FixedUpdate()
    {
        transform.position += _speed * Time.fixedDeltaTime * transform.right;
    }

    protected override void GiveDamageToEnemy(Enemy enemy)
    {
        //Instantiate(_boltFXPrefab, transform.position, Quaternion.identity);
        enemy.HealthCompo.ApplyDamage(Mathf.CeilToInt(_gearDamageMultiply /* * _player.Damage어딨는데*/), PlayerManager.instance.playerTrm);
    }
}
