using FSM;
using UnityEngine;

public class FoghornChainController : WeaponCollision
{
    [SerializeField] private Transform _visualTrm;
    [SerializeField] private GameObject _foghornChainFXPrefab;
    [SerializeField] private float _speed = 8f;

    private float _gearDamageMultiply = 10f;

    private void FixedUpdate()
    {
        transform.position += _speed * Time.fixedDeltaTime * transform.right;
    }

    protected override void GiveDamageToEnemy(Enemy enemy)
    {
        //Instantiate(_WheelChainFXPrefab, transform.position, Quaternion.identity);
        enemy.HealthCompo.ApplyDamage(Mathf.CeilToInt(_gearDamageMultiply /* * _player.Damage¾îµø´Âµ¥*/), PlayerManager.instance.playerTrm);
    }
}
