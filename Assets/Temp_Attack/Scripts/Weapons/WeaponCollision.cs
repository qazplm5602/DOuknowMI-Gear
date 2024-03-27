using FSM;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    private float _damageMultiply = 1f;
    protected virtual void OnTriggerEnter2D(Collider2D collision){
        if(!collision.CompareTag("Enemy") && !collision.CompareTag("Wall")) return;
        Enemy enemy = collision.GetComponent<Enemy>();
        GiveDamageToEnemy(enemy);
    }
    protected virtual void GiveDamageToEnemy(Enemy enemy){
        enemy.HealthCompo.ApplyDamage(Mathf.CeilToInt(_damageMultiply /** _player.Damage어딨는데*/), PlayerManager.instance.playerTrm);
    }
}
