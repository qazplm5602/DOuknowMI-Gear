using UnityEngine;

public class EnemyLJ_KStone : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 2f;
    [SerializeField] private float _force = 10f;
    [SerializeField] private int _damage = 1;

    private Rigidbody2D _rigidbody;
    
    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        Destroy(gameObject, _lifeTime);
    }

    public void Explode(float spawnX) {
        Vector2 dir = new Vector2(spawnX, Mathf.Sin(Mathf.Acos(spawnX / 4.5f)) * 4.5f);
        dir.Normalize();

        _rigidbody.AddForce(dir * _force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.TryGetComponent(out PlayerHealth playerHealth)) {
            playerHealth.ApplyDamage(_damage, transform);
        }
    }
}
