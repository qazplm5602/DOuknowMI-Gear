using UnityEngine;


namespace FSM {
    [RequireComponent(typeof(EnemyDamageCaster), typeof(EnemyHealth))]
    public abstract class Enemy : Entity
    {
        [HideInInspector] public EnemyDamageCaster DamageCasterCompo;
        [HideInInspector] public EnemyHealth HealthCompo;

        [Header("Move Settings")]
        public float moveSpeed;

        protected float _defualtMoveSpeed;

        [Header("Check Settings")]
        public float nearDistance;
        [SerializeField] private  LayerMask _whatIsPlayer;
        [SerializeField] private LayerMask _whatIsObstacle;

        [Header("Attack Settings")]
        public Vector2 attackRange;
        public Vector2 attackOffset;
        public int attackDamage;
        public float attackCooldown;
        [HideInInspector] public float lastAttackTime;

        [Header("ETC Settings")]
        public DropTableSO dropTable;

        protected int _lastAnimationBoolHash;

        protected override void Awake() {
            base.Awake();
            DamageCasterCompo = GetComponent<EnemyDamageCaster>();
            HealthCompo = GetComponent<EnemyHealth>();
            HealthCompo.SetOwner(this);

            _defualtMoveSpeed = moveSpeed;
            lastAttackTime = -attackCooldown;
        }

        public virtual void AssignLastAnimationHash(int hashCode) {
            _lastAnimationBoolHash = hashCode;
        }

        public virtual int GetLastAnimationHash() => _lastAnimationBoolHash;

        public abstract void AnimationFinishTrigger();

        public bool CanAttack() {
            return Time.time >= lastAttackTime + attackCooldown;
        }

        public override void ReturnDefaultSpeed() {
            moveSpeed = _defualtMoveSpeed;
            AnimatorCompo.speed = 1f;
        }

        public virtual bool IsPlayerDetected(Vector2 checkOffset, Vector2 checkRange) {
            return Physics2D.OverlapBox((Vector2)transform.position + checkOffset * FacingDirection, checkRange, 0, _whatIsPlayer);
        }

        public virtual bool IsObstacleInLine(float distance, Vector3 direction) {
            return Physics2D.Raycast(transform.position, direction, distance, _whatIsObstacle);
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube((Vector2)transform.position + attackOffset * FacingDirection, attackRange);
        }
    }
}
