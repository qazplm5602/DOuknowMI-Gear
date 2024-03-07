using UnityEngine;

namespace FSM {
    public abstract class Enemy : Entity
    {
        [Header("Settings")]
        public float moveSpeed;
        public float idleTime;
        public float battleTime;

        protected float _defualtMoveSpeed;

        [SerializeField] protected LayerMask _playerLayer;

        [Header("Attack Settings")]
        public float attackRange;
        public float attackCooldown;
        [HideInInspector] public float lastAttackTime;

        protected int _lastAnimationBoolHash;

        protected override void Awake() {
            base.Awake();

            _defualtMoveSpeed = moveSpeed;
        }

        public virtual void AssignLastAnimationHash(int hashCode) {
            _lastAnimationBoolHash = hashCode;
        }

        public virtual int GetLastAnimationHash() => _lastAnimationBoolHash;

        public abstract void AnimationFinishTrigger();

        public override void ReturnDefaultSpeed() {
            moveSpeed = _defualtMoveSpeed;
            AnimatorCompo.speed = 1f;
        }
    }
}
