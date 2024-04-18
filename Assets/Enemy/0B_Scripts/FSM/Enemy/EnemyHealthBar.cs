using UnityEngine;

public class EnemyHealthBar : PoolableMono
{
    private Transform _healthBarTransform;

    public void Init(Transform healthBarTransform, float scale) {
        _healthBarTransform = healthBarTransform;
        transform.localScale = Vector3.one * scale;
    }

    private void FixedUpdate() {
        if(_healthBarTransform != null) {
            transform.position = Camera.main.WorldToScreenPoint(_healthBarTransform.position);
        }
    }

    public override void ResetItem() {
        transform.SetParent(PoolManager.Instance._poolingCanvas.transform);
        _healthBarTransform = null;
    }
}
