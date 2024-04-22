using UnityEngine;

public class EnemyBoomAnimationTrigger : MonoBehaviour
{
    private EnemyBoom _enemyBoom;

    private void Awake() {
        _enemyBoom = transform.parent.GetComponent<EnemyBoom>();
    }

    private void AnimationFinishTrigger() {
        _enemyBoom.AnimationFinishTrigger();
    }
}
