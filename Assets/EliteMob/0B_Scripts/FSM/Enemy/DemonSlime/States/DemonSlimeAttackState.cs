using UnityEngine;
using FSM;

public class DemonSlimeAttackState : DemonSlimeAtkState
{
    public DemonSlimeAttackState(Enemy enemy, EnemyStateMachine<DemonSlimeStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }


}
