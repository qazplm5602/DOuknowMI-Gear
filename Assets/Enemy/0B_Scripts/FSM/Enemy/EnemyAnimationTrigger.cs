using UnityEngine;

namespace FSM {
    public class EnemyAnimationTrigger : MonoBehaviour
    {
        [SerializeField] private string clipName;

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

        private void AnimationStepTrigger() {
            SoundManager.Instance.PlaySound(clipName);  
        }
    }
}
