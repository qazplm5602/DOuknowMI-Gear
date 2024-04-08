using UnityEngine;

public class EnemyProjectile : PoolableMono
{
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _whatIsEnemy;

    private float _lifeTime;
    private float _speed;
    private float _timer = 0f;
    private bool _isInit = false;
    private Vector2 _direction;

    private void Update() {
        if(!_isInit) return;
        
        Timer();
        Move();
    }

    public void Init(float lifeTime, float speed, Vector2 direction) {
        _lifeTime = lifeTime;
        _speed = speed;
        _direction = direction;

        _isInit = true;
    }

    private void Timer() {
        _timer += Time.deltaTime;
        if(_timer >= _lifeTime) {
            gameObject.SetActive(true);
        }
    }

    private void Move() {
        transform.position += (Vector3)_direction.normalized * _speed * Time.deltaTime;
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.TryGetComponent(out IDamageable health)) {
            health.ApplyDamage(_damage, transform);
        }
        int otherLayer = 1 << other.gameObject.layer;
        if((otherLayer & _whatIsEnemy.value) > 0) {
            gameObject.SetActive(false);
        }
    }

    public override void ResetItem() {
        _timer = 0f;
        _isInit = false;
    }
}
