using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : PoolableMono
{
    private Transform _healthBarTransform;

    public void Init() {
        _healthBarTransform = null;
        GameManager.Instance.ShowHealthBarEvent -= ShowAndHide;
    }

    public void Init(Transform healthBarTransform, float scale) {
        _healthBarTransform = healthBarTransform;
        transform.localScale = Vector3.one * scale;
        GameManager.Instance.ShowHealthBarEvent += ShowAndHide;
    }

    private void OnDestroy() {
        if (!GameManager.Instance) return;
        GameManager.Instance.ShowHealthBarEvent -= ShowAndHide;
    }

    private void FixedUpdate() {
        if(_healthBarTransform != null) {
            transform.position = Camera.main.WorldToScreenPoint(_healthBarTransform.position);
        }
    }

    private void ShowAndHide(bool flag) {
        gameObject.SetActive(flag);
    }

    public override void ResetItem() {
        transform.SetParent(PoolManager.Instance._poolingCanvas.transform);
        _healthBarTransform = null;
        transform.GetChild(0).GetComponent<Image>().fillAmount = 1f;
    }
}
