using System;
using UnityEngine;
using UnityEngine.Events;

namespace FSM {
    public class Entity : MonoBehaviour
    {
        [HideInInspector] public bool isDead = false;

        #region Component

        public Animator Animator;
        public Rigidbody2D Rigidbody;
        public SpriteRenderer SpriteRenderer;
        public Collider2D Collider;

        #endregion

        [Header("Collision Info")]
        [SerializeField] protected LayerMask _groundAndWallLayer;
        [SerializeField] protected Transform _groundChecker;
        [SerializeField] protected Vector2 _groundCheckRange;
        [SerializeField] protected Transform _wallChecker;
        [SerializeField] protected Vector2 _wallCheckRange;

        [Header("Knock Back Info")]
        [SerializeField] protected bool _knockBackable = true;
        [SerializeField] protected float _knockBackDuration;
        protected Coroutine _knockBackCoroutine = null;
        [HideInInspector] public bool _isKnockBacked = false;

        [Header("Stun Info")]
        [SerializeField] protected bool _stunable = true;
        public float stunDuration;
        public Vector2 stunPower;
        protected bool _canBeStun;

        [Space]
        [Header("Feedback Event")]
        public UnityEvent HitEvent;

        public Action<int> OnFlip;

        public int FacingDirection { get; protected set; } = 1;
        public Vector3 GroundPostion => new Vector3(transform.position.x, _groundChecker.position.y);
        public bool CanStateChangeable { get; set; } = true;

        protected virtual void Awake() {
            Transform visualTrm = transform.Find("Visual");
            Animator = visualTrm.GetComponent<Animator>();
            SpriteRenderer = visualTrm.GetComponent<SpriteRenderer>();
            Rigidbody = GetComponent<Rigidbody2D>();
            Collider = GetComponent<Collider2D>();
        }
    }
}
