using FSM;
using UnityEngine;

public class PistonController : WeaponCollision
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private GameObject _pistonFXPrefab;
    private float _gearDamageMultiply = 3f;
    private void FixedUpdate()
    {
        transform.position += _speed * Time.fixedDeltaTime * transform.right;
    }

    protected override void GiveDamageToEnemy(Enemy enemy)
    {
        //Instantiate(_pistonFXPrefab, transform.position, Quaternion.identity);
        enemy.HealthCompo.ApplyDamage(Mathf.CeilToInt(_gearDamageMultiply /* * _player.Damage어딨는데*/), PlayerManager.instance.playerTrm);
    }
}
