using UnityEngine;

public class EnemyDamageCaster : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsEnemy;

    public void Damage(int damage, Vector2 position, Vector2 range) {
        Collider2D[] cols = Physics2D.OverlapBoxAll(position, range, 0, _whatIsEnemy);

        foreach(Collider2D col in cols) {
            if(col.TryGetComponent(out IDamageable target)) {
                target.ApplyDamage(damage, transform);
            }
        }
    }
}
