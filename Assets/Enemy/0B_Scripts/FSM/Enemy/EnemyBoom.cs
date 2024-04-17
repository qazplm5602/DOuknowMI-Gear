using UnityEngine;

public class EnemyBoom : PoolableMono
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _attackRadius = 1.5f;
    [SerializeField] private LayerMask _whatIsEnemy;

    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    public override void ResetItem() {
        Boom();
    }

    private void Boom() {
        _animator.Play("Boom");

        Collider2D cols = Physics2D.OverlapCircle(transform.position, _attackRadius, _whatIsEnemy);

        if(cols && cols.TryGetComponent<IDamageable>(out IDamageable health)) {
            health.ApplyDamage(_damage, transform);
        }
    }

    private void AnimationFinishTrigger() {
        PoolManager.Instance.Push(this);
    }

    private void OnDrawGizmos() {
        Gizmos.color = new Color(1, 0.5f, 0);
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }

}