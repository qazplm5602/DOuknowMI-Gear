using System.Collections;
using UnityEngine;
using TMPro;

public class DamageText : PoolableMono
{
    [SerializeField] private float _lifetime = 0.5f;
    [SerializeField] private float _fadeOutTime = 0.3f;
    [SerializeField] private float _speed = 3f;
    private TextMeshPro tmPro;

    private void Awake() {
        tmPro = GetComponent<TextMeshPro>();
    }

    public void Init(int damage) {
        tmPro.text = damage.ToString();
        StartCoroutine(FadeOut());
    }

    private void Update() {
        transform.position += Vector3.up * Time.deltaTime * _speed;
    }

    private IEnumerator FadeOut() {
        yield return new WaitForSeconds(_lifetime);

        float timer = 0f;
        while(timer < _fadeOutTime) {
            timer += Time.deltaTime;

            float alpha = 1 - (timer / _fadeOutTime);
            tmPro.color = new Color(1, 1, 1, alpha);

            yield return null;
        }
        PoolManager.Instance.Push(this);
    }

    public override void ResetItem() {
        tmPro.color = new Color(1, 1, 1, 1);
    }
    
}
