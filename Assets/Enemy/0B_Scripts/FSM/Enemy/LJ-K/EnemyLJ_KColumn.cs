using System.Collections;
using UnityEngine;

public class EnemyLJ_KColumn : MonoBehaviour
{
    public int index = 0;

    [SerializeField] private float _lifeTime = 1f;
    [SerializeField] private float _appearTime;
    [SerializeField] private float _disappearTime;
    [SerializeField] private int _damage;

    [SerializeField] private BoxCollider2D _collider;

    private bool _damageable = true;

    private void Start() {
        transform.GetChild(0).GetChild(index).gameObject.SetActive(true);
        _collider.offset = new Vector3(0, 0.5f + 0.5f * index);
        _collider.size = new Vector3(3, 5 + index);

        SoundManager.Instance.PlaySound("ShotGun Shot Single Shot Interior");

        StartCoroutine(Appear());
    }

    private IEnumerator Appear() {
        float timer = 0f;
        Vector2 originPosition = transform.position;
        Vector2 endPosition = transform.position + Vector3.up * (7f + index);

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
