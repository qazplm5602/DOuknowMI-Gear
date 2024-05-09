using UnityEngine;

public class EnemyDamageCaster : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsEnemy;

    private Collider2D[] _cols;

    private void Start() {
        _cols = new Collider2D[10];
    }

    public bool Damage(int damage, Vector2 position, Vector2 range) {
        int count = Physics2D.OverlapBoxNonAlloc(transform.position + (Vector3)position, range, 0f, _cols, _whatIsEnemy);
        Debug.Log(count);

        if(count > 0) {
            foreach(Collider2D col in _cols) {
                Debug.Log(col.transform.name);
                if(col.TryGetComponent(out PlayerHealth target)) {
                    Debug.Log($"{col.gameObject.name}(이)가 맞음");
                    target.ApplyDamage(damage, transform);
                }
            }
        }
        return _cols.Length > 0;
    }
}
