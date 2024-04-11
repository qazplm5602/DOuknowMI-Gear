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
        float randomMultiplier = Random.Range(0.5f, 0.75f);
        Vector2 dir = new Vector2(spawnX, Mathf.Abs(spawnX) * randomMultiplier);
        Debug.Log(dir);

        _rigidbody.AddForce(dir * _force, ForceMode2D.Impulse);
        _rigidbody.AddTorque(10 * randomMultiplier);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.TryGetComponent(out PlayerHealth playerHealth)) {
            playerHealth.ApplyDamage(_damage, transform);
        }
    }
}
