using FSM;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    private Enemy _enemy;

    private float _damageMultiply = 1f;
    protected virtual void OnTriggerEnter2D(Collider2D collision){
        print("충돌하였슴");
        if(!collision.CompareTag("Enemy") && !collision.CompareTag("Wall")) return;
        print("적이나 벽임 마치 적벽대전의 성지 우한");
        if (collision.TryGetComponent<Enemy>(out _enemy))
        {
            GiveDamageToEnemy(_enemy);
        }
    }
    protected virtual void GiveDamageToEnemy(Enemy enemy){
        print("데미지줌");
        enemy.HealthCompo.ApplyDamage(Mathf.CeilToInt(_damageMultiply /** _player.Damage어딨는데*/), PlayerManager.instance.playerTrm);
    }
}
