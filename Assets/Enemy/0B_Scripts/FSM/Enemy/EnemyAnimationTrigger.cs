using UnityEngine;

namespace FSM {
    public class EnemyAnimationTrigger : MonoBehaviour
    {
        private Enemy _enemy;

        private void Awake() {
            _enemy = transform.parent.GetComponent<Enemy>();
        }

        private void AnimationFinishTrigger() {
            _enemy.AnimationFinishTrigger();
        }

        private void AnimationAttackTrigger() {
            _enemy.Attack();
        }
    }
}
