using UnityEngine;

public class EnemyDamageCaster : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsEnemy;

    public bool Damage(int damage, Vector2 position, Vector2 range) {
        Debug.Log("공격시도");
        Collider2D[] cols = Physics2D.OverlapBoxAll(position, range, 0, _whatIsEnemy);

        foreach(Collider2D col in cols) {
            if(col.TryGetComponent(out IDamageable target)) {
                Debug.Log($"{col.gameObject.name}(이)가 맞음");
                target.ApplyDamage(damage, transform);
            }
        }
        return cols.Length > 0;
    }
}
