using UnityEngine;

namespace FSM {
    public class EnemyAnimationTrigger : MonoBehaviour
    {
        protected Enemy _enemy;

        protected virtual void Awake() {
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
