using System;
using System.Collections;
using UnityEngine;

namespace FSM {
    public abstract class Entity : MonoBehaviour
    {
        [HideInInspector] public bool isDead = false;

        #region Components

        public SpriteRenderer SpriteRendererCompo;
        public Animator AnimatorCompo;
        public Rigidbody2D RigidbodyCompo;
        public Collider2D ColliderCompo;

        #endregion

        public Action<int> OnFlip;

        public int FacingDirection { get; protected set; } = 1;
        public bool CanStateChangeable { get; set; } = true;

        protected virtual void Awake() {
            Transform visaulTrm = transform.Find("Visual");
            SpriteRendererCompo = visaulTrm.GetComponent<SpriteRenderer>();
            AnimatorCompo = visaulTrm.GetComponent<Animator>();
            RigidbodyCompo = GetComponent<Rigidbody2D>();
            ColliderCompo = GetComponent<Collider2D>();
        }

        public abstract void Attack();
        public abstract void ReturnDefaultSpeed();

        #region DelayCallbackCoroutine

        public Coroutine StartDelayCallback(float delayTime, Action callback) {
            return StartCoroutine(DelayCoroutine(delayTime, callback));
        }

        private IEnumerator DelayCoroutine(float delayTime, Action callback) {
            yield return new WaitForSeconds(delayTime);
            callback?.Invoke();
        }

        #endregion

        #region VelocityControl

        public void SetVelocity(float x, float y, bool doNotFlip = false) {
            RigidbodyCompo.velocity = new Vector2(x, y);

            if(!doNotFlip) FlipController(x);
        }

        public void StopImmediately(bool withYAxis) {
            if(withYAxis) RigidbodyCompo.velocity = Vector2.zero;
            else RigidbodyCompo.velocity = new Vector2(0, RigidbodyCompo.velocity.y);
        }

        #endregion

        #region FlipController

        public virtual void Flip() {
            FacingDirection = FacingDirection * -1;
            transform.Rotate(0, 180f, 0);
            OnFlip?.Invoke(FacingDirection);
        }

        public virtual void FlipController(float x) {
            if(Mathf.Abs(x) < 0.05f) return;
            
            if(Mathf.Abs(FacingDirection + Mathf.Sign(x)) < 0.5f)
                Flip();
        }

        #endregion
    }
}
