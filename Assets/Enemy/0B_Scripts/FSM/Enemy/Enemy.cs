using UnityEngine;
using UnityEngine.UI;

namespace FSM {
    [RequireComponent(typeof(EnemyDamageCaster), typeof(EnemyHealth))]
    public abstract class Enemy : Entity
    {
        [HideInInspector] public EnemyDamageCaster DamageCasterCompo;
        [HideInInspector] public EnemyHealth HealthCompo;

        [Header("Check Settings")]
        public float nearDistance;
        [SerializeField] private  LayerMask _whatIsPlayer;
        public LayerMask whatIsObstacle;

        [Header("Attack Settings")]
        public Vector2 attackRange;
        public Vector2 attackOffset;
        public int attackDamage;
        [HideInInspector] public float lastAttackTime;

        [Header("HealthBar Settings")]
        public Transform healthBarTransform;
        public float healthBarScale = 1f;

        [Header("ETC Settings")]
        public DropTableSO dropTable;

        protected int _lastAnimationBoolHash;
        protected EnemyHealthBar _healthBar;

        protected override void Awake() {
            base.Awake();
            DamageCasterCompo = GetComponent<EnemyDamageCaster>();
            HealthCompo = GetComponent<EnemyHealth>();
            HealthCompo.SetOwner(this);

            lastAttackTime = -Stat.attackCooldown.GetValue();

            _healthBar = PoolManager.Instance.Pop(PoolingType.HealthBar) as EnemyHealthBar;
            _healthBar.Init(healthBarTransform, healthBarScale);
            HealthCompo.healthFilled = _healthBar.transform.Find("Filled").GetComponent<Image>();
            _healthBar.gameObject.SetActive(GameManager.Instance.ShowHealthBar);
        }

        protected virtual void Update() {
            AnimatorCompo.speed = Stat.attackSpeed.GetValue();
        }

        public virtual void AssignLastAnimationHash(int hashCode) {
            _lastAnimationBoolHash = hashCode;
        }

        public virtual int GetLastAnimationHash() => _lastAnimationBoolHash;

        public abstract void AnimationFinishTrigger();

        public bool CanAttack() {
            return Time.time >= lastAttackTime + Stat.attackCooldown.GetValue();
        }

        public override void ReturnDefaultSpeed() {
            AnimatorCompo.speed = 1f;
        }

        public virtual bool IsPlayerDetected(Vector2 checkOffset, Vector2 checkRange) {
            return Physics2D.OverlapBox((Vector2)transform.position + checkOffset * FacingDirection, checkRange, 0, _whatIsPlayer);
        }

        public virtual bool IsObstacleInLine(float distance, Vector3 direction) {
            return Physics2D.Raycast(transform.position, direction, distance, whatIsObstacle);
        }

        public virtual void SetDead() {
            PoolManager.Instance.Push(_healthBar);
            DropItems();
        }

        public virtual void DropItems() {
            int randomSmallPartAmount = Random.Range(dropTable.smallPartAmount.x, dropTable.smallPartAmount.y + 1);
            int randomBigPartAmount = Random.Range(dropTable.bigPartAmount.x, dropTable.bigPartAmount.y + 1);
            for(int i = 0; i < randomSmallPartAmount; ++i) {
                Transform trm = PoolManager.Instance.Pop(PoolingType.SmallPart).transform;
                trm.position = transform.position;
            }
            for(int i = 0; i < randomBigPartAmount; ++i) {
                Transform trm = PoolManager.Instance.Pop(PoolingType.BigPart).transform;
                trm.position = transform.position;
            }
        }

        protected virtual void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube((Vector2)transform.position + attackOffset * FacingDirection, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, nearDistance);
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(healthBarTransform.position, new Vector2(4.173373f, 0.5711204f) * healthBarScale);
        }
    }
}
