using UnityEngine;

namespace FSM {
    public abstract class Enemy : Entity
    {
        [HideInInspector] public EnemyDamageCaster DamageCasterCompo;
        [HideInInspector] public EnemyHealth HealthCompo;

        [Header("Settings")]
        public float moveSpeed;
        public float idleTime;
        public float battleTime;

        protected float _defualtMoveSpeed;

        [Header("Check Settings")]
        [SerializeField] protected float _checkDistance;
        [SerializeField] protected Transform _checkTransform;
        public LayerMask whatIsPlayer;

        [Header("Attack Settings")]
        public Vector2 attackRange;
        public Vector2 attackOffset;
        public int attackDamage;
        public float attackCooldown;
        [HideInInspector] public float lastAttackTime;

        [Header("ETC Settings")]
        public int experienceValue;

        protected int _lastAnimationBoolHash;

        protected override void Awake() {
            base.Awake();
            DamageCasterCompo = GetComponent<EnemyDamageCaster>();
            HealthCompo = GetComponent<EnemyHealth>();
            HealthCompo.SetOwner(this);

            _defualtMoveSpeed = moveSpeed;
        }

        public virtual void AssignLastAnimationHash(int hashCode) {
            _lastAnimationBoolHash = hashCode;
        }

        public virtual int GetLastAnimationHash() => _lastAnimationBoolHash;

        public abstract void AnimationFinishTrigger();

        public virtual RaycastHit2D IsPlayerDetected()
            => Physics2D.Raycast(_checkTransform.position, Vector2.right * FacingDirection, _checkDistance, whatIsPlayer);

        public bool CanAttack() {
            return Time.time >= lastAttackTime + attackCooldown;
        }

        public override void ReturnDefaultSpeed() {
            moveSpeed = _defualtMoveSpeed;
            AnimatorCompo.speed = 1f;
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(_checkTransform.position, Vector3.right * FacingDirection * _checkDistance);
        }
    }
}
