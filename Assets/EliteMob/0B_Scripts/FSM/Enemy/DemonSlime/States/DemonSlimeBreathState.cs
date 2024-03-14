using UnityEngine;
using FSM;

public class DemonSlimeBreathState : DemonSlimeAtkState
{
    public DemonSlimeBreathState(Enemy enemy, EnemyStateMachine<DemonSlimeStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    
}
