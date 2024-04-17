using System.Collections;
using UnityEngine;

public class EnemyLJ_KColumn : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 1f;
    [SerializeField] private float _appearTime;
    [SerializeField] private float _disappearTime;
    [SerializeField] private int _damage;

    private bool _damageable = true;

    private void Start() {
        StartCoroutine(Appear());
    }

    private IEnumerator Appear() {
        float timer = 0f;
        Vector2 originPosition = transform.position;
        Vector2 endPosition = transform.position + Vector3.up * transform.localScale.y;

        while(timer < _appearTime) {
            timer += Time.deltaTime;

            transform.position = Vector2.Lerp(originPosition, endPosition, timer / _appearTime);

            yield return null;
        }

        _damageable = false;
        yield return new WaitForSeconds(_lifeTime);

        timer = 0;
        while(timer < _disappearTime) {
            timer += Time.deltaTime;

            transform.position = Vector2.Lerp(endPosition, originPosition, timer / _disappearTime);

            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(_damageable && other.transform.TryGetComponent(out PlayerHealth playerHealth)) {
            playerHealth.ApplyDamage(_damage, transform);
        }
    }
}
