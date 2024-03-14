using UnityEngine;
using FSM;

public class DemonSlimeSmashState : DemonSlimeAtkState
{
    public DemonSlimeSmashState(Enemy enemy, EnemyStateMachine<DemonSlimeStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

}
