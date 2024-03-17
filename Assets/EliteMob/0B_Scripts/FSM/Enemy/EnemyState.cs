using UnityEngine;

namespace FSM {
    public class EnemyState<T> where T : System.Enum
    {
        protected EnemyStateMachine<T> _stateMachine;
        protected Enemy _enemy;

        protected bool _triggerCalled;
        protected int _animationBoolHash;

        protected Rigidbody2D _rigidbody;

        public EnemyState(Enemy enemy, EnemyStateMachine<T> stateMachine, string animationBoolName) {
            _enemy = enemy;
            _stateMachine = stateMachine;
            _animationBoolHash = Animator.StringToHash(animationBoolName);
        }

        public virtual void UpdateState() { }

        public virtual void Enter() {
            _triggerCalled = false;
            _enemy.AnimatorCompo.SetBool(_animationBoolHash, true);
            _rigidbody = _enemy.RigidbodyCompo;
        }

        public virtual void Exit() {
            _enemy.AnimatorCompo.SetBool(_animationBoolHash, false);
            _enemy.AssignLastAnimationHash(_animationBoolHash);
        }

        public virtual void AnimationAttackTrigger() {
            
        }

        public void AnimationFinishTrigger() {
            _triggerCalled = true;
        }
    }
}
