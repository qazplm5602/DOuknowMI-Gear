using System.Collections;
using UnityEngine;

public class EnemyBoom : PoolableMono
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _attackRadius = 1.5f;
    [SerializeField] private LayerMask _whatIsEnemy;

    private readonly int _boomAnimHash = Animator.StringToHash("Boom");

    private float _defaultRadius;
    private Animator _animator;

    private void Awake() {
        _animator = GetComponentInChildren<Animator>();

        _defaultRadius = _attackRadius;
    }

    public override void ResetItem() {
        
    }

    public void Init(float size, int damage) {
        transform.localScale = Vector3.one * size / 2;
        _attackRadius = size;
        _damage = damage;

        Boom();
    }

    private void Boom() {
        _animator.Play(_boomAnimHash);

        Collider2D col = Physics2D.OverlapCircle(transform.position, _attackRadius, _whatIsEnemy);

        SoundManager.Instance.PlaySound("Explosion 1");

        if(col && col.TryGetComponent(out PlayerHealth health)) {
            health.ApplyDamage(_damage, transform);
        }
    }

    public void AnimationFinishTrigger() {
        PoolManager.Instance.Push(this);
    }

    private void OnDrawGizmos() {
        Gizmos.color = new Color(1, 0.5f, 0);
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
}
