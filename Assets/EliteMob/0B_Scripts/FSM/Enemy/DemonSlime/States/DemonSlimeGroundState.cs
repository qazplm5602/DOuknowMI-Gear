using UnityEngine;
using FSM;

public class DemonSlimeGroundState : EnemyState<DemonSlimeStateEnum>
{
    public DemonSlimeGroundState(Enemy enemy, EnemyStateMachine<DemonSlimeStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    
}
