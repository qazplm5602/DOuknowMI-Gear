using System.Collections.Generic;

namespace FSM {
    public class EnemyStateMachine<T> where T : System.Enum
    {
        public EnemyState<T> CurrentState { get; private set; }
        public Dictionary<T, EnemyState<T>> StateDictionary = new Dictionary<T, EnemyState<T>>();

        public Enemy _enemy;

        public void Initialize(T startState, Enemy enemy) {
            CurrentState = StateDictionary[startState];
            CurrentState.Enter();
            _enemy = enemy;
        }

        public void ChangeState(T newState, bool forceMode = false) {
            if(!_enemy.CanStateChangeable && !forceMode) return;

            CurrentState.Exit();
            CurrentState = StateDictionary[newState];
            CurrentState.Enter();
        }

        public void AddState(T stateEnum, EnemyState<T> enemyState) {
            StateDictionary.Add(stateEnum, enemyState);
        }
    }
}
