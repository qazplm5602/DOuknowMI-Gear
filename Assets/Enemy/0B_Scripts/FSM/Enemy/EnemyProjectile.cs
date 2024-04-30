using UnityEngine;

public class EnemyProjectile : PoolableMono
{
    [SerializeField] protected int _damage;
    [SerializeField] protected LayerMask _whatIsEnemy;
    [SerializeField] protected LayerMask _whatIsGround;

    [Header("Effect Prefabs")]
    [SerializeField] private GameObject _impactPrefab;
    [SerializeField] private GameObject _wallPrefab;

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

    public void Init(float lifeTime, float speed, Vector2 direction, int damage) {
        _lifeTime = lifeTime;
        _speed = speed;
        _direction = direction;
        _damage = damage;

        _isInit = true;

        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }

    private void Timer() {
        _timer += Time.deltaTime;
        if(_timer >= _lifeTime) {
            PoolManager.Instance.Push(this);
        }
    }

    private void Move() {
        transform.position += (Vector3)_direction.normalized * _speed * Time.deltaTime;
    }
    
    protected virtual void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.TryGetComponent(out IDamageable health)) {
            health.ApplyDamage(_damage, transform);
        }

        int otherLayer = 1 << other.gameObject.layer;
        if((otherLayer & _whatIsEnemy.value) > 0) {
            gameObject.SetActive(false);
            Destroy(Instantiate(_impactPrefab, transform.position, Quaternion.identity), 1f);
        }
        else if((otherLayer & _whatIsGround.value) > 0) {
            gameObject.SetActive(false);
            Destroy(Instantiate(_wallPrefab, transform.position, Quaternion.identity), 1f);
        }
    }

    public override void ResetItem() {
        _timer = 0f;
        _isInit = false;
    }
}
