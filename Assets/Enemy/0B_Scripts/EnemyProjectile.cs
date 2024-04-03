using UnityEngine;

public class EnemyProjectile : PoolableMono
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private LayerMask _whatIsEnemy;

    private float _timer = 0f;
    private bool _isInit = false;
    private Vector2 direction;

    private void Update() {
        if(!_isInit) return;
        
        Timer();
        Move();
    }

    public void Init(Vector2 direction) {
        this.direction = direction;

        _isInit = true;
    }

    private void Timer() {
        _timer += Time.deltaTime;
        if(_timer >= _lifeTime) {
            gameObject.SetActive(true);
        }
    }

    private void Move() {
        transform.position += (Vector3)direction.normalized * _speed * Time.deltaTime;
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.TryGetComponent(out IDamageable health)) {

        }
        if(other.gameObject.layer == _whatIsEnemy.value) gameObject.SetActive(false);
    }

    public override void ResetItem() {
        _timer = 0f;
        _isInit = false;
    }
}
