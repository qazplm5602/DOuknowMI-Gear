using UnityEngine;

public class EnemyBombBoom : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _attackRadius = 1.5f;
    [SerializeField] private LayerMask _whatIsEnemy;

    private void Boom() {
        Collider2D cols = Physics2D.OverlapCircle(transform.position, _attackRadius, _whatIsEnemy);

        if(cols.TryGetComponent<IDamageable>(out IDamageable health)) {
            health.ApplyDamage(_damage, transform);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = new Color(1, 0.5f, 0);
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
}
