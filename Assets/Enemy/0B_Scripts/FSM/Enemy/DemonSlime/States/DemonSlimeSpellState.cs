using UnityEngine;
using FSM;

public class DemonSlimeSpellState : DemonSlimeAtkState
{
    public DemonSlimeSpellState(Enemy enemy, EnemyStateMachine<DemonSlimeStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    
}
