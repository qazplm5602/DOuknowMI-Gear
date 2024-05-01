using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonChainController : WeaponCollision
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private GameObject _pistonChainFXPrefab;
    private float _gearDamageMultiply = 2f;
    private void FixedUpdate()
    {
        transform.position += _speed * Time.fixedDeltaTime * transform.right;
    }

    protected override void GiveDamageToEnemy(Enemy enemy)
    {
        //Instantiate(_boltFXPrefab, transform.position, Quaternion.identity);
        enemy.HealthCompo.ApplyDamage(Mathf.CeilToInt(_gearDamageMultiply /* * _player.Damage¾îµø´Âµ¥*/), PlayerManager.instance.playerTrm);
    }
}
